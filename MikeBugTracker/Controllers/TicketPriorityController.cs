using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MikeBugTracker.Models;

namespace MikeBugTracker.Controllers
{
    public class TicketPriorityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketPriority
        public ActionResult Index()
        {
            return View(db.TicketPriorities.ToList());
        }

        // GET: TicketPriority/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // GET: TicketPriority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketPriority/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PriorityName,Description")] TicketPriorities ticketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.TicketPriorities.Add(ticketPriorities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketPriorities);
        }

        // GET: TicketPriority/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // POST: TicketPriority/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PriorityName,Description")] TicketPriorities ticketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketPriorities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketPriorities);
        }

        // GET: TicketPriority/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // POST: TicketPriority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            db.TicketPriorities.Remove(ticketPriorities);
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
