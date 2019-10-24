using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class TicketStatus
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        public string Description { get; set; }        

        //Nav Section
        public virtual ICollection<Ticket> Tickets { get; set; }


        public TicketStatus()
        {
            Tickets = new HashSet<Ticket>();

        }
    }
}