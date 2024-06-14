using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMobileRecharge.Data;
using OnlineMobileRecharge.Models;

namespace OnlineMobileRecharge.Controllers
{
    [Authorize(Roles = "Admin")]
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

        // GET: AdminController/Packages
        public IActionResult Packages(string searchQuery, int minPrice, int maxPrice, string packageType, string sortOrder, int page = 1, int pageSize = 30)
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

        // GET: AdminController/PackageDetails/5
        public ActionResult PackageDetails(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                return View(package);
            }
            return RedirectToAction(nameof(Error404));
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

        // GET: AdminController/PackageEdit/5
        public ActionResult PackageEdit(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                return View(package);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/PackageEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PackageEdit(int id, Package package)
        {
            if (id != package.Package_Id)
            {
                return RedirectToAction(nameof(Error404));
            }
            if (ModelState.IsValid)
            {
                _context.Packages.Update(package);
                _context.SaveChanges();
                return RedirectToAction(nameof(Packages));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/PackageDelete/5
        public ActionResult PackageDelete(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                return View(package);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/PackageDelete/5
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
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/Recharge
        public IActionResult Recharges(string searchQuery, int minPrice, int maxPrice, string rechargeType, string sortOrder, int page = 1, int pageSize = 30)
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

        // GET: AdminController/RechargeDetails/5
        public ActionResult RechargeDetails(int id)
        {
            var recharge = _context.Recharges.Find(id);
            if (recharge != null)
            {
                return View(recharge);
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/RechargeAdd
        public ActionResult RechargeAdd() { return View(); }

        // POST: AdminController/RechargeAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RechargeAdd(Recharge recharge)
        {
            if (ModelState.IsValid)
            {
                _context.Recharges.Add(recharge);
                _context.SaveChanges();
                return RedirectToAction(nameof(Recharges));
            }
            return View();
        }

        // GET: AdminController/RechargeEdit/5
        public ActionResult RechargeEdit(int id)
        {
            var recharge = _context.Recharges.Find(id);
            if (recharge != null)
            {
                return View(recharge);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/RechargeEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RechargeEdit(int id, Recharge recharge)
        {
            if (id != recharge.Recharge_Id)
            {
                return RedirectToAction(nameof(Error404));
            }
            if (ModelState.IsValid)
            {
                _context.Recharges.Update(recharge);
                _context.SaveChanges();
                return RedirectToAction(nameof(Recharges));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/RechargeDelete/5
        public ActionResult RechargeDelete(int id)
        {
            var recharge = _context.Recharges.Find(id);
            if (recharge != null)
            {
                return View(recharge);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/RechargeDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RechargeDelete(int id, string name)
        {
            var recharge = _context.Recharges.Find(id);
            if (recharge != null)
            {
                _context.Recharges.Remove(recharge);
                _context.SaveChanges();
                return RedirectToAction(nameof(Recharges));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/CallerTunes
        public IActionResult CallerTunes(string searchQuery, int minPrice, int maxPrice, string sortOrder, int page = 1, int pageSize = 30)
        {
            var tune = _context.CallerTunes.ToList();

            if (searchQuery != null)
            {
                tune = tune.FindAll(c => c.Tune_Name.ToLower().Contains(searchQuery.ToLower()));
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                tune = tune.Where(c => c.Tune_Price >= minPrice && c.Tune_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    tune = tune.OrderBy(c => c.Tune_Name).ToList();
                    break;
                case "name_desc":
                    tune = tune.OrderByDescending(c => c.Tune_Name).ToList();
                    break;
                case "price":
                    tune = tune.OrderBy(c => c.Tune_Price).ToList();
                    break;
                case "price_desc":
                    tune = tune.OrderByDescending(c => c.Tune_Price).ToList();
                    break;
            }

            var totalCount = tune.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = tune.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/CallerTuneAdd
        public ActionResult CallerTuneAdd() { return View(); }

        // POST: AdminController/CallerTuneAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CallerTuneAdd(CallerTune callertune)
        {
            if (ModelState.IsValid)
            {
                _context.CallerTunes.Add(callertune);
                _context.SaveChanges();
                return RedirectToAction(nameof(CallerTunes));
            }
            return View();
        }
        // GET: AdminController/CallerTuneDetails/5
        public ActionResult CallerTuneDetails(int id)
        {
            var callerTune = _context.CallerTunes.Find(id);
            if (callerTune != null)
            {
                return View(callerTune);
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/CallerTuneEdit/5
        public ActionResult CallerTuneEdit(int id)
        {
            var callertune = _context.CallerTunes.Find(id);
            if (callertune != null)
            {
                return View(callertune);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/CallerTuneEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CallerTuneEdit(int id, CallerTune callertune)
        {
            if (id != callertune.Tune_Id)
            {
                return RedirectToAction(nameof(Error404));
            }
            if (ModelState.IsValid)
            {
                _context.CallerTunes.Update(callertune);
                _context.SaveChanges();
                return RedirectToAction(nameof(CallerTunes));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/CallerTuneDelete/5
        public ActionResult CallerTuneDelete(int id)
        {
            var callertune = _context.CallerTunes.Find(id);
            if (callertune != null)
            {
                return View(callertune);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/CallerTuneDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CallerTuneDelete(int id, string name)
        {
            var callertune = _context.CallerTunes.Find(id);
            if (callertune != null)
            {
                _context.CallerTunes.Remove(callertune);
                _context.SaveChanges();
                return RedirectToAction(nameof(CallerTunes));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/TaxRates
        public IActionResult TaxRates(string searchQuery, string sortOrder, int page = 1, int pageSize = 30)
        {
            var taxRate = _context.TaxRates.ToList();

            if (searchQuery != null)
            {
                taxRate = taxRate.FindAll(t => t.Tax_Name.ToLower().Contains(searchQuery.ToLower()));
            }
            switch (sortOrder)
            {
                case "name":
                    taxRate = taxRate.OrderBy(t => t.Tax_Name).ToList();
                    break;
                case "name_desc":
                    taxRate = taxRate.OrderByDescending(t => t.Tax_Name).ToList();
                    break;
            }

            var totalCount = taxRate.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = taxRate.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/TaxDetails/5
        public ActionResult TaxDetails(int id)
        {
            var taxrate = _context.TaxRates.Find(id);
            if (taxrate != null)
            {
                return View(taxrate);
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/TaxAdd
        public ActionResult TaxAdd() { return View(); }

        // POST: AdminController/TaxAdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaxAdd(TaxRate taxrate)
        {
            if (ModelState.IsValid)
            {
                _context.TaxRates.Add(taxrate);
                _context.SaveChanges();
                return RedirectToAction(nameof(TaxRates));
            }
            return View();
        }

        // GET: AdminController/TaxEdit/5
        public ActionResult TaxEdit(int id)
        {
            var taxrate = _context.TaxRates.Find(id);
            if (taxrate != null)
            {
                return View(taxrate);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/TaxEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaxEdit(int id, TaxRate taxrate)
        {
            if (id != taxrate.Tax_Id)
            {
                return RedirectToAction(nameof(Error404));
            }
            if (ModelState.IsValid)
            {
                _context.TaxRates.Update(taxrate);
                _context.SaveChanges();
                return RedirectToAction(nameof(TaxRates));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/TaxDelete/5
        public ActionResult TaxDelete(int id)
        {
            var taxrate = _context.TaxRates.Find(id);
            if (taxrate != null)
            {
                return View(taxrate);
            }
            return RedirectToAction(nameof(Error404));
        }

        // POST: AdminController/TaxDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaxDelete(int id, string name)
        {
            var taxrate = _context.TaxRates.Find(id);
            if (taxrate != null)
            {
                _context.TaxRates.Remove(taxrate);
                _context.SaveChanges();
                return RedirectToAction(nameof(TaxRates));
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/Service
        public IActionResult Service(string searchQuery, string sortOrder, int page = 1, int pageSize = 30)
        {
            var services = _context.Services.Include(s => s.Caller_Tune).Include(s => s.IdentityUser).ToList();

            if (searchQuery != null)
            {
                services = services.FindAll(s => s.IdentityUser.UserName.ToLower().Contains(searchQuery.ToLower()));
            }
            switch (sortOrder)
            {
                case "name":
                    services = services.OrderBy(s => s.IdentityUser.UserName).ToList();
                    break;
                case "name_desc":
                    services = services.OrderByDescending(s => s.IdentityUser.UserName).ToList();
                    break;
            }

            var totalCount = services.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = services.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(services);
        }

        // GET: AdminController/ServiceDetails/5
        public ActionResult ServiceDetails(int id)
        {
            var service = _context.Services.Include(s => s.Caller_Tune).Include(s => s.IdentityUser).First(s => s.Service_Id == id);
            if (service != null)
            {
                return View(service);
            }
            return RedirectToAction(nameof(Error404));
        }

        // GET: AdminController/PackageTransaction
        public IActionResult PackageTransaction(string searchQuery, int minPrice, int maxPrice, string packageType, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.PackageTransactions.Include(x => x.Package).Include(x => x.IdentityUser).ToList();

            switch (packageType)
            {
                case "prepaid":
                    transactions = transactions.FindAll(p => p.Package.Package_Type.Equals(EnumPackageType.Prepaid));
                    break;
                case "postpaid":
                    transactions = transactions.FindAll(p => p.Package.Package_Type.Equals(EnumPackageType.Postpaid));
                    break;
            }
            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.Package.Package_Name.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                transactions = transactions.Where(p => p.Package.Package_Price >= minPrice && p.Package.Package_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.Package.Package_Name).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.Package.Package_Name).ToList();
                    break;
                case "price":
                    transactions = transactions.OrderBy(p => p.Package.Package_Price).ToList();
                    break;
                case "price_desc":
                    transactions = transactions.OrderByDescending(p => p.Package.Package_Price).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/PackageTrasnactionDetails/5
        public IActionResult PackageTrasnactionDetails(int id)
        {
            var transaction = _context.PackageTransactions.Include(x => x.Package).Include(x => x.IdentityUser).First(x => x.PackageTransaction_Id == id);
            return View(transaction);
        }

        // TODO: REcharge TYPES
        // GET: AdminController/RechargeTransaction
        public IActionResult RechargeTransaction(string searchQuery, int minPrice, int maxPrice, string rechargeType, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.RechargeTransactions.Include(x => x.Recharge).Include(x => x.IdentityUser).ToList();

            switch (rechargeType)
            {
                case "prepaid":
                    transactions = transactions.FindAll(p => p.Recharge.Recharge_Type.Equals(EnumPackageType.Prepaid));
                    break;
                case "postpaid":
                    transactions = transactions.FindAll(p => p.Recharge.Recharge_Type.Equals(EnumPackageType.Postpaid));
                    break;
            }
            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.Recharge.Recharge_Name.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                transactions = transactions.Where(p => p.Recharge.Recharge_Price >= minPrice && p.Recharge.Recharge_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.Recharge.Recharge_Name).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.Recharge.Recharge_Name).ToList();
                    break;
                case "price":
                    transactions = transactions.OrderBy(p => p.Recharge.Recharge_Price).ToList();
                    break;
                case "price_desc":
                    transactions = transactions.OrderByDescending(p => p.Recharge.Recharge_Price).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/RechargeTransactionDetails/5
        public IActionResult RechargeTransactionDetails(int id)
        {
            var transaction = _context.RechargeTransactions.Include(x => x.Recharge).Include(x => x.IdentityUser).First(x => x.RechargeTransaction_Id == id);
            return View(transaction);
        }

        // GET: AdminController/CustomRechargeTransaction
        public IActionResult CustomRechargeTransaction(string searchQuery, int minPrice, int maxPrice, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.CustomRechargeTransactions.Include(x => x.TaxRate).Include(x => x.IdentityUser).ToList();

            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.IdentityUser.UserName.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                transactions = transactions.Where(p => p.Recharge_Price >= minPrice && p.Recharge_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.IdentityUser.UserName).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.IdentityUser.UserName).ToList();
                    break;
                case "price":
                    transactions = transactions.OrderBy(p => p.Recharge_Price).ToList();
                    break;
                case "price_desc":
                    transactions = transactions.OrderByDescending(p => p.Recharge_Price).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/CustomRechargeTransactionDetails/5
        public IActionResult CustomRechargeTransactionDetails(int id)
        {
            var transaction = _context.CustomRechargeTransactions.Include(x => x.TaxRate).Include(x => x.IdentityUser).First(x => x.CustomRecharge_Id == id);
            return View(transaction);
        }

        // GET: AdminController/ServiceTransaction
        public IActionResult ServiceTransaction(string searchQuery, int minPrice, int maxPrice, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.ServiceTransactions.Include(x => x.CallerTune).Include(x => x.IdentityUser).ToList();

            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.IdentityUser.UserName.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                transactions = transactions.Where(p => p.CallerTune.Tune_Price >= minPrice && p.CallerTune.Tune_Price <= maxPrice).ToList();
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.IdentityUser.UserName).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.IdentityUser.UserName).ToList();
                    break;
                case "price":
                    transactions = transactions.OrderBy(p => p.CallerTune.Tune_Price).ToList();
                    break;
                case "price_desc":
                    transactions = transactions.OrderByDescending(p => p.CallerTune.Tune_Price).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/ServiceTransactionDetails/5
        public IActionResult ServiceTransactionDetails(int id)
        {
            var transaction = _context.ServiceTransactions.Include(x => x.CallerTune).Include(x => x.IdentityUser).First(x => x.ServiceTransaction_Id == id);
            return View(transaction);
        }

        // GET: AdminController/Contact
        public IActionResult Contact(string searchQuery, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.Contacts.ToList();

            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.Contact_Email.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.Contact_Email).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.Contact_Email).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/FeedBack
        public IActionResult FeedBack(string searchQuery, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.Feedbacks.ToList();

            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.Feedback_Email.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.Feedback_Email).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.Feedback_Email).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/Newsletter
        public IActionResult Newsletter(string searchQuery, string sortOrder, int page = 1, int pageSize = 30)
        {
            var transactions = _context.Newsletter.ToList();

            if (searchQuery != null)
            {
                transactions = transactions.FindAll(p => p.Newsletter_Email.ToLower().Contains(searchQuery.ToLower()));
                page = 1;
            }
            switch (sortOrder)
            {
                case "name":
                    transactions = transactions.OrderBy(p => p.Newsletter_Email).ToList();
                    break;
                case "name_desc":
                    transactions = transactions.OrderByDescending(p => p.Newsletter_Email).ToList();
                    break;
            }

            var totalCount = transactions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            var itemsPerPage = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["totalPages"] = totalPages;
            ViewData["currentPage"] = page;

            return View(itemsPerPage);
        }

        // GET: AdminController/Error404
        public IActionResult Error404() { return View(); }
    }
}
