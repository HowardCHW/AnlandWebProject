using AnlandProject.Service;
using AnlandProject.Service.Interface;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AnlandProject.Backend.Filters;
using AnlandProject.Backend.Models;

namespace AnlandProject.Backend.Controllers.Settings
{
    [Authorize]
    public class NoOfVisitorsController : BaseController
    {
        private ICommonService _commonService;
        
        // GET: News
        public ActionResult Index()
        {
            using (_commonService = new CommonService())
            {
                ViewBag.NoOfVisitors = _commonService.VisitorsQuery();
            }                
            return View("~/Views/Settings/NoOfVisitors/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult NoOfVisitorsSave(int updateNo)
        {
            bool saveStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    saveStatus = _commonService.VisitorsUpdate(updateNo);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return Json(saveStatus);
        }
    }
}