namespace AccountSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AccountSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AccountSystem.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //  Initialize roles of identity
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var userRole = new IdentityRole("User");
            var adminRole = new IdentityRole("Admin");

            if (!roleManager.RoleExists(userRole.Name)) {
                roleManager.Create(userRole);
            }
            if (!roleManager.RoleExists(adminRole.Name))
            {
                roleManager.Create(adminRole);
            }
        }
    }
}
