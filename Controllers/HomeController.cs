using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineMobileRecharge.Data;
using OnlineMobileRecharge.Models;
using Stripe.Checkout;
using System.Diagnostics;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // index page
        public IActionResult Index() { return View(); }

        // Packages list page
        [Authorize]
        public IActionResult Packages(string searchQuery, string packageType)
        {
            var packages = _context.Packages.ToList();

            switch (packageType)
            {
                case "prepaid":
                    packages = packages.FindAll(x => x.Package_Type.Equals(EnumPackageType.Prepaid));
                    break;
                case "postpaid":
                    packages = packages.FindAll(x => x.Package_Type.Equals(EnumPackageType.Postpaid));
                    break;
            }

            if (searchQuery != null)
            {
                packages = packages.FindAll(x => x.Package_Name.Contains(searchQuery)/* || x.Package_Description.Contains(searchQuery)*/);
            }

            return View(packages);
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
                SuccessUrl = domain + $"Home/PackageOrderComplete/" + packageTransaction.PackageTransaction_Id,
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
        public IActionResult Recharges(string searchQuery, string packageType)
        {
            var recharges = _context.Recharges.ToList();

            switch (packageType)
            {
                case "prepaid":
                    recharges = recharges.FindAll(x => x.Recharge_Type.Equals(EnumPackageType.Prepaid));
                    break;
                case "postpaid":
                    recharges = recharges.FindAll(x => x.Recharge_Type.Equals(EnumPackageType.Postpaid));
                    break;
            }

            if (searchQuery != null)
            {
                recharges = recharges.FindAll(x => x.Recharge_Name.Contains(searchQuery)/* || x.Package_Description.Contains(searchQuery)*/);
            }

            return View(recharges);
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
            var services = _context.Services.Where(x => x.IdentityUser.UserName == Environment.UserName).FirstOrDefault();
            return View(services);
        }

        // Caller Tune
        [Authorize]
        public IActionResult CallerTunes()
        {
            var callerTunes = _context.CallerTunes.ToList();
            return View(callerTunes);
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
