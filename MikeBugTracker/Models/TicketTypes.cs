using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class TicketTypes
    {
        public int Id { get; set; }
        [Display(Name = "Type of Ticket")]
        public string TypeName { get; set; }
        public string Description { get; set; }

        
        //Nav Section
        public virtual ICollection<Ticket> Tickets { get; set; }


        public TicketTypes()
        {
            Tickets = new HashSet<Ticket>();

        }
    }
}