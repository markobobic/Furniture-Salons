using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartInItProjekat.Controllers
{
    [Authorize]
    public class FurnitureSalonsController : Controller
    {
        IFurnitureSalonRepo _db;
        public FurnitureSalonsController(IFurnitureSalonRepo db)
        {
            _db = db;
        }
        // GET: FurnitureSalons
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View("IndexAdmin");
            }
            return View("IndexBuyer");
        }
        public ActionResult GetData()
        {
            var salonsList = _db.GetAll().ToList();
            var salonData = salonsList.Select(s => new
            {
                s.FurnitureSalonId,
                s.Name,
                s.Owner,
                s.Adress,
                s.TelephoneNumber,
                s.Email,
                s.WebPage,
                s.PIB,
                s.AccountNumber
            });
            return this.Json(salonData, JsonRequestBehavior.AllowGet);
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
       
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(FurnitureSalon salon)
        {
          
            if (ModelState.IsValid)
            {
                await _db.Add(salon);
                await _db.SaveAsync();
                return Json(new { success = true, message = "Added Successfully" });
            }
            return View(salon);
        }
        public ActionResult Update(int? id)
        {
            return Details(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(FurnitureSalon salon)
        {
            if (ModelState.IsValid)
            {
              await _db.Update(salon);
              return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return View(salon);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _db.Delete(id);
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoesPIBExist(string PIB)
        {
            return Json(!_db.DoesPIBExist(PIB), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DoesAccountNumberExist(string accountNumber)
        {
            return Json(!_db.DoesAccountNumberExist(accountNumber), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DoesEmailExist(string Email)
        {
            return Json(!_db.DoesEmailExist(Email), JsonRequestBehavior.AllowGet);
        }

    }
}

