using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartInItProjekat.Controllers
{
    [Authorize]
    public class FurnituresController : Controller
    {
        IFurnitureRepo _db;
        public FurnituresController(IFurnitureRepo db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            if(User.IsInRole("Admin"))
            return View("IndexAdmin");
            return View("IndexBuyer");
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
        [Authorize(Roles ="Admin")]
        public ActionResult Add()
        {
            ViewBag.CategoryId = _db.IncludeCategory();
            ViewBag.FurnitureSalonId = _db.IncludeFurnitureSalon();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Furniture furniture, HttpPostedFileBase img)
        {
            if (_db.GetAll().Any(f => f.Code == furniture.Code))
            {
                ModelState.AddModelError("","ALREADY EXIST");
            }
            if (ModelState.IsValid)
            {
                _db.Add(furniture, img);
                return Json(new { success = true, message = "Added Successfully" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.CategoryId = _db.IncludeCategory();
            ViewBag.FurnitureSalonId = _db.IncludeFurnitureSalon();
           
            return Index();
            
        }
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var furniture =_db.GetById(id);
            if (furniture == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = _db.IncludeCategory();
            ViewBag.FurnitureSalonId = _db.IncludeFurnitureSalon();
            return View(furniture);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Furniture furniture, HttpPostedFileBase img = null)
        {
            if (ModelState.IsValid)
            {
                _db.Update(furniture, img);
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.CategoryId = _db.IncludeCategory();
            ViewBag.FurnitureSalonId = _db.IncludeFurnitureSalon();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _db.Delete(id);
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetData()
        {
            var lst = _db.GetAll().ToList();
            var data = lst.Select(s => new
            {
                s.FurnitureId,
                s.Code,
                s.Name,
                s.CountryOfOrigin,
                s.Year,
                Category = s.Category.Name,
                FurnitureSalon = s.FurnitureSalon.Name,
                s.Amount,
                s.Price,
                ProductImage = s.ProductImage != null ? Convert.ToBase64String(s.ProductImage) : null,
                s.PhotoType
            });

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult GetDataFromSalon(int id)
        {
            var lst = _db.GetAll().Where(s=>s.FurnitureSalonId==id).ToList();
            var data = lst.Select(s => new
            {
                s.FurnitureId,
                s.Name,
                Category = s.Category.Name,
                s.Amount,
                s.Price,
                ProductImage = s.ProductImage != null ? Convert.ToBase64String(s.ProductImage) : null,
                s.PhotoType
            });

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Error()
        {
            return View("Erorr");
        }
        
    }
}