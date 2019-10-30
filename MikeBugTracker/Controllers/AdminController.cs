using MikeBugTracker.Helpers;
using MikeBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeBugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult ManageRoles()
        {
            ViewBag.Users = new MultiSelectList(db.Users,"Id","Email");
            ViewBag.Role = new SelectList(db.Roles,"Name","Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> users, string role)
        {
            return RedirectToAction("Index","Home");
        }

        public ActionResult EditUser(string id)
        {
            var user = db.Users.Find(id);
            AdminUser AdminModel = new AdminUser();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUserRoles(id);
            AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            AdminModel.Id = user.Id;
            AdminModel.Name = user.DisplayName;

            return View(AdminModel);
        }

        public ActionResult EditUser(AdminUser model)
        {
            var user = db.Users.Find(model.id);
            UserRolesHelper helper = new UserRolesHelper();
            foreach(var rolemv in db.Roles.Select(r=>r.Id).ToList())
            {
                helper.RemoveUserFromRole(user.Id, rolermv);
            }
            foreach(var roleadd in db.Roles.Select(r => r.Id).ToList())
            {
                helper.AddUserToRole(user.Id, roleadd);
            }
            return RedirectToAction("Index");
        }
    }
}