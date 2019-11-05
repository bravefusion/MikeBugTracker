using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MikeBugTracker.Models;
    
namespace MikeBugTracker.Helpers
{
    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int SetDefaultTicketStatus()
        {
            return db.TicketStatus.FirstOrDefault(ts => ts.StatusName == "Open").Id;
        }

        public List<Ticket> ListMyTickets()
        {
            //Step 1: Detemine my role
            var myId = HttpContext.Current.User.Identity.GetUserId();
            List<Ticket> tickets = db.Tickets.Where(t => t.OwnerUserId == myId || t.AssignedToUserId == myId).ToList();
            return tickets;
        }

        public List<Ticket> ListMyTickets(string role)
        {
            var myId = HttpContext.Current.User.Identity.GetUserId();
            
            if (role == "Submitter")
            {
                List<Ticket> tickets = db.Tickets.Where(t => t.OwnerUserId == myId).ToList();
                return tickets;
            }

            if ( role == "Developer")
            {
                List<Ticket> tickets = db.Tickets.Where(t => t.AssignedToUserId == myId).ToList();
                return tickets;
            }
            return null;
        }

        public void AssignTicket(string userId, int ticketId)
        {
            Ticket ticket = db.Tickets.Find(ticketId);
            ticket.AssignedToUserId = userId;
            db.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}