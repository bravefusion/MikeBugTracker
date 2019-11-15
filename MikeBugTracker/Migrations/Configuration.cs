namespace MikeBugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MikeBugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

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

            if (!context.Roles.Any(r => r.Name == "Demo_Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo_Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Demo_Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo_Project Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Demo_Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo_Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Demo_Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo_Submitter" });
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
                    DisplayName = "Mike H",
                    
                }, WebConfigurationManager.AppSettings["AdminPassword"]);

            }

            if (!context.Users.Any(u => u.Email == "demomichaelhntn1990@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demomichaelhntn1990@gmail.com",
                    Email = "demomichaelhntn1990@gmail.com",
                    FirstName = "Michael",
                    LastName = "Hinton",
                    DisplayName = "Mike H",

                }, WebConfigurationManager.AppSettings["AdminPassword"]);

            }

            if (!context.Users.Any(u => u.Email == "JasonTwichell@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JasonTwichell@Mailinator.com",
                    Email = "JasonTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Jason T",

                }, WebConfigurationManager.AppSettings["PmPassword"]);

            }

            if (!context.Users.Any(u => u.Email == "jmhowse@ncat.edu"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jmhowse@ncat.edu",
                    Email = "jmhowse@ncat.edu",
                    FirstName = "Jordan",
                    LastName = "Howse",
                    DisplayName = "Jordan H",

                }, WebConfigurationManager.AppSettings["DPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "mjjackson@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "mjjackson@gmail.com",
                    Email = "mjjackson@gmail.com",
                    FirstName = "Michael",
                    LastName = "Jackson",
                    DisplayName = "Michael J",

                }, WebConfigurationManager.AppSettings["SubPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "rinkurusu@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "rinkurusu@gmail.com",
                    Email = "rinkurusu@gmail.com",
                    FirstName = "Rin",
                    LastName = "Kurusu",
                    DisplayName = "Rin K",

                }, WebConfigurationManager.AppSettings["NWDPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "sorahinton@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "sorahinton@gmail.com",
                    Email = "sorahinton@gmail.com",
                    FirstName = "Sora",
                    LastName = "Hinton",
                    DisplayName = "Sora H",

                }, WebConfigurationManager.AppSettings["NPMPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "lfiasco@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "lfiasco@gmail.com",
                    Email = "lfiasco@gmail.com",
                    FirstName = "Lupe",
                    LastName = "Fiasco",
                    DisplayName = "Lupe F",

                }, WebConfigurationManager.AppSettings["NDPassword"]);
            }
            
            if (!context.Users.Any(u => u.Email == "yuseifudo@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "yuseifudo@gmail.com",
                    Email = "yuseifudo@gmail.com",
                    FirstName = "Yusei",
                    LastName = "Fudo",
                    DisplayName = "Yusei F",

                }, WebConfigurationManager.AppSettings["NSubPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "jadenyuki@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jadenyuki@gmail.com",
                    Email = "jadenyuki@gmail.com",
                    FirstName = "Jaden",
                    LastName = "Yuki",
                    DisplayName = "Jaden Y",

                }, WebConfigurationManager.AppSettings["NWSubPassword"]);
            }


            #endregion

            #region Assign roles to Users
            var adminId = userManager.FindByEmail("michaelhntn1990@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var DemoadminId = userManager.FindByEmail("demomichaelhntn1990@gmail.com").Id;
            userManager.AddToRole(DemoadminId, "Demo_Admin");

            var projectManagerId = userManager.FindByEmail("JasonTwichell@Mailinator.com").Id;
            userManager.AddToRole(projectManagerId, "Project Manager");

            var DemoprojectManagerId = userManager.FindByEmail("sorahinton@gmail.com").Id;
            userManager.AddToRole(DemoprojectManagerId, "Demo_Project Manager");

            var developerId = userManager.FindByEmail("jmhowse@ncat.edu").Id;
            userManager.AddToRole(developerId, "Developer");

            developerId = userManager.FindByEmail("lfiasco@gmail.com").Id;
            userManager.AddToRole(developerId, "Developer");

            var DemodeveloperId = userManager.FindByEmail("yuseifudo@gmail.com").Id;
            userManager.AddToRole(DemodeveloperId, "Demo_Developer");

            var submitterId = userManager.FindByEmail("mjjackson@gmail.com").Id;
            userManager.AddToRole(submitterId, "Submitter");

            submitterId = userManager.FindByEmail("rinkurusu@gmail.com").Id;
            userManager.AddToRole(submitterId, "Submitter");

            var DemosubmitterId = userManager.FindByEmail("jadenyuki@gmail.com").Id;
            userManager.AddToRole(DemosubmitterId, "Demo_Submitter");
            #endregion

            //project seed
            context.Projects.AddOrUpdate(
                p => p.Name,
                new Project { Name = "Portfolio", Description = "Showing the portfolio" },
                new Project { Name = "Blog Project", Description = "Showcasing the blog" },
                new Project { Name = "Bug Tracker", Description = "The bug tracker" }
                );

            //Load up a few other tables..
            context.TicketStatus.AddOrUpdate(
                ts => ts.StatusName,
                new TicketStatus { StatusName = "Open", Description = "A newly created or unassigned Ticket" },
                new TicketStatus { StatusName = "Assigned", Description = "A Ticket that has been assigned and not worked on" },
                new TicketStatus { StatusName = "In Progress", Description = "A Ticket that has been assigned and is being worked on" },
                new TicketStatus { StatusName = "Resolved", Description = "A Ticket that has been completed" },
                new TicketStatus { StatusName = "Open", Description = "A newly created or unassigned Ticket" }
                );

            context.TicketPriorities.AddOrUpdate(
                tp => tp.PriorityName,
                new TicketPriorities { PriorityName = "Highest", Description = "This priority level requires completion within 1 days" },
                new TicketPriorities { PriorityName = "High", Description = "This priority level requires completion within 2 week" },
                new TicketPriorities { PriorityName = "Medium", Description = "This priority level requires completion within 3 week" },
                new TicketPriorities { PriorityName = "Low", Description = "This priority level requires completion within 4 weeks" }
                );

            context.TicketTypes.AddOrUpdate(
                ty => ty.TypeName,
                new TicketTypes { TypeName = "Defect", Description = "A defect in the software has been identified" },
                new TicketTypes { TypeName = "Feature Request", Description = "The client has called and requested a new feature be added" },
                new TicketTypes { TypeName = "Documentation Request", Description = "The client has called requesting documentation for a specific procedure" },
                new TicketTypes { TypeName = "Training Request", Description = "The client has called requesting training on the software" }
                );

            //save the above foreign key for the ticket seed

            context.SaveChanges();
            var projects = context.Projects;
            var priorities = context.TicketPriorities;
            var status = context.TicketStatus;
            var types = context.TicketTypes;

            //seeding a ticket
            context.Tickets.AddOrUpdate(
                t => t.Title,
                new Ticket
                {
                    ProjectId = projects.FirstOrDefault(p => p.Name == "Portfolio").Id,
                    TicketPrioritiesId = priorities.FirstOrDefault(tp => tp.PriorityName == "Low").Id,
                    TicketStatusId = status.FirstOrDefault(ts => ts.StatusName == "In Progress").Id,
                    TicketTypesId = types.FirstOrDefault(ty => ty.TypeName == "Feature Request").Id,
                    OwnerUserId = DemosubmitterId,
                    Title = "Needs Attention",
                    Created = DateTime.Now,
                    Description = "Need help adding this",
                    AssignedToUserId = DemodeveloperId
                }
                );
        }
    }
}
