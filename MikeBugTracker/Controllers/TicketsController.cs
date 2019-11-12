using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MikeBugTracker.Helpers;
using MikeBugTracker.Models;

namespace MikeBugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TicketHistoryHelper historyHelper = new TicketHistoryHelper();

        // GET: Tickets
        public ActionResult Index()
        {

            var tickets = db.Tickets;
            var userId = User.Identity.GetUserId();

            if (User.IsInRole("Developer"))
            {
                var devTickets = tickets.Where(t => t.AssignedToUserId == userId);
                return View(devTickets.ToList());
            }
            else if (User.IsInRole("Submitter"))
            {
                var subTickets = tickets.Where(t => t.OwnerUserId == userId);
                return View(subTickets.ToList());
            }
            
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }
        [Authorize(Roles = "Submitter")]
        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FullName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FullName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPrioritiesId = new SelectList(db.TicketPriorities, "Id", "PriorityName");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "StatusName");
            ViewBag.TicketTypesId = new SelectList(db.TicketTypes, "Id", "TypeName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,TicketTypesId,TicketPrioritiesId,TicketStatusId,Title,Description,Created,Updated")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Created = DateTime.Now;
                ticket.OwnerUserId = User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPrioritiesId = new SelectList(db.TicketPriorities, "Id", "PriorityName", ticket.TicketPrioritiesId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "StatusName", ticket.TicketStatusId);
            ViewBag.TicketTypesId = new SelectList(db.TicketTypes, "Id", "TypeName", ticket.TicketTypesId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPrioritiesId = new SelectList(db.TicketPriorities, "Id", "PriorityName", ticket.TicketPrioritiesId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "StatusName", ticket.TicketStatusId);
            ViewBag.TicketTypesId = new SelectList(db.TicketTypes, "Id", "TypeName", ticket.TicketTypesId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypesId,TicketPrioritiesId,TicketStatusId,AssignedToUserId,Title,Description,Created")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {

                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);

                ticket.Updated = DateTime.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                var newTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                historyHelper.RecordHistoricalChanges(oldTicket, newTicket);

                return RedirectToAction("Index");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPrioritiesId = new SelectList(db.TicketPriorities, "Id", "UserId", ticket.TicketPrioritiesId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "UserId", ticket.TicketStatusId);
            ViewBag.TicketTypesId = new SelectList(db.TicketTypes, "Id", "UserId", ticket.TicketTypesId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
