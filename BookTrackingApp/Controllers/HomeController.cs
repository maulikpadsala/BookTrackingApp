using BookTrackingApp.Database;
using BookTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Controllers
{
    [ResponseCache(Duration = 4000)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public BookTrackingDataContext _DbContext;
        public HomeController(ILogger<HomeController> logger, BookTrackingDataContext context)
        {
            _logger = logger;
            _DbContext = context;
        }

        // GET: HomeController        
        public IActionResult Index()
        {
            List<BookModel> listOfCategoy = new List<BookModel>();
            try
            {
                listOfCategoy = _DbContext.Set<BookModel>().ToList();

                foreach (var item in listOfCategoy)
                {
                    item.CategoryModel = _DbContext.Set<CategoryModel>().Find(item.Category);
                }
            }
            catch (Exception ex)
            {
                listOfCategoy = new List<BookModel>();
            }

            IEnumerable<BookModel> result = listOfCategoy;

            return View(result);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            BookModel model = new BookModel();
            try
            {
                ViewBag.Category = GetAllCategory();
                model = _DbContext.Set<BookModel>().Find(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            BookModel model = new BookModel();
            ViewBag.Category = GetAllCategory();
            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModel model)
        {
            try
            {
                var duplicateISBNData = _DbContext.Set<BookModel>().Where(m => m.ISBN.ToLower() == model.ISBN.ToLower()).ToList();
                if (duplicateISBNData.Any())
                {
                    ModelState.AddModelError("ISBN", "Already Registered with this ISBN Number");                    
                }

                var duplicateTitleData = _DbContext.Set<BookModel>().Where(m => m.Title.ToLower() == model.Title.ToLower()).ToList();
                if (duplicateTitleData.Any())
                {
                    ModelState.AddModelError("Title", "Already Registered with this Title");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Category = GetAllCategory();
                    return View(model);
                }

                _DbContext.Set<BookModel>().Add(model);
                int id = _DbContext.SaveChanges();

                if (id <= 0)
                {
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            BookModel model = new BookModel();
            try
            {
                ViewBag.Category = GetAllCategory();
                model = _DbContext.Set<BookModel>().Find(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookModel model)
        {
            try
            {
                var duplicateISBNData = _DbContext.Set<BookModel>().Where(m => m.ISBN.ToLower() == model.ISBN.ToLower() && m.BookId !=id).ToList();
                if (duplicateISBNData.Any())
                {
                    ModelState.AddModelError("ISBN", "Already Registered with this ISBN Number");
                }

                var duplicateTitleData = _DbContext.Set<BookModel>().Where(m => m.Title.ToLower() == model.Title.ToLower() && m.BookId != id).ToList();
                if (duplicateTitleData.Any())
                {
                    ModelState.AddModelError("Title", "Already Registered with this Title");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Category = GetAllCategory();
                    return View(model);
                }

                _DbContext.Set<BookModel>().Update(model);
                _DbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        public bool Delete(int id)
        {
            var result = false;
            try
            {
                var entity = _DbContext.Set<BookModel>().Find(id);
                _DbContext.Set<BookModel>().Remove(entity);
                _DbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }

        public List<SelectListItem> GetAllCategory()
        {
            List<SelectListItem> categoryList = new List<SelectListItem>();
            try
            {
                var listOfCategoy = _DbContext.Set<CategoryModel>().ToList();

                foreach (var item in listOfCategoy)
                {
                    categoryList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.NameToken });
                }

            }
            catch (Exception ex)
            {
                categoryList = new List<SelectListItem>();
            }
            return categoryList;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
