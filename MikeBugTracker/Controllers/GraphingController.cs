using MikeBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikeBugTracker.Controllers
{
    public class GraphingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Graphing
        public JsonResult ProduceChart1Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach (var priority in db.TicketPriorities.ToList())
            {
                data = new MorrisBarData();
                data.label = priority.PriorityName;
                data.value = db.Tickets.Where(t => t.TicketPriorities.PriorityName == priority.PriorityName).Count();
                myData.Add(data);
            }

            return Json(myData);
        }

        public JsonResult ProduceChart2Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach (var status in db.TicketStatus.ToList())
            {
                myData.Add(new MorrisBarData
                {
                    label = status.StatusName,
                    value = db.Tickets.Where(t => t.TicketStatus.StatusName == status.StatusName).Count()
                });
            }
            return Json(myData);
        }

    }
}