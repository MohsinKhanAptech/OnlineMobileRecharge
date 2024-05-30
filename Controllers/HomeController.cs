using Microsoft.AspNetCore.Mvc;
using OnlineMobileRecharge.Data;
using OnlineMobileRecharge.Models;
using System.Diagnostics;

namespace OnlineMobileRecharge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // index page
        public IActionResult Index()
        {
            return View();
        }

        // Packages list page
        public IActionResult Packages()
        {
            return View();
        }

        // contact us page
        public IActionResult Contact()
        {
            return View();
        }

        // POST: contact us page form submit
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return View(nameof(Index));
        }

        // Feedback page
        public IActionResult Feedback()
        {
            return View();
        }

        // About page
        public IActionResult About()
        {
            return View();
        }

        // FAQ page
        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Error page
        public IActionResult Error404()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
