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
        public IEnumerable<UserViewModel> GetAllBuyers()
        {
            var data = (from user in db.Users
                        from userRole in user.Roles
                        join role in db.Roles on userRole.RoleId equals role.Id
                        where role.Name =="Buyer"
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
        //string firstName = (from users in db.Users where users.UserName.Equals(User.Identity.Name) select users.FirstName).First();
        //public string ReturnName()
        //{
        //    return db.Users.Where(x => x.UserName == User).First();
        //}
        public SelectList IncludeRoles(string id)
        {
            var role = GetRole(id);
            return new SelectList(db.Roles.Where(m => !m.Name.Contains(role.Name)), "Name", "Name");
        }
        public SelectList IncludeRolesAdd()
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
        public bool DoesEmailExist(string email)
        {
            return db.Users.Any(x => x.Email == email);
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
        public void Delete(string id)
        {
            db.Users.Remove(GetById(id));
            db.SaveChanges();
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

