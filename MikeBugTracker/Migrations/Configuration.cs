namespace MikeBugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MikeBugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MikeBugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MikeBugTracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region Role creation
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            #endregion
            #region User creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "michaelhntn1990@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "michaelhntn1990@gmail.com",
                    Email = "michaelhntn1990@gmail.com",
                    FirstName = "Michael",
                    LastName = "Hinton",
                    DisplayName = "MikeyH",
                    
                }, "November8");

            }

            if (!context.Users.Any(u => u.Email == "JasonTwichell@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JasonTwichell@Mailinator.com",
                    Email = "JasonTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Teach",

                }, "Abcand123");

            }

            if (!context.Users.Any(u => u.Email == "jmhowse@ncat.edu"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jmhowse@ncat.edu",
                    Email = "jmhowse@ncat.edu",
                    FirstName = "Jordan",
                    LastName = "Howse",
                    DisplayName = "Jhowse25",

                }, "January8*");
            }

            if (!context.Users.Any(u => u.Email == "mjjackson@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "mjjackson@gmail.com",
                    Email = "mjjackson@gmail.com",
                    FirstName = "Michael",
                    LastName = "Jackson",
                    DisplayName = "KingOfPop",

                }, "Whosbad87");
            }


            #endregion

            #region Assign roles to Users
            var adminId = userManager.FindByEmail("michaelhntn1990@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var projectManagerId = userManager.FindByEmail("JasonTwichell@Mailinator.com").Id;
            userManager.AddToRole(projectManagerId, "Project Manager");

            var developerId = userManager.FindByEmail("jmhowse@ncat.edu").Id;
            userManager.AddToRole(developerId, "Developer");

            var submitterId = userManager.FindByEmail("mjjackson@gmail.com").Id;
            userManager.AddToRole(submitterId, "Submitter");

            #endregion

        }
    }
}
