using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SmartInItProjekat.Models;

[assembly: OwinStartupAttribute(typeof(SmartInItProjekat.Startup))]
namespace SmartInItProjekat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists(RoleName.Admin))
            {
                var role = new IdentityRole();
                role.Name = RoleName.Admin;
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "marko";
                user.Email = "bobic015@gmail.com";
                user.FirstName = "Marko";
                user.LastName = "Bobic";
                user.Adress = "Milosa Pocerca";
                string userPWD = "Marko.123";

                var chkUser = userManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                { var result1 = userManager.AddToRole(user.Id, RoleName.Admin); }
            }
            else
            {
                var role = new IdentityRole();
                role.Name = RoleName.Buyer;
                roleManager.Create(role);
            }
        }
    }

}

