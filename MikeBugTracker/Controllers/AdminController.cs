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
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        // GET: Admin
        public ActionResult ManageRoles()
        {
            ViewBag.Users = new MultiSelectList(db.Users,"Id","Email");
            ViewBag.Role = new SelectList(db.Roles,"Name","Name");

            var users = new List<ManageRolesViewModel>(); 
            foreach(var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}"
                });
            }

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> usersIds, string role)
        {
            //Uneneroll all the selected Users from ANY 
            //roles they may current occupy
            foreach(var userId in usersIds)
            {
                var userRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
                if(userRole != null)
                {
                    rolesHelper.RemoveUserFromRole(userId, userRole);
                }
            }

            //Step 2:Add them back to the selected Role
            if(! string.IsNullOrEmpty(role))
            {
                foreach (var userId in usersIds)
                {
                    rolesHelper.AddUserToRole(userId, role);
                }
            }
            

            return RedirectToAction("ManageRoles","Admin");
        }

        public ActionResult EditUser(string id)
        {
            var user = db.Users.Find(id);
            var adminModel = new AdminUser();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUserRoles(id);
            adminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            adminModel.User.Id = user.Id;
            adminModel.User.DisplayName = user.DisplayName;

            return View(adminModel);
        }

        public ActionResult EditUser(AdminUser model)
        {
            var user = db.Users.Find(model.User.Id);
            UserRolesHelper helper = new UserRolesHelper();
            foreach(var rolemv in db.Roles.Select(r=>r.Id).ToList())
            {
                helper.RemoveUserFromRole(user.Id, rolemv);
            }
            foreach(var roleadd in db.Roles.Select(r => r.Id).ToList())
            {
                helper.AddUserToRole(user.Id, roleadd);
            }
            return RedirectToAction("Index");
        }
    }
}