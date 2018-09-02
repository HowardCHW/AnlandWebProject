using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using AnlandProject.Web.Extensions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class OpinionExchangeController : BaseController
    {
        private IFAQService _faqService;
        private IDirectorFAQService _directorFAQService;
        private ISMTPSetupService _smptSetupService;

        // GET: OpinionExchange
        public ActionResult EmailSuggest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailSuggestQuery()
        {
            using (_faqService = new FAQService())
            {
                List<FAQModel> result = _faqService.FAQQueryAll();
                return Json(new { data = result.OrderByDescending(n => n.MsgDate) });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmailSuggestSave(FAQModel saveModel)
        {
            bool saveStatus = true;
            using (_faqService = new FAQService())
            using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveModel.MsgDate = DateTime.Now;
                    saveStatus = _faqService.FAQSave(saveModel);

                    if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.MsgEmail))
                    {
                        SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("email");
                        //目前沒有SMTP Server
                        //smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                        //smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                        //先用自己的WebMail當測試
                        smtpData.SendMailByWeb("hwchan67@gmail.com", saveModel.MsgContent);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(saveStatus);
        }

        public ActionResult SuggestDetail(int id)
        {
            using (_faqService = new FAQService())
            {
                FAQModel result = new FAQModel();
                if (id > 0)
                {
                    result = _faqService.FAQQueryByNo(id);
                }

                return View("~/Views/OpinionExchange/EmailSuggestDetail.cshtml", result);
            }            
        }

        public ActionResult SuggestDirector()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SuggestDirectorQuery()
        {
            using (_directorFAQService = new DirectorFAQService())
            {
                List<FAQModel> result = _directorFAQService.DirectorFAQQueryAll();
                return Json(new { data = result.OrderByDescending(n => n.MsgDate) });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SuggestDirectorSave(FAQModel saveModel)
        {
            bool saveStatus = true;
            using (_directorFAQService = new DirectorFAQService())
            using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveModel.MsgDate = DateTime.Now;
                    saveStatus = _directorFAQService.DirectorFAQSave(saveModel);

                    if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.MsgEmail))
                    {
                        SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                        //目前沒有SMTP Server
                        //smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                        //smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                        //先用自己的WebMail當測試
                        smtpData.SendMailByWeb("hwchan67@gmail.com", saveModel.MsgContent);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(saveStatus);
        }

        public ActionResult SuggestDRDetail(int id)
        {
            using (_directorFAQService = new DirectorFAQService())
            {
                FAQModel result = new FAQModel();
                if (id > 0)
                {
                    result = _directorFAQService.DirectorFAQQueryByNo(id);
                }

                return View("~/Views/OpinionExchange/SuggestDirectorDetail.cshtml", result);
            }
        }
    }
}