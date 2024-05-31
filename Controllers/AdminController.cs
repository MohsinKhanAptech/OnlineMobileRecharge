using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Core;
using OnlineMobileRecharge.Data;
using OnlineMobileRecharge.Models;

namespace OnlineMobileRecharge.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: AdminController
        public ActionResult Index() { return View(); }

        public IActionResult Packages()
        {
            var data = _context.Packages.ToList();
            return View(data);
        }

        // GET: AdminController/Details/5
        public ActionResult PackageDetails(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                return View(package);
            }
            return View(nameof(Error404));
        }

        // GET: AdminController/PackageAdd
        public ActionResult PackageAdd() { return View(); }

        // POST: AdminController/PackageAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PackageAdd(Package package)
        {
            if (ModelState.IsValid)
            {
                _context.Packages.Add(package);
                _context.SaveChanges();
                return RedirectToAction(nameof(Packages));
            }
            return View();
        }

        // GET: AdminController/Edit/5
        public ActionResult PackageEdit(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                return View(package);
            }
            return View(nameof(Error404));
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PackageEdit(int id, Package package)
        {
            if (id != package.Package_Id)
            {
                return View(nameof(Error404));
            }
            if (ModelState.IsValid)
            {
                _context.Packages.Update(package);
                _context.SaveChanges();
                return RedirectToAction(nameof(Packages));
            }
            return View(nameof(Error404));
        }

        // GET: AdminController/Delete/5
        public ActionResult PackageDelete(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                return View(package);
            }
            return View(nameof(Error404));
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PackageDelete(int id, string name)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                _context.SaveChanges();
                return RedirectToAction(nameof(Packages));
            }
            return View(nameof(Error404));
        }

        // GET: AdminController/Error404
        public IActionResult Error404() { return View(); }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
