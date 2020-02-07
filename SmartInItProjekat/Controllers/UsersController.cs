using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SmartInItProjekat.Models;
using SmartInItProjekat.Repository;
using SmartInItProjekat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult IndexBuyers()
        {
            return View("Buyers");
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
        public ActionResult GetAllBuyers()
        {
            var data = _db.GetAllBuyers();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            ViewBag.Name = _db.IncludeRolesAdd();
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
            ViewBag.Name = _db.IncludeRolesAdd();
            return Index();
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = _db.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            IdentityRole role = _db.GetRole(id);
            var details = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Adress,
                Email = user.Email,
                UserRole = role.Name
            };
            return View(details);
        }
        public ActionResult Update(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = _db.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            IdentityRole role = _db.GetRole(id);
            ViewBag.Name = _db.IncludeRoles(id);
            ViewBag.Role = role.Name;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _db.Update(user);
                await _db.GetManager().UpdateAsync(currentUser);

                if (user.UserRoles == RoleName.Admin || user.UserRoles == RoleName.Buyer)
                {
                    string role = user.UserRoles == RoleName.Admin ? RoleName.Buyer : RoleName.Admin;
                    await UserManager.RemoveFromRoleAsync(user.Id, role);
                    await UserManager.AddToRoleAsync(user.Id, user.UserRoles);
                }
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            _db.Delete(id);
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DoesUserNameExist(string userName)
        {
            return Json(!_db.DoesExist(userName), JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult DoesEmailExist(string Email)
        {
            return Json(!_db.DoesEmailExist(Email), JsonRequestBehavior.AllowGet);
        }

    }
}