using MikeBugTracker.Helpers;
using MikeBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MikeBugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        public ActionResult Index()
        {
            if (User.IsInRole("Admin,Demo_Admin"))
            {
                var aData = new IndexViewModel();
                aData.allTickets = db.Tickets.ToList();
                aData.myProjects = db.Projects.ToList();
                aData.Users = db.Users.ToList();
                return View(aData);
            }
            else if (User.IsInRole("Project Manager,Demo_Project Manager"))
            {
                var userId =  User.Identity.GetUserId();
                var myData = new IndexViewModel();
                myData.myProjects = db.Projects.Where(p => p.Name == userId ).ToList();
                return View(myData);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}