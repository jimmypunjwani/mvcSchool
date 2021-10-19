using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using mvcSchool.Models;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(mvcSchool.Startup))]
namespace mvcSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        //Adding Class to create Roles and Admin user:
        public void createRolesAndUsers()
        {
            //Include Application DB Context:
            var context = new ApplicationDbContext();

            //Initialize Objects for roleManager and userManager:
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            //Adding Admin Role if not exist:
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            //Adding Teacher Role if not exist:
            if (!roleManager.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                roleManager.Create(role);
            }

            //Adding Supervisor Role if not exist:
            if (!roleManager.RoleExists("Supervisor"))
            {
                var role = new IdentityRole();
                role.Name = "Supervisor";
                roleManager.Create(role);
            }

            //Adding Student Role if not exist:
            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }

            //Adding admin user and assign Role Admin:
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@school.com",
                BirthDate = DateTime.Now
            };

            var password = "password";

            //Creating admin user if not exist:
            var adminUsr = userManager.FindByName("admin");

            if (adminUsr == null)
            {
                //Create admin user:
                var usr = userManager.Create(user, password);
                //If user is created successfully then assign Admin role to it:
                if (usr.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

        }
    }
}
