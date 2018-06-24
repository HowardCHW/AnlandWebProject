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
    public class TranscriptController : BaseController
    {
        private IOnlineApplicationService _transcriptService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/OnlineApplication/Transcript/Index.cshtml");
        }        

        public ActionResult TranscriptView(int? id)
        {
            using (_transcriptService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _transcriptService.EngCopyQueryByID(id.Value);
                }

                return View("~/Views/OnlineApplication/Transcript/TranscriptView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult TranscriptQuery()
        {
            using (_transcriptService = new OnlineApplicationService())
            {
                List<OnlineApplicationModel> result = _transcriptService.EngCopyQueryAll();
                return Json(new { data = result });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult TranscriptDelete(int id)
        {
            bool delStatus = false;
            using (_transcriptService = new OnlineApplicationService())
            {
                try
                {
                    delStatus = _transcriptService.EngCopyDelete(id);
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
                Title = "刪除網路申請英文謄本",
                Content = @"您確定要刪除編號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Transcript",
                ActionName = "TranscriptDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}