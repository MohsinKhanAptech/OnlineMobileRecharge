using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMobileRecharge.Data;
using OnlineMobileRecharge.Models;
using OpenHtmlToPdf;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;

namespace OnlineMobileRecharge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }

        [BindProperty]
        public RechargeTransaction rechargeTransaction { get; set; }
        [BindProperty]
        public CustomRechargeTransaction customRechargeTransaction { get; set; }
        [BindProperty]
        public PackageTransaction packageTransaction { get; set; }
        [BindProperty]
        public ServiceTransaction tuneTransaction { get; set; }

        // index page
        public IActionResult Index() { return View(); }

        // genereate receipt Html
        public string GenerateReceiptHtml(string data, string receiptNo)
        {
            return
                "<!doctype html>" +
                "<html lang=en>" +
                "<head>" +
                    "<meta charset=UTF-8>" +
                    "<meta name=viewport content=\"width=device-width,initial-scale=1\">" +
                    "<title>Receipt</title>" +
                "</head>" +
                "<style>" +
                    "*{box-sizing:border-box;}" +
                    ".text-center{text-align:center}" +
                    ".mp-0{padding:0;margin:0}" +
                    ".border-light{border-radius:.5rem;border:2px solid #d3d3d3}" +
                    "html{font-family:system-ui,-apple-system,BlinkMacSystemFont,\"Segoe UI\",Roboto,Oxygen,Ubuntu,Cantarell,\"Open Sans\",\"Helvetica Neue\",sans-serif}" +
                    "body{height:100vh;margin:6rem 0 0 0;}" +
                    ".wrapper{width:80rem;margin: auto;padding:2rem}" +
                    ".heading{margin-bottom:.25rem;font-size:2.5rem}" +
                    ".sub-heading{color:gray}" +
                    ".container{display:grid;grid-template-columns:auto auto;margin:2rem 6rem 0 6rem;overflow:hidden}" +
                    ".container>div{padding:.5rem;width:50%;}" +
                    ".list-title{background-color:#dcdcdc;float:left;}" +
                    ".list-title>div{padding:0.5rem;}" +
                    ".list-content{float:right;}" +
                    ".list-content>div{padding:0.5rem;}" +
                "</style>" +
                "<body>" +
                    "<div class=\"wrapper border-light\">" +
                        "<h1 class=\"heading text-center mp-0\">Rechargio</h1>" +
                        $"<p class=\"sub-heading text-center mp-0\">Receipt No. {receiptNo}</p>" +
                        "<div class=\"container border-light\">" +
                            data +
                        "</div>" +
                    "</div>" +
                "</body>" +
                "</html>";
        }

        // Packages list page
        [Authorize]
        public IActionResult Packages(string searchQuery, int minPrice, int maxPrice, string packageType, string sortOrder, int page = 1, int pageSize = 8)
        {
            var packages = _context.Packages.ToList();

            switch (packageType)
            {
                case "prepaid":
                    packages = packages.FindAll(p => p.Package_Type.Equals(EnumPackageType.Prepaid));
                    break;
                case "postpaid":
                    packages = packages.FindAll(p => p.Package_Type.Equals(EnumPackageType.Postpaid));
                    break;
            }
            if (searchQuery != null)
            {
                packages = packages.FindAll(p => p.Package_Name.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                packages = packages.Where(p => p.Package_Price >= minPrice && p.Package_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    packages = packages.OrderBy(p => p.Package_Name).ToList();
                    break;
                case "name_desc":
                    packages = packages.OrderByDescending(p => p.Package_Name).ToList();
                    break;
                case "price":
                    packages = packages.OrderBy(p => p.Package_Price).ToList();
                    break;
                case "price_desc":
                    packages = packages.OrderByDescending(p => p.Package_Price).ToList();
                    break;
            }

            var totalCount = packages.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = packages.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // Package Order summary
        [Authorize]
        public IActionResult PackageOrderSummary(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            packageTransaction = new PackageTransaction()
            {
                User_Id = userId,
                IdentityUser = _context.Users.Find(userId),
                Package_Id = id,
                Package = _context.Packages.Find(id)
            };
            return View(packageTransaction);
        }

        // POST Package Order summary
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult PackageOrderSummary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            packageTransaction.User_Id = userId;
            packageTransaction.IdentityUser = _context.Users.Find(userId);
            packageTransaction.Package = _context.Packages.Find(packageTransaction.Package_Id);
            packageTransaction.Transaction_Date = DateTime.UtcNow;
            packageTransaction.Session_Id = "0";
            _context.PackageTransactions.Add(packageTransaction);
            _context.SaveChanges();

            var domain = "https://localhost:7147/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"Home/PackageOrderComplete/{packageTransaction.PackageTransaction_Id}",
                CancelUrl = domain + $"Home/Index"
            };

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(packageTransaction.Package.Package_Price * 100),
                    Currency = "PKR",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = packageTransaction.Package.Package_Name
                    },
                },
                Quantity = 1

            };
            options.LineItems.Add(sessionLineItem);

            var service = new SessionService();
            Session session = service.Create(options);
            packageTransaction.Session_Id = session.Id;
            _context.SaveChanges();
            Response.Headers.Append("Location", session.Url);
            _context.SaveChanges();

            if (packageTransaction.Package.Package_Type == EnumPackageType.Postpaid)
            {
                return View(nameof(PackageOrderComplete), packageTransaction);
            }

            return new StatusCodeResult(303);
        }

        // Package Order complete
        [Authorize]
        public IActionResult PackageOrderComplete(int id)
        {
            var packageTransaction = _context.PackageTransactions.Include(x => x.IdentityUser).Include(x => x.Package).First(x => x.PackageTransaction_Id == id);
            if (packageTransaction == null)
            {
                return View(nameof(Packages));
            }
            return View(packageTransaction);
        }

        // Package genereate receipt
        [Authorize]
        public IActionResult PackageGenerateReceipt(int id, bool download)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transaction = _context.PackageTransactions.Include(x => x.IdentityUser).Include(x => x.Package).First(x => x.PackageTransaction_Id == id);

            if (currentUserId != transaction.IdentityUser.Id)
            {
                return RedirectToAction(nameof(Error404));
            }

            var html = GenerateReceiptHtml(
                            "<div class=\"list-title\">" +
                                "<div>Transaction Id</div>" +
                                "<div>Customer Name</div>" +
                                "<div>Mobile Number</div>" +
                                "<div>Package Name</div>" +
                                "<div>Package Duration</div>" +
                                "<div>Package Type</div>" +
                                "<div>Package Price</div>" +
                                "<div>Payment Method</div>" +
                                "<div>Transaction Date</div>" +
                            "</div>" +
                            "<div class=\"list-content\">" +
                                $"<div>{transaction.PackageTransaction_Id}</div>" +
                                $"<div>{transaction.IdentityUser.UserName}</div>" +
                                $"<div>{transaction.Mobile_Number}</div>" +
                                $"<div>{transaction.Package.Package_Name}</div>" +
                                $"<div>{transaction.Package.Package_Duration}</div>" +
                                $"<div>{transaction.Package.Package_Type}</div>" +
                                $"<div>{transaction.Package.Package_Price}</div>" +
                                $"<div>Stripe</div>" +
                                $"<div>{transaction.Transaction_Date.ToLocalTime()}</div>" +
                            "</div>",
                            transaction.PackageTransaction_Id.ToString()
                            );

            var pdf = Pdf.From(html).OfSize(OpenHtmlToPdf.PaperSize.A4Rotated).Content();

            if (download == true)
            {
                return File(pdf, "application/pdf", $"Reciept{transaction.Transaction_Date}.pdf");
            }

            return File(pdf, "application/pdf");
        }

        // Recharges
        public IActionResult Recharges(string searchQuery, int minPrice, int maxPrice, string rechargeType, string sortOrder, int page = 1, int pageSize = 8)
        {
            var recharges = _context.Recharges.ToList();

            switch (rechargeType)
            {
                case "prepaid":
                    recharges = recharges.FindAll(r => r.Recharge_Type.Equals(EnumPackageType.Prepaid));
                    break;
                case "postpaid":
                    recharges = recharges.FindAll(r => r.Recharge_Type.Equals(EnumPackageType.Postpaid));
                    break;
                case "special":
                    recharges = recharges.FindAll(r => r.Recharge_Type.Equals(EnumPackageType.Special));
                    break;
            }
            if (searchQuery != null)
            {
                recharges = recharges.FindAll(r => r.Recharge_Name.ToLower().Contains(searchQuery.ToLower()));
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                recharges = recharges.Where(r => r.Recharge_Price >= minPrice && r.Recharge_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    recharges = recharges.OrderBy(r => r.Recharge_Name).ToList();
                    break;
                case "name_desc":
                    recharges = recharges.OrderByDescending(r => r.Recharge_Name).ToList();
                    break;
                case "price":
                    recharges = recharges.OrderBy(r => r.Recharge_Price).ToList();
                    break;
                case "price_desc":
                    recharges = recharges.OrderByDescending(r => r.Recharge_Price).ToList();
                    break;
            }

            var totalCount = recharges.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = recharges.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // Recharge Order summary
        public IActionResult RechargeOrderSummary(int id)
        {
            rechargeTransaction = new RechargeTransaction()
            {
                Recharge_Id = id,
                Mobile_Number = "",
                Recharge = _context.Recharges.Find(id)
            };
            return View(rechargeTransaction);
        }

        // POST Recharge Order summary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RechargeOrderSummary()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                customRechargeTransaction.User_Id = userId;
                customRechargeTransaction.IdentityUser = _context.Users.Find(userId);
            }

            rechargeTransaction.Recharge = _context.Recharges.Find(rechargeTransaction.Recharge_Id);
            rechargeTransaction.Transaction_Date = DateTime.UtcNow;
            rechargeTransaction.Session_Id = "0";
            _context.RechargeTransactions.Add(rechargeTransaction);
            _context.SaveChanges();

            var domain = "https://localhost:7147/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"Home/RechargeOrderComplete/{rechargeTransaction.RechargeTransaction_Id}",
                CancelUrl = domain + $"Home/Index"
            };

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(rechargeTransaction.Recharge.Recharge_Price * 100),
                    Currency = "PKR",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = rechargeTransaction.Recharge.Recharge_Name
                    },
                },
                Quantity = 1

            };
            options.LineItems.Add(sessionLineItem);

            var service = new SessionService();
            Session session = service.Create(options);
            rechargeTransaction.Session_Id = session.Id;
            _context.SaveChanges();
            Response.Headers.Append("Location", session.Url);
            _context.SaveChanges();
            return new StatusCodeResult(303);
        }

        // Recharge Order complete
        public IActionResult RechargeOrderComplete(int id)
        {
            var rechargeTransaction = _context.RechargeTransactions.Include(x => x.IdentityUser).Include(x => x.Recharge).First(x => x.RechargeTransaction_Id == id);
            if (rechargeTransaction == null)
            {
                return View(nameof(Recharges));
            }
            return View(rechargeTransaction);
        }

        // Package genereate receipt
        [Authorize]
        public IActionResult RechargeGenerateReceipt(int id, bool download)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transaction = _context.RechargeTransactions.Include(x => x.IdentityUser).Include(x => x.Recharge).First(x => x.RechargeTransaction_Id == id);

            if (currentUserId != transaction.IdentityUser.Id)
            {
                return RedirectToAction(nameof(Error404));
            }

            var html = GenerateReceiptHtml(
                            "<div class=\"list-title\">" +
                                "<div>Transaction Id</div>" +
                                "<div>Customer Name</div>" +
                                "<div>Mobile Number</div>" +
                                "<div>Recharge Name</div>" +
                                "<div>Recharge Type</div>" +
                                "<div>Recharge Price</div>" +
                                "<div>Tax Rate</div>" +
                                "<div>Taxed Amount</div>" +
                                "<div>Recharge Amount</div>" +
                                "<div>Payment Method</div>" +
                                "<div>Transaction Date</div>" +
                            "</div>" +
                            "<div class=\"list-content\">" +
                                $"<div>{transaction.RechargeTransaction_Id}</div>" +
                                $"<div>{transaction.IdentityUser.UserName}</div>" +
                                $"<div>{transaction.Mobile_Number}</div>" +
                                $"<div>{transaction.Recharge.Recharge_Name}</div>" +
                                $"<div>{transaction.Recharge.Recharge_Type}</div>" +
                                $"<div>{transaction.Recharge.Recharge_Price}</div>" +
                                $"<div>{transaction.Recharge.Recharge_Tax_Rate}</div>" +
                                $"<div>{transaction.Recharge.Recharge_Taxed_Amount}</div>" +
                                $"<div>{transaction.Recharge.Recharge_Amount}</div>" +
                                $"<div>Stripe</div>" +
                                $"<div>{transaction.Transaction_Date.ToLocalTime()}</div>" +
                            "</div>",
                            transaction.RechargeTransaction_Id.ToString()
                            );

            var pdf = Pdf.From(html).OfSize(OpenHtmlToPdf.PaperSize.A4Rotated).Content();

            if (download == true)
            {
                return File(pdf, "application/pdf", $"Reciept{transaction.Transaction_Date}.pdf");
            }

            return File(pdf, "application/pdf");
        }

        // Custom Recharge 
        public IActionResult CustomRecharge()
        {
            customRechargeTransaction = new CustomRechargeTransaction
            {
                TaxRate = _context.TaxRates.First(),
                Tax_Id = _context.TaxRates.First().Tax_Id,
                Transaction_Date = DateTime.UtcNow,
            };
            return View(customRechargeTransaction);
        }

        // POST Custom Recharge Order summary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomRechargeOrderSummary()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                customRechargeTransaction.User_Id = userId;
                customRechargeTransaction.IdentityUser = _context.Users.Find(userId);
            }

            customRechargeTransaction.TaxRate = _context.TaxRates.First();
            customRechargeTransaction.Tax_Id = customRechargeTransaction.TaxRate.Tax_Id;
            if (customRechargeTransaction.Recharge_Amount != customRechargeTransaction.Recharge_Price - customRechargeTransaction.Recharge_Price * customRechargeTransaction.TaxRate.Tax_Rate / 100)
            {
                return RedirectToAction(nameof(CustomRecharge), customRechargeTransaction);
            };
            customRechargeTransaction.Transaction_Date = DateTime.UtcNow;
            customRechargeTransaction.Session_Id = "0";
            _context.CustomRechargeTransactions.Add(customRechargeTransaction);
            _context.SaveChanges();

            var domain = "https://localhost:7147/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"Home/CustomRechargeOrderComplete/{customRechargeTransaction.CustomRecharge_Id}",
                CancelUrl = domain + $"Home/Index"
            };

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(customRechargeTransaction.Recharge_Price * 100),
                    Currency = "PKR",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"Custom Recharge Rs.{customRechargeTransaction.Recharge_Price}"
                    },
                },
                Quantity = 1

            };
            options.LineItems.Add(sessionLineItem);

            var service = new SessionService();
            Session session = service.Create(options);
            customRechargeTransaction.Session_Id = session.Id;
            _context.SaveChanges();
            Response.Headers.Append("Location", session.Url);
            _context.SaveChanges();
            return new StatusCodeResult(303);
        }

        // Custom Recharge Order complete
        public IActionResult CustomRechargeOrderComplete(int id)
        {
            var customRechargeTransaction = _context.CustomRechargeTransactions.Include(x => x.IdentityUser).Include(x => x.TaxRate).First(x => x.CustomRecharge_Id == id);
            if (customRechargeTransaction == null)
            {
                return RedirectToAction(nameof(CustomRecharge));
            }
            return View(customRechargeTransaction);
        }

        // Package genereate receipt
        [Authorize]
        public IActionResult CustomRechargeGenerateReceipt(int id, bool download)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transaction = _context.CustomRechargeTransactions.Include(x => x.IdentityUser).Include(x => x.TaxRate).First(x => x.CustomRecharge_Id == id);

            if (currentUserId != transaction.IdentityUser.Id)
            {
                return RedirectToAction(nameof(Error404));
            }

            var html = GenerateReceiptHtml(
                            "<div class=\"list-title\">" +
                                "<div>Transaction Id</div>" +
                                "<div>Customer Name</div>" +
                                "<div>Mobile Number</div>" +
                                "<div>Recharge Price</div>" +
                                "<div>Tax Rate</div>" +
                                "<div>Recharge Amount</div>" +
                                "<div>Payment Method</div>" +
                                "<div>Transaction Date</div>" +
                            "</div>" +
                            "<div class=\"list-content\">" +
                                $"<div>{transaction.CustomRecharge_Id}</div>" +
                                $"<div>{transaction.IdentityUser.UserName}</div>" +
                                $"<div>{transaction.Mobile_Number}</div>" +
                                $"<div>{transaction.Recharge_Price}</div>" +
                                $"<div>{transaction.TaxRate.Tax_Rate}</div>" +
                                $"<div>{transaction.Recharge_Amount}</div>" +
                                $"<div>Stripe</div>" +
                                $"<div>{transaction.Transaction_Date.ToLocalTime()}</div>" +
                            "</div>",
                            transaction.CustomRecharge_Id.ToString()
                            );

            var pdf = Pdf.From(html).OfSize(OpenHtmlToPdf.PaperSize.A4Rotated).Content();

            if (download == true)
            {
                return File(pdf, "application/pdf", $"Reciept{transaction.Transaction_Date}.pdf");
            }

            return File(pdf, "application/pdf");
        }

        // Services
        [Authorize]
        public IActionResult Services()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var services = _context.Services.First(x => x.User_Id == userId);
            services.Caller_Tune = _context.CallerTunes.Find(services.Caller_Tune_Id);
            services.IdentityUser = _context.Users.Find(services.User_Id);
            return View(services);
        }

        // Caller Tune
        [Authorize]
        public IActionResult CallerTunes(string searchQuery, int minPrice, int maxPrice, string sortOrder, int page = 1, int pageSize = 8)
        {
            var callerTunes = _context.CallerTunes.ToList();

            if (searchQuery != null)
            {
                callerTunes = callerTunes.FindAll(x => x.Tune_Name.ToLower().Contains(searchQuery.ToLower()));
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                callerTunes = callerTunes.Where(r => r.Tune_Price >= minPrice && r.Tune_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    callerTunes = callerTunes.OrderBy(r => r.Tune_Name).ToList();
                    break;
                case "name_desc":
                    callerTunes = callerTunes.OrderByDescending(r => r.Tune_Name).ToList();
                    break;
                case "price":
                    callerTunes = callerTunes.OrderBy(r => r.Tune_Price).ToList();
                    break;
                case "price_desc":
                    callerTunes = callerTunes.OrderByDescending(r => r.Tune_Price).ToList();
                    break;
            }

            var totalCount = callerTunes.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = callerTunes.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // CallerTunes Order summary
        [Authorize]
        public IActionResult CallerTuneOrderSummary(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Find(userId);
            tuneTransaction = new ServiceTransaction()
            {
                Tune_Id = id,
                CallerTune = _context.CallerTunes.Find(id),
                User_Id = userId,
                IdentityUser = user,
                Mobile_Number = user.PhoneNumber,
                Transaction_Date = DateTime.UtcNow,
            };
            return View(tuneTransaction);
        }

        // POST CallerTunes Order summary
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CallerTuneOrderSummary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Find(userId);

            tuneTransaction.User_Id = userId;
            tuneTransaction.IdentityUser = user;
            tuneTransaction.Mobile_Number = user.PhoneNumber;
            tuneTransaction.CallerTune = _context.CallerTunes.Find(tuneTransaction.Tune_Id);
            tuneTransaction.Transaction_Date = DateTime.UtcNow;
            tuneTransaction.Session_Id = "0";
            _context.ServiceTransactions.Add(tuneTransaction);
            _context.SaveChanges();

            var domain = "https://localhost:7147/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"Home/CallerTuneOrderComplete/{tuneTransaction.ServiceTransaction_Id}",
                CancelUrl = domain + $"Home/Index"
            };

            var sessionLineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(tuneTransaction.CallerTune.Tune_Price * 100),
                    Currency = "PKR",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = tuneTransaction.CallerTune.Tune_Name
                    },
                },
                Quantity = 1

            };
            options.LineItems.Add(sessionLineItem);

            var service = new SessionService();
            Session session = service.Create(options);
            rechargeTransaction.Session_Id = session.Id;
            _context.SaveChanges();
            Response.Headers.Append("Location", session.Url);
            _context.SaveChanges();
            return new StatusCodeResult(303);
        }

        // CallerTunes Order complete
        [Authorize]
        public IActionResult CallerTuneOrderComplete(int id)
        {
            var tuneTransaction = _context.ServiceTransactions.Find(id);
            tuneTransaction.CallerTune = _context.CallerTunes.Find(tuneTransaction.Tune_Id);
            if (tuneTransaction == null)
            {
                return View(nameof(CallerTunes));
            }
            return View(tuneTransaction);
        }

        // Package genereate receipt
        [Authorize]
        public IActionResult CallerTuneGenerateReceipt(int id, bool download)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transaction = _context.ServiceTransactions.Include(x => x.IdentityUser).Include(x => x.CallerTune).First(x => x.ServiceTransaction_Id == id);

            if (currentUserId != transaction.IdentityUser.Id)
            {
                return RedirectToAction(nameof(Error404));
            }

            var html = GenerateReceiptHtml(
                            "<div class=\"list-title\">" +
                                "<div>Transaction Id</div>" +
                                "<div>Customer Name</div>" +
                                "<div>Mobile Number</div>" +
                                "<div>Caller Tune Price</div>" +
                                "<div>Payment Method</div>" +
                                "<div>Transaction Date</div>" +
                            "</div>" +
                            "<div class=\"list-content\">" +
                                $"<div>{transaction.ServiceTransaction_Id}</div>" +
                                $"<div>{transaction.IdentityUser.UserName}</div>" +
                                $"<div>{transaction.Mobile_Number}</div>" +
                                $"<div>{transaction.CallerTune.Tune_Price}</div>" +
                                $"<div>Stripe</div>" +
                                $"<div>{transaction.Transaction_Date.ToLocalTime()}</div>" +
                            "</div>",
                            transaction.ServiceTransaction_Id.ToString()
                            );

            var pdf = Pdf.From(html).OfSize(OpenHtmlToPdf.PaperSize.A4Rotated).Content();

            if (download == true)
            {
                return File(pdf, "application/pdf", $"Reciept{transaction.Transaction_Date}.pdf");
            }

            return File(pdf, "application/pdf");
        }

        // Do Not Disturb
        [Authorize]
        public IActionResult DoNotDisturb(int serviceId, bool doNotDisturb)
        {
            var service = _context.Services.Find(serviceId);
            service.Do_Not_Disturb = doNotDisturb;
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Services));
        }

        // Transaction History
        [Authorize]
        public IActionResult TransactionHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var rechargeTransactions = _context.RechargeTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.Recharge).ToList();
            var customRechargeTransactions = _context.CustomRechargeTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.TaxRate).ToList();
            var packageTransactions = _context.PackageTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.Package).ToList();
            var serviceTransactions = _context.ServiceTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.CallerTune).ToList();

            ViewBag.rechargeTransactions = rechargeTransactions;
            ViewBag.customRechargeTransactions = customRechargeTransactions;
            ViewBag.packageTransactions = packageTransactions;
            ViewBag.serviceTransactions = serviceTransactions;

            return View();
        }

        // download transaction history
        [Authorize]
        public List<object> DownloadTransactionHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var rechargeTransactions = _context.RechargeTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.Recharge).ToList();
            var customRechargeTransactions = _context.CustomRechargeTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.TaxRate).ToList();
            var packageTransactions = _context.PackageTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.Package).ToList();
            var serviceTransactions = _context.ServiceTransactions.Where(x => x.User_Id == userId).Include(x => x.IdentityUser).Include(x => x.CallerTune).ToList();

            var transactionHistory = new List<object> { rechargeTransactions, customRechargeTransactions, packageTransactions, serviceTransactions };

            return transactionHistory;
        }


        // contact us page
        public IActionResult Contact() { return View(); }

        // POST: contact us page form submit
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return View(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Feedback page
        public IActionResult Feedback() { return View(); }

        // POST: Feedback page form submit
        [HttpPost]
        public IActionResult Feedback(Feedback feedback)
        {
            try
            {
                _context.Feedbacks.Add(feedback);
                _context.SaveChanges();
                return View(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // About page
        public IActionResult About() { return View(); }

        // FAQ page
        public IActionResult FAQ() { return View(); }

        public IActionResult Privacy() { return View(); }

        // Error page
        public IActionResult Error404() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
