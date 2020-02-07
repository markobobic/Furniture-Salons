using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartInItProjekat.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoriesRepo _db;
        public CategoriesController(ICategoriesRepo db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View("IndexAdmin");
            return View("IndexBuyer");
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
               _db.Add(category);
                return Json(new { success = true, message = "Added Successfully" });
            }
            return View(category);
        }
        public ActionResult Update(int? id)
        {
            return Details(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Update(category);
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return View(category);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (_db.GetById(id) == null)
            {
                return HttpNotFound();
            }
              return View(_db.GetById(id));
         }
        public ActionResult GetData()
        {
            var lst = _db.GetAll();
            var data = lst.Select(c => new
            {
              c.CategoryId,
              Name = c.Name,
              Description=c.Description
            });
            return this.Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Delete(int id)
        {
            _db.Delete(id);
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DoesNameExist(string name)
        {
            return Json(!_db.DoesNameExist(name), JsonRequestBehavior.AllowGet);
        }
    }
}