using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartInItProjekat.Models;
using SmartInItProjekat.ViewModels;

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
    }
}

