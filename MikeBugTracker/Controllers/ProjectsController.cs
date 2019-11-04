using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MikeBugTracker.Helpers;
using MikeBugTracker.Models;

namespace MikeBugTracker.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        private ProjectsHelper projHelper = new ProjectsHelper();

        public ActionResult ManageUsers(int id)
        {
            ViewBag.ProjectId = id;

            #region PM section
            var pmId = projHelper.ListUsersOnProjectInRole(id, "Project_Manager").FirstOrDefault();
            ViewBag.ProjectManagerId = new SelectList(rolesHelper.UsersInRole("Project_Manager"), "Id", "Email", pmId);
            #endregion

            #region Dev Section
            ViewBag.Developers = new MultiSelectList(rolesHelper.UsersInRole("Developer"), "Id", "Email", projHelper.ListUsersOnProjectInRole(id, "Developer"));
            #endregion

            #region Sub Section
            ViewBag.Submitters = new MultiSelectList(rolesHelper.UsersInRole("Submitter"), "Id", "Email", projHelper.ListUsersOnProjectInRole(id, "Submitter"));
            #endregion

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUsers(int projectId, string projectManagerId, List<string> developers, List<string> submitters)
        {
            foreach(var user in projHelper.UsersOnProject(projectId).ToList())
            {
                projHelper.RemoveUserFromProject(user.Id, projectId);
            }

            //In order to ensure that I always have the correct and current set of assign users
            //I will first remove all users from the project and then I will add back the selected users
            if (!string.IsNullOrEmpty(projectManagerId))
            {
                projHelper.AddUserToProject(projectManagerId, projectId);
            }

            if (developers != null)
            {
                foreach (var developerId in developers)
                {
                    projHelper.AddUserToProject(developerId, projectId);
                }
            }

            if (submitters != null)
            {
                foreach (var submitterId in submitters)
                {
                    projHelper.AddUserToProject(submitterId, projectId);
                }
            }
            return RedirectToAction("ManageUsers", new { id = projectId });
        }


        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
