using Microsoft.AspNet.Identity.Owin;
using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using SmartInItProjekat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartInItProjekat.Controllers
{
    [Authorize(Roles ="Admin")]
  
    public class UsersController : Controller
    {
        ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        IUsers _db;
        public UsersController(IUsers db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            var data = _db.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            ViewBag.Name = _db.IncludeRoles();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Adress = model.Address };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.UserRole);
                    return Json(new { success = true, message = "Added Successfully" }, JsonRequestBehavior.AllowGet);

                }
            }
            ViewBag.Name = _db.IncludeRoles();
            return Index();
        }
        [AllowAnonymous]
        public JsonResult DoesUserNameExist(string userName)
        {
            return Json(!_db.DoesExist(userName), JsonRequestBehavior.AllowGet);
        }
    }
}