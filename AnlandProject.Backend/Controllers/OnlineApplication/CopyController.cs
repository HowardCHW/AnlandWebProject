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
    public class CopyController : BaseController
    {
        private IOnlineApplicationService _copyService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/OnlineApplication/Copy/Index.cshtml");
        }        

        public ActionResult CopyView(int? id)
        {
            using (_copyService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _copyService.CopyQueryByID(id.Value);
                }

                return View("~/Views/OnlineApplication/Copy/CopyView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult CopyQuery()
        {
            using (_copyService = new OnlineApplicationService())
            {
                List<OnlineApplicationModel> result = _copyService.CopyQueryAll();
                return Json(new { data = result });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CopyDelete(int id)
        {
            bool delStatus = false;
            using (_copyService = new OnlineApplicationService())
            {
                try
                {
                    delStatus = _copyService.CopyDelete(id);
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
                Title = "刪除檔案閱覽抄錄複製",
                Content = @"您確定要刪除編號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Copy",
                ActionName = "CopyDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}