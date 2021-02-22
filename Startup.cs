using Microsoft.Owin;
using Owin;
using ProiectDaw.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(ProiectDaw.Startup))]
namespace ProiectDaw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CreateAdminUserAndApplicationRoles();
        }

        private void CreateAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole { Name = "Admin" });

            if (null == userManager.FindByName("admin@gmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                var adminCreated = userManager.Create(user, "!1Admin");
                if (adminCreated.Succeeded)
                    userManager.AddToRole(user.Id, "Admin");
            }

            if (!roleManager.RoleExists("Colaborator"))
                roleManager.Create(new IdentityRole { Name = "Colaborator" });

            if (!roleManager.RoleExists("User"))
                roleManager.Create(new IdentityRole { Name = "User" });

        }
    }
}
