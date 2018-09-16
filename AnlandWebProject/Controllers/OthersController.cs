using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class OthersController : Controller
    {
        // GET: Others
        public ActionResult LandUnit()
        {
            return View();
        }

        public ActionResult LandService()
        {
            return View();
        }

        public ActionResult TaxUnit()
        {
            return View();
        }

    }
}