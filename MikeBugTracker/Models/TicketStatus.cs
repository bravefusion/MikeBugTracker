using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class TicketStatus
    {
        public int Id { get; set; }
        [Display(Name = "Ticket Status")]
        public string StatusName { get; set; }
        
        public string Description { get; set; }        

        //Nav Section
        public virtual ICollection<Ticket> Tickets { get; set; }


        public TicketStatus()
        {
            Tickets = new HashSet<Ticket>();

        }
    }
}