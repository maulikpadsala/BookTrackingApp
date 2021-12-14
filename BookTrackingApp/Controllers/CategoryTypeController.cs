using BookTrackingApp.Database;
using BookTrackingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Controllers
{
    [ResponseCache(Duration = 4000)]
    public class CategoryTypeController : Controller
    {
        public BookTrackingDataContext _DbContext;
        public CategoryTypeController(BookTrackingDataContext context)
        {
            _DbContext = context;
        }

        // GET: CategoryTypeController1
        public ActionResult Index()
        {
            List<CategoryTypeModel> listOfCategoyType = new List<CategoryTypeModel>();
            try
            {
                listOfCategoyType = _DbContext.Set<CategoryTypeModel>().ToList();
            }
            catch (Exception ex)
            {
                listOfCategoyType = new List<CategoryTypeModel>();
            }

            IEnumerable<CategoryTypeModel> result = listOfCategoyType;

            return View(result);
        }

        // GET: CategoryTypeController1/Details/5
        public ActionResult Details(int id)
        {
            CategoryTypeModel model = new CategoryTypeModel();
            try
            {
                model = _DbContext.Set<CategoryTypeModel>().Find(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: CategoryTypeController1/Create
        public ActionResult Create()
        {
            CategoryTypeModel model = new CategoryTypeModel();
            
            return View(model);
        }

        // POST: CategoryTypeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryTypeModel model)
        {
            try
            {
                var duplicateTypeData = _DbContext.Set<CategoryTypeModel>().Where(m => m.Type.ToLower() == model.Type.ToLower()).ToList();
                if (duplicateTypeData.Any())
                {
                    ModelState.AddModelError("Type", "Already Available this Category Type");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                _DbContext.Set<CategoryTypeModel>().Add(model);
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

        // GET: CategoryTypeController1/Edit/5
        public ActionResult Edit(int id=0)
        {
            CategoryTypeModel model = new CategoryTypeModel();
            try
            {
                model = _DbContext.Set<CategoryTypeModel>().Find(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: CategoryTypeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryTypeModel model)
        {
            try
            {
                var duplicateTypeData = _DbContext.Set<CategoryTypeModel>().Where(m => m.Type.ToLower() == model.Type.ToLower() && m.CategoryTypeId != id).ToList();
                if (duplicateTypeData.Any())
                {
                    ModelState.AddModelError("Type", "Already Available this Category Type");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                _DbContext.Set<CategoryTypeModel>().Update(model);
                _DbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryTypeController1/Delete/5
        public bool Delete(int id)
        {
            var result = false;
            try
            {
                var listOfCategory = _DbContext.Set<CategoryModel>().Where(m => m.Type == id).ToList();
                if (listOfCategory.Any())
                {
                    return false;
                }

                var entity = _DbContext.Set<CategoryTypeModel>().Find(id);
                _DbContext.Set<CategoryTypeModel>().Remove(entity);
                _DbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }

        
    }
}
