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
    public class FAQController : BaseController
    {
        private IFAQService _faqService;
        private ISMTPSetupService _smptSetupService;

        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/FAQ/Index.cshtml");
        }

        public ActionResult CreateFAQ(int? id)
        {
            string subTitle = "新增";
            using (_faqService = new FAQService())
            {
                FAQModel result = new FAQModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _faqService.FAQQueryByNo(id.Value);
                    subTitle = "編輯";
                }

                ViewBag.Subtitle = subTitle;
                return View("~/Views/UnitItems/FAQ/FAQEdit.cshtml", result);
            }
        }

        public ActionResult FAQView(int? id)
        {
            using (_faqService = new FAQService())
            {
                FAQModel result = new FAQModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _faqService.FAQQueryByNo(id.Value);
                }
                
                return View("~/Views/UnitItems/FAQ/FAQView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult FAQQuery()
        {
            using (_faqService = new FAQService())
            {
                List<FAQModel> result = _faqService.FAQQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult FAQSave(FAQModel model)
        {
            bool saveStatus = false;
            using (_faqService = new FAQService())
            using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveStatus = _faqService.FAQSave(model);
                    if (saveStatus && !string.IsNullOrWhiteSpace(model.MsgEmail))
                    {
                        SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("email");
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
        public JsonResult FAQDelete(int id)
        {
            bool delStatus = false;
            using (_faqService = new FAQService())
            {
                try
                {
                    delStatus = _faqService.FAQDelete(id);
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
                Title = "刪除地政答客問",
                Content = @"您確定要刪除編號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "FAQ",
                ActionName = "FAQDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}