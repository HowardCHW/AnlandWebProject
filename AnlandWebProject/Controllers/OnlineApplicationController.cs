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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class OnlineApplicationController : BaseController
    {
        private IOnlineApplicationService _onlineApplicationService;
        private ISMTPSetupService _smptSetupService;

        #region All Views
        // GET: OnlineApplication
        public ActionResult AppCopy()
        {
            return View();
        }

        public ActionResult InfoService()
        {
            return View();
        }

        public ActionResult AppOnline()
        {
            return View();
        }

        public ActionResult AppBackup()
        {
            return View();
        }

        public ActionResult AppBackupApplication()
        {
            return View();
        }

        public ActionResult PriceInfo()
        {
            return View();
        }

        public ActionResult PriceInfoApplication()
        {
            return View();
        }

        public ActionResult EngEstate()
        {
            return View();
        }

        public ActionResult EngEstateApplication()
        {
            return View();
        }

        public ActionResult OnlineBooking()
        {
            return View();
        }

        public ActionResult OnlineBookingApplication()
        {
            return View();
        }

        public ActionResult Status()
        {
            return View();
        }
        #endregion

        #region Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AppBackupSave(OnlineApplicationModel saveModel)
        {
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = false;
            if (status)
            {
                using (_onlineApplicationService = new OnlineApplicationService())
                {
                    try
                    {
                        int newSaveID = 0;
                        saveModel.WDate = DateTime.Now;
                        saveStatus = _onlineApplicationService.CopySave(saveModel, out newSaveID);
                        if (newSaveID > 0)
                        {
                            Task.Factory.StartNew(() => SendMail("AppBackup", saveModel.ApplyName, saveModel.ApplyEMail, newSaveID));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PriceInfoSave(OnlineApplicationModel saveModel)
        {
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = false;
            if (status)
            {
                using (_onlineApplicationService = new OnlineApplicationService())
                {
                    try
                    {
                        int newSaveID = 0;
                        saveModel.WDate = DateTime.Now;
                        saveStatus = _onlineApplicationService.LandPriceSave(saveModel, out newSaveID);
                        if (newSaveID > 0)
                        {
                            Task.Factory.StartNew(() => SendMail("PriceInfo", saveModel.ApplyName, saveModel.ApplyEMail, newSaveID));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EngEstateSave(OnlineApplicationModel saveModel)
        {
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = false;
            if (status)
            {
                using (_onlineApplicationService = new OnlineApplicationService())
                {
                    try
                    {
                        int newSaveID = 0;
                        saveModel.WDate = DateTime.Now;
                        saveStatus = _onlineApplicationService.EngCopySave(saveModel, out newSaveID);
                        if (newSaveID > 0)
                        {
                            Task.Factory.StartNew(() => SendMail("EngEstate", saveModel.ApplyName, saveModel.ApplyEMail, newSaveID));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnlineBookingSave(OnlineApplicationModel saveModel)
        {
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = false;
            if (status)
            {
                using (_onlineApplicationService = new OnlineApplicationService())
                {
                    try
                    {
                        int newSaveID = 0;
                        saveModel.WDate = DateTime.Now;
                        saveStatus = _onlineApplicationService.OnlineAppSave(saveModel, out newSaveID);
                        if (newSaveID > 0)
                        {
                            Task.Factory.StartNew(() => SendMail("OnlineBooking", saveModel.ApplyName, saveModel.ApplyEMail, newSaveID));
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
        #endregion

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


        private void SendMail(string typeName, string userName, string userEmail, int newID)
        {
            string subject = "";
            string msgContent = "";

            string storagePath = ConfigurationManager.AppSettings["WebUrl"];

            switch (typeName)
            {
                case "AppBackup":
                    subject = "安樂地政事務所-檔案閱覽抄錄複製";
                    msgContent = $"{userName} 先生/小姐 您好：<br /><br />" +
                        "謝謝您的來信，我們會盡快處理您的申請案件，以下為您的申請書預覽連結<br /><br />" +
                       $"{storagePath}/Preview/AppBackup/{newID}<br /><br />" +
                        "安樂地政事務所";
                    break;
                case "PriceInfo":
                    subject = "安樂地政事務所-公告地價申請書";
                    msgContent = $"{userName} 先生/小姐 您好：<br /><br />" +
                        "謝謝您的來信，我們會盡快處理您的申請案件，以下為您的申請書預覽連結<br /><br />" +
                        $"{storagePath}/Preview/PriceInfo/{newID}<br /><br />" +
                        "安樂地政事務所";
                    break;
                case "EngEstate":
                    subject = "安樂地政事務所-英文謄本申請表";
                    msgContent = $"{userName} 先生/小姐 您好：<br /><br />" +
                        "謝謝您的來信，我們會盡快處理您的申請案件，以下為您的申請書預覽連結<br /><br />" +
                        $"{storagePath}/Preview/EngEstate/{newID}<br /><br />" +
                        "安樂地政事務所";
                    break;
                case "OnlineBooking":
                    subject = "安樂地政事務所-線上預約服務申請表";
                    msgContent = $"{userName} 先生/小姐 您好：<br /><br />" +
                        "謝謝您的來信，我們會盡快處理您的申請案件，以下為您的申請書預覽連結<br /><br />" +
                        $"{storagePath}/Preview/OnlineBooking/{newID}<br /><br />" +
                        "安樂地政事務所";
                    break;
                default:
                    break;
            }
            using (_smptSetupService = new SMTPSetupService())
            {
                SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                smtpData.SendMailBySMTP(userEmail, subject, msgContent, false);  //發MAIL給系統管理員並CC給填寫意見者
            }
        }
    }
}