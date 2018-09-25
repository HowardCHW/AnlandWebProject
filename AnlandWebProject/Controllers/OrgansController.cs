﻿using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class OrgansController : BaseController
    {
        private IIntroService _introService;

        // GET: Organization
        public ActionResult Intro()
        {
            return View();
        }

        public ActionResult Precinct()
        {
            return View();
        }

        public ActionResult Organization()
        {
            return View();
        }

        public ActionResult FloorIntro()
        {
            return View();
        }

        public ActionResult ContactInfo()
        {
            return View();
        }

        public ActionResult GeoTraffic()
        {
            return View();
        }

        public ActionResult AdmRegulation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IntroQuery()
        {
            using (_introService = new IntroService())
            {
                List<DefaultDataModel> result = _introService.IntroQueryAll();
                return Json(new { data = result.OrderByDescending(r => r.PostDate) });
            }
        }

        public ActionResult RegulationView(int? id)
        {
            using (_introService = new IntroService())
            {
                DefaultDataModel result = new DefaultDataModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _introService.IntroQueryByID(id.Value);
                }

                return View("~/Views/Organs/RegulationDetail.cshtml", result);
            }
        }

    }
}