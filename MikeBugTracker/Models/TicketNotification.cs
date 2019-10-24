using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public string Body { get; set; }
        public bool Unread { get; set; }
        

        //Nav Section
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}