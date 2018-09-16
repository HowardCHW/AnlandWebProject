using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class OnlineSearchController : BaseController
    {
        // GET: Online
        public ActionResult OInheritance()
        {
            return View("~/Views/OnlineSearch/OverInherited.cshtml");
        }

        public ActionResult InheritanceNote()
        {
            return View("~/Views/OnlineSearch/OINote.cshtml");
        }
        public ActionResult InheritanceRights()
        {
            return View("~/Views/OnlineSearch/OIRights.cshtml");
        }

        public ActionResult InheritanceLand()
        {
            return View("~/Views/OnlineSearch/InheritanceLand.cshtml");
        }
    }
}