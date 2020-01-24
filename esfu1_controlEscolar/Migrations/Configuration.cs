namespace esfu1_controlEscolar.Migrations
{
    using esfu1_controlEscolar.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<esfu1_controlEscolar.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
            ContextKey = "esfu1_controlEscolar.Models.ApplicationDbContext";
        }

        protected override void Seed(esfu1_controlEscolar.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "Prefecto"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Prefecto" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "TrabajoSocial"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "TrabajoSocial" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Alumno"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Alumno" };

                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "admin_esfu"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin_esfu", Email = "esfu.uno@gmail.com" };

                manager.Create(user, "educacion.1010");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
