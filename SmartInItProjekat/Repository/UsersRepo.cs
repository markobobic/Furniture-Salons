using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SmartInItProjekat.Models;
using SmartInItProjekat.ViewModels;
using System.Net;



namespace SmartInItProjekat.Repository
{
    public class UsersRepo:IUsers,IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public IEnumerable<UserViewModel> GetAll()
        {
            var data = (from user in db.Users
                          from userRole in user.Roles
                          join role in db.Roles on userRole.RoleId equals role.Id
                          select new UserViewModel()
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              Address = user.Adress,
                              Email = user.Email,
                              UserRole = role.Name
                          }).ToList();

            return data;
        }

        public SelectList IncludeRoles()
        {
            return new SelectList(db.Roles.ToList(), "Name", "Name");
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public bool DoesExist(string userName)
        {
            return db.Users.Any(x => x.UserName == userName);
        }
      public  ApplicationUser Update(ApplicationUser user)
         {
            var currentUser = GetManager().FindById(user.Id);
            currentUser.FirstName = user.FirstName;
            currentUser.Email = user.Email;
            currentUser.LastName = user.LastName;
            currentUser.Adress = user.Adress;
            return currentUser;
        }
        public UserManager<ApplicationUser> GetManager()
        {
            var store = new UserStore<ApplicationUser>(db);
            var manager = new UserManager<ApplicationUser>(store);
            return manager;
        }
      public ApplicationUser GetById(string id)
        {
            return db.Users.Find(id);
            
        }
        public IdentityUserRole GetUserRole(string id)
        {
            var user = GetById(id);
            IdentityUserRole userRole = user.Roles.Where(x => x.UserId ==id).FirstOrDefault();
            return userRole;
        }
        public IdentityRole GetRole(string id)
        {
            var userRole = GetUserRole(id);
            return db.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefault();
        }
    }
}

