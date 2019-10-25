using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class TicketPriorities
    {
        public int Id { get; set; }
        [Display(Name = "Ticket Priority")]
        public string PriorityName { get; set; }
        public string Description { get; set; }

        //Nav Section      
        

        public virtual ICollection<Ticket> Tickets { get; set; }
        

        public TicketPriorities()
        {
            Tickets = new HashSet<Ticket>();
            
        }
    }
}