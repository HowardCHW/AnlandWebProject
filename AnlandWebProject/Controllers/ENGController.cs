using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using AnlandProject.Web.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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

        public ActionResult Sitemap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OpinionsSave(FAQModel saveModel)
        {
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = false;
            if (status)
            {
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
                            smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgSubject, saveModel.MsgContent);  //發MAIL給系統管理員並CC給填寫意見者
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }

            return Json(saveStatus);
        }

        #region Google reCaptcha Validation
        private bool ValidateCaptcha(string response)
        {
            //To Validate Google recaptcha
            string secValue = ConfigurationManager.AppSettings["recapKey"];
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secValue, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            return status;
        }
        #endregion
    }
}