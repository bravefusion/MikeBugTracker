using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypesId { get; set; }
        public int TicketPrioritiesId { get; set; }
        public int TicketStatusId { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedToUserId { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        //Nav Section
        public virtual Project Project { get; set; }
        public virtual TicketTypes TicketTypes { get; set; }
        public virtual TicketPriorities TicketPriorities { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }

        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComments> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistory { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //Constructor Section
        public Ticket()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComments>();
            TicketHistory = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
        }



    }
}
