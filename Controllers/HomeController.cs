using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using OnlineMobileRecharge.Data;
using OnlineMobileRecharge.Models;
using Stripe.Checkout;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Security.Claims;

namespace OnlineMobileRecharge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public RechargeTransaction rechargeTransaction { get; set; }
        [BindProperty]
        public CustomRechargeTransaction customRechargeTransaction { get; set; }
        [BindProperty]
        public PackageTransaction packageTransaction { get; set; }
        [BindProperty]
        public ServiceTransaction tuneTransaction { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // index page
        public IActionResult Index() { return View(); }

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
            packageTransaction.Transaction_Date = DateTime.Now;
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
            return new StatusCodeResult(303);
        }

        // Package Order complete
        [Authorize]
        public IActionResult PackageOrderComplete(int id)
        {
            var packageTransaction = _context.PackageTransactions.Find(id);
            packageTransaction.Package = _context.Packages.Find(packageTransaction.Package_Id);
            if (packageTransaction == null)
            {
                return View(nameof(Packages));
            }
            return View(packageTransaction);
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
            rechargeTransaction.Recharge = _context.Recharges.Find(rechargeTransaction.Recharge_Id);
            rechargeTransaction.Transaction_Date = DateTime.Now;
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
            var rechargeTransaction = _context.RechargeTransactions.Find(id);
            rechargeTransaction.Recharge = _context.Recharges.Find(rechargeTransaction.Recharge_Id);
            if (rechargeTransaction == null)
            {
                return View(nameof(Recharges));
            }
            return View(rechargeTransaction);
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
                Transaction_Date = DateTime.Now,
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
            tuneTransaction.Transaction_Date = DateTime.Now;
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

        // Do Not Disturb
        public IActionResult DoNotDisturb(int serviceId, bool doNotDisturb)
        {
            var service = _context.Services.Find(serviceId);
            service.Do_Not_Disturb = doNotDisturb;
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Services));
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
