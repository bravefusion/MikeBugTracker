﻿using Microsoft.AspNet.Identity;
using MikeBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeBugTracker.Helpers
{
    public class NotificationsHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUser != null;
            var ticketHasBeenUnassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            var ticketHasBeenReassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;

            if (ticketHasBeenAssigned)
            {
                AddAssignmentNotification(oldTicket, newTicket);
            }
            else if (ticketHasBeenUnassigned)
            {
                AddUnassignmentNotification(oldTicket, newTicket);
            }
            else if (ticketHasBeenReassigned)
            {
                AddAssignmentNotification(oldTicket, newTicket);
                AddUnassignmentNotification(oldTicket, newTicket);
            }
        }

        private void AddAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                Unread = false,
                RecipientId = newTicket.AssignedToUserId,
                Body = $"You have been assigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}."
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        private void AddUnassignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                Unread = false,
                RecipientId = newTicket.AssignedToUserId,
                Body = $"You have been assigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}."
            };
            db.TicketNotifications.Remove(notification);
            db.SaveChanges();

        }

        public static List<TicketNotification> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Include("Sender").Include("Recipient").Where(t => t.RecipientId == currentUserId && !t.Unread).ToList();
        }
    }
}