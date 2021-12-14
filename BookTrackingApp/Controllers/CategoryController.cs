using BookTrackingApp.Database;
using BookTrackingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Controllers
{
    public class CategoryController : Controller
    {
        public BookTrackingDataContext _DbContext;
        public CategoryController(BookTrackingDataContext context)
        {
            _DbContext = context;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            List<CategoryModel> listOfCategoy = new List<CategoryModel>();
            try
            {
                listOfCategoy = _DbContext.Set<CategoryModel>().ToList();

                foreach (var item in listOfCategoy)
                {
                    item.CategoryTypeModel = _DbContext.Set<CategoryTypeModel>().Find(item.Type);
                }
            }
            catch (Exception ex)
            {
                listOfCategoy = new List<CategoryModel>();
            }

            IEnumerable<CategoryModel> result = listOfCategoy;
            return View(result);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            CategoryModel model = new CategoryModel();
            try
            {
                ViewBag.Type=GetAllCategoryType();
                model = _DbContext.Set<CategoryModel>().Find(id);
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
            CategoryModel model = new CategoryModel();
            ViewBag.Type = GetAllCategoryType();
            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel model)
        {
            try
            {
                var duplicateNameData = _DbContext.Set<CategoryModel>().Where(m => m.NameToken.ToLower() == model.NameToken.ToLower()).ToList();
                if (duplicateNameData.Any())
                {
                    ModelState.AddModelError("NameToken", "Already Available this Category");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Type = GetAllCategoryType();
                    return View(model);
                }

                _DbContext.Set<CategoryModel>().Add(model);
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
            CategoryModel model = new CategoryModel();
            try
            {
                ViewBag.Type = GetAllCategoryType();
                model = _DbContext.Set<CategoryModel>().Find(id);
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
        public ActionResult Edit(int id, CategoryModel model)
        {
            try
            {
                var duplicateNameData = _DbContext.Set<CategoryModel>().Where(m => m.NameToken.ToLower() == model.NameToken.ToLower() && m.CategoryId!=id).ToList();
                if (duplicateNameData.Any())
                {
                    ModelState.AddModelError("NameToken", "Already Available this Category");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Type = GetAllCategoryType();
                    return View(model);
                }

                _DbContext.Set<CategoryModel>().Update(model);
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
                var listOfBook = _DbContext.Set<BookModel>().Where(m=>m.Category==id).ToList();
                if (listOfBook.Any())
                {
                    return false;
                }

                var entity = _DbContext.Set<CategoryModel>().Find(id);
                _DbContext.Set<CategoryModel>().Remove(entity);
                _DbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }

        public List<SelectListItem> GetAllCategoryType()
        {
            List<SelectListItem> categoryTypeList = new List<SelectListItem>();
            try
            {
                var listOfCategoyType = _DbContext.Set<CategoryTypeModel>().ToList();

                foreach (var item in listOfCategoyType)
                {
                    categoryTypeList.Add(new SelectListItem {Value=item.CategoryTypeId.ToString(),Text=item.Type });
                }

            }
            catch (Exception ex)
            {
                categoryTypeList = new List<SelectListItem>();
            }
            return categoryTypeList;
        }

    }
}
