using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class ApplicationNotesController : BaseController
    {
        // GET: ApplicationNotes
        public ActionResult AppRegister()
        {
            return View();
        }

        public ActionResult AppMeasure()
        {
            return View();
        }

        public ActionResult AppLandPrice()
        {
            return View();
        }

        public ActionResult AppDuplicate()
        {
            return View();
        }

        public ActionResult AppLandData()
        {
            return View();
        }

        public ActionResult AppHandy()
        {
            return View();
        }

        public ActionResult AppDocument()
        {
            return View();
        }

        public ActionResult AppCorrection()
        {
            return View();
        }

        public ActionResult AppDownload(int? id)
        {
            string type = "";
            if (id.HasValue)
            {
                type = string.Format("correct{0}-tab", id);
            }
            ViewBag.TabType = type;
            return View();
        }
    }
}