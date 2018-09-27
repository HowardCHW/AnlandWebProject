using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using AnlandProject.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class ENGController : BaseController
    {
        private IDirectorFAQService _directorFAQService;
        private ISMTPSetupService _smptSetupService;

        // GET: ENG
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Introduction()
        {
            return View();
        }

        public ActionResult Organization()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Transportation()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Opinions()
        {
            return View();
        }

        public ActionResult Applications()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OpinionsSave(FAQModel saveModel)
        {
            bool saveStatus = false;
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
                        smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                        smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                    }
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