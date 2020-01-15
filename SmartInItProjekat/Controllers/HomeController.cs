using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartInItProjekat.Controllers
{
    public class HomeController : Controller
    {
        IFurnitureRepo _db;
        public HomeController(IFurnitureRepo db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("IndexAuth");
            }
            ViewBag.FurnitureSalonId = _db.IncludeFurnitureSalon();
            return View();
        }
        public ActionResult PopUpUpdate()
        {
            return PartialView("_PopUpUpdate");
        }
        public ActionResult PopUpAdd()
        {
            return PartialView("_PopUpAdd");
        }
        public ActionResult PopUpDelete()
        {
            return PartialView("_PopUpDelete");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Proba()
        {
            return View();
        }
    }
}