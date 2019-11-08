using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MikeBugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   public class ApplicationUser: IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public string FullName 
        {
            get
            {
                return $"{FirstName},{LastName}";
            } 
        }
        public virtual ICollection<TicketComments> TicketComments { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistory { get; set; }
        public virtual ICollection<TicketNotification> TicketNotification { get; set; }

        public ApplicationUser()
        {
            TicketComments = new HashSet<TicketComments>();
            Projects = new HashSet<Project>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketHistory = new HashSet<TicketHistory>();
            TicketNotification = new HashSet<TicketNotification>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


    }    
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.Ticket> Tickets { get; set; }

        //public System.Data.Entity.DbSet<MikeBugTracker.Models.ApplicationUser> ApplicationUsers { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketPriorities> TicketPriorities { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketStatus> TicketStatus { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketTypes> TicketTypes { get; set; }

        //public System.Data.Entity.DbSet<MikeBugTracker.Models.ApplicationUser> ApplicationUsers { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketAttachment> TicketAttachments { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketComments> TicketComments { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketHistory> TicketHistories { get; set; }

        public System.Data.Entity.DbSet<MikeBugTracker.Models.TicketNotification> TicketNotifications { get; set; }
    }
}