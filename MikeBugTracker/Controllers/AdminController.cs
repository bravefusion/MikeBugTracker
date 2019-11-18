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
        private ProjectsHelper projHelper = new ProjectsHelper();
        // GET: Admin
        public ActionResult ManageRoles()
        {
            ViewBag.UsersIds = new MultiSelectList(db.Users, "Id", "FullName");
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");

            var users = new List<ManageRolesViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}",
                    RoleName = rolesHelper.ListUserRoles(user.Id).FirstOrDefault()
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
            foreach (var userId in usersIds)
            {


                var userRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
                if (userRole != null)
                {
                    rolesHelper.RemoveUserFromRole(userId, userRole);
                }
            }

            //Step 2:Add them back to the selected Role
            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in usersIds)
                {
                    rolesHelper.AddUserToRole(userId, role);
                }
            }


            return RedirectToAction("ManageRoles", "Admin");
        }

        
        public ActionResult ManageProjectUsers()
        {
            ViewBag.Projects = new MultiSelectList(db.Projects, "Id", "Name");
            ViewBag.Developers = new MultiSelectList(rolesHelper.UsersInRole("Developer").Union(rolesHelper.UsersInRole("Demo_Developer")), "Id", "FullName");
            ViewBag.Submitters = new MultiSelectList(rolesHelper.UsersInRole("Submitter").Union(rolesHelper.UsersInRole("Demo_Project Manager")), "Id", "FullName");

            if (User.IsInRole("Admin") || User.IsInRole("Demo_Admin"))
            {
                ViewBag.ProjectManager = new SelectList(rolesHelper.UsersInRole("Project Manager").Union(rolesHelper.UsersInRole("Demo_Project Manager")), "Id", "FullName");
            }
            //Lets create a View Model for purposes of displaying User's and their associated Projects
            var myData = new List<UserProjectListViewModel>();
            UserProjectListViewModel userVm = null;
            foreach (var user in db.Users.ToList())
            {
                userVm = new UserProjectListViewModel
                {
                    Name = $"{user.LastName}, {user.FirstName}",
                    ProjectNames = projHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                };

                if (userVm.ProjectNames.Count() == 0)
                    userVm.ProjectNames.Add("N/A");

                myData.Add(userVm);
            };

            return View(myData);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(List<int> projects, string projectManagerId, List<string> developers, List<string> submitters)
        {
            //Remove users from every project I have selected
            if(projects !=null)
            {
                foreach (var projectId in projects)
                {
                    //Remove everyone from This project
                    foreach(var user in projHelper.UsersOnProject(projectId).ToList())
                    {
                        projHelper.RemoveUserFromProject(user.Id, projectId);
                    }

                    //Add back a Pm if I can
                    if(!string.IsNullOrEmpty(projectManagerId))
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

                }

            }
            return RedirectToAction("ManageProjectUsers");
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

        [Authorize]
        public ActionResult index()
        {
            return View();
        }


    }
}