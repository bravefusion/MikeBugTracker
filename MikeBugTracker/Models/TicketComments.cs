using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Models
{
    public class TicketComments
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public DateTime Created { get; set; }

        //Foreign Keys
        public string UserId { get; set; }
        public int TicketId { get; set; } 
       
        
        
        
        //Nav Section
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
        
    }
}