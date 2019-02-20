namespace JustDoIt.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using JustDoIt.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<JustDoIt.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JustDoIt.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Put the seed function into a seed method
            AddUsers(context);
        }

        void AddUsers(JustDoIt.Models.ApplicationDbContext context)
        {
            // Add new user
            var user = new ApplicationUser { UserName = "user@email.com" };

            // User Manager
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            // Create user password
            um.Create(user, "password");
        }
    }
}
