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
    public class OnlineApplicationController : BaseController
    {
        private IOnlineApplicationService _onlineApplicationService;
        //private ISMTPSetupService _smptSetupService;

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
            bool saveStatus = false;
            using (_onlineApplicationService = new OnlineApplicationService())
            //using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveModel.WDate = DateTime.Now;
                    saveStatus = _onlineApplicationService.CopySave(saveModel);

                    //if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.ApplyEMail))
                    //{
                    //    SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                    //    //目前沒有SMTP Server
                    //    //smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                    //    //smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                    //    //先用自己的WebMail當測試
                    //    smtpData.SendMailByWeb("hwchan67@gmail.com", saveModel.ProposalContent);
                    //}
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
        public JsonResult PriceInfoSave(OnlineApplicationModel saveModel)
        {
            bool saveStatus = false;
            using (_onlineApplicationService = new OnlineApplicationService())
            //using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveModel.WDate = DateTime.Now;
                    saveStatus = _onlineApplicationService.LandPriceSave(saveModel);

                    //if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.ApplyEMail))
                    //{
                    //    SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                    //    //目前沒有SMTP Server
                    //    //smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                    //    //smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                    //    //先用自己的WebMail當測試
                    //    smtpData.SendMailByWeb("hwchan67@gmail.com", saveModel.ProposalContent);
                    //}
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
        public JsonResult EngEstateSave(OnlineApplicationModel saveModel)
        {
            bool saveStatus = false;
            using (_onlineApplicationService = new OnlineApplicationService())
            //using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveModel.WDate = DateTime.Now;
                    saveStatus = _onlineApplicationService.EngCopySave(saveModel);

                    //if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.ApplyEMail))
                    //{
                    //    SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                    //    //目前沒有SMTP Server
                    //    //smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                    //    //smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                    //    //先用自己的WebMail當測試
                    //    smtpData.SendMailByWeb("hwchan67@gmail.com", saveModel.ProposalContent);
                    //}
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
        public JsonResult OnlineBookingSave(OnlineApplicationModel saveModel)
        {
            bool saveStatus = false;
            using (_onlineApplicationService = new OnlineApplicationService())
            //using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveModel.WDate = DateTime.Now;
                    saveStatus = _onlineApplicationService.OnlineAppSave(saveModel);

                    //if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.ApplyEMail))
                    //{
                    //    SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                    //    //目前沒有SMTP Server
                    //    //smtpData.SendMailBySMTP(saveModel.MsgEmail, saveModel.MsgContent);  //發MAIL給填寫意見者
                    //    //smtpData.SendMailBySMTP(smtpData.Recipient, saveModel.MsgContent);   //發MAIL給系統管理員
                    //    //先用自己的WebMail當測試
                    //    smtpData.SendMailByWeb("hwchan67@gmail.com", saveModel.ProposalContent);
                    //}
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(saveStatus);
        }
        #endregion

    }
}