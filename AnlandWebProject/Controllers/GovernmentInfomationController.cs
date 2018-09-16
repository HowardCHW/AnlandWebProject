using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class GovernmentInfomationController : Controller
    {
        // GET: GovernmentInfomation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Budget(int id)
        {
            string htmlName = string.Format("budget{0}.cshtml", id);
            return View("~/Views/GovernmentInfomation/Budget/" + htmlName);
        }

        public ActionResult MonthlyReport()
        {
            return View();
        }

        public ActionResult Statistical()
        {
            return View();
        }

        public ActionResult Training()
        {
            return View();
        }

        public ActionResult CreateReport()
        {
            return View();
        }
    }
}