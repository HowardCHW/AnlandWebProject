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
using AnlandProject.Backend.Extensions;

namespace AnlandProject.Backend.Controllers.UnitItems
{
    [Authorize]
    public class DirectorFAQController : BaseController
    {
        private IDirectorFAQService _directorfaqService;
        private ISMTPSetupService _smptSetupService;

        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/DirectorFAQ/Index.cshtml");
        }

        public ActionResult CreateDirectorFAQ(int? id)
        {
            string subTitle = "新增";
            using (_directorfaqService = new DirectorFAQService())
            {
                FAQModel result = new FAQModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _directorfaqService.DirectorFAQQueryByNo(id.Value);
                    subTitle = "編輯";
                }

                ViewBag.Subtitle = subTitle;
                return View("~/Views/UnitItems/DirectorFAQ/DirectorFAQEdit.cshtml", result);
            }
        }

        public ActionResult DirectorFAQView(int? id)
        {
            using (_directorfaqService = new DirectorFAQService())
            {
                FAQModel result = new FAQModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _directorfaqService.DirectorFAQQueryByNo(id.Value);
                }
                
                return View("~/Views/UnitItems/DirectorFAQ/DirectorFAQView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult DirectorFAQQuery()
        {
            using (_directorfaqService = new DirectorFAQService())
            {
                List<FAQModel> result = _directorfaqService.DirectorFAQQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DirectorFAQSave(FAQModel model)
        {
            bool saveStatus = false;
            using (_directorfaqService = new DirectorFAQService())
            using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveStatus = _directorfaqService.DirectorFAQSave(model);
                    if (saveStatus && !string.IsNullOrWhiteSpace(model.MsgEmail))
                    {
                        SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");                        
                        smtpData.SendMailBySMTP(model.MsgEmail, model.RpyContent);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return Json(saveStatus);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DirectorFAQDelete(int id)
        {
            bool delStatus = false;
            using (_directorfaqService = new DirectorFAQService())
            {
                try
                {
                    delStatus = _directorfaqService.DirectorFAQDelete(id);
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
                Title = "刪除主任信箱",
                Content = @"您確定要刪除編號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "DirectorFAQ",
                ActionName = "DirectorFAQDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}