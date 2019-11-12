using Microsoft.AspNet.Identity;
using MikeBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void RecordHistoricalChanges(Ticket oldTicket, Ticket newTicket)
        {
            if(oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketStatusId",
                    TicketId = oldTicket.Id,
                    OldValue = oldTicket.TicketStatus.StatusName,
                    NewValue = newTicket.TicketStatus.StatusName,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);

            }

            if(oldTicket.TicketPrioritiesId != newTicket.TicketPrioritiesId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketPriorityId",
                    TicketId = oldTicket.Id,
                    OldValue = oldTicket.TicketPriorities.PriorityName,
                    NewValue = newTicket.TicketPriorities.PriorityName,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.OwnerUserId != newTicket.OwnerUserId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "DeveloperId",
                    TicketId = oldTicket.Id,
                    OldValue = oldTicket.OwnerUserId == null ? "UnAssigned" : oldTicket.OwnerUserId,
                    NewValue = newTicket.OwnerUserId == null ? "UnAssigned" : oldTicket.OwnerUserId,
                    Changed = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);
            }
            db.SaveChanges();
        }
    }
}