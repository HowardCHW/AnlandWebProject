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

namespace AnlandProject.Backend.Controllers.OnlineApplication
{
    [Authorize]
    public class LandPriceController : BaseController
    {
        private IOnlineApplicationService _LandPriceService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/OnlineApplication/LandPrice/Index.cshtml");
        }        

        public ActionResult LandPriceView(int? id)
        {
            using (_LandPriceService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _LandPriceService.LandPriceQueryByID(id.Value);
                }

                return View("~/Views/OnlineApplication/LandPrice/LandPriceView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult LandPriceQuery()
        {
            using (_LandPriceService = new OnlineApplicationService())
            {
                List<OnlineApplicationModel> result = _LandPriceService.LandPriceQueryAll();
                return Json(new { data = result });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult LandPriceDelete(int id)
        {
            bool delStatus = false;
            using (_LandPriceService = new OnlineApplicationService())
            {
                try
                {
                    delStatus = _LandPriceService.LandPriceDelete(id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(delStatus);
        }

        public ActionResult DeleteConfirmDialog()
        {
            //設定對話框
            DialogViewModel model = new DialogViewModel()
            {
                ID = "DeleteConfirmDialog",
                Title = "刪除公告地價資料",
                Content = @"您確定要刪除編號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "LandPrice",
                ActionName = "LandPriceDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}