using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartInItProjekat.Models;
using SmartInItProjekat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartInItProjekat.Repository
{
    public interface IUsers
    {
        IEnumerable<UserViewModel> GetAll();
        SelectList IncludeRoles();
        bool DoesExist(string userName);
        ApplicationUser Update(ApplicationUser user);
        ApplicationUser GetById(string id);
        IdentityRole GetRole(string id);
        IdentityUserRole GetUserRole(string id);
        UserManager<ApplicationUser> GetManager();
    }
}