using AnlandProject.Service;
using AnlandProject.Service.Interface;
using AnlandProject.Service.BusinessModel;
using System;
using System.Web.Mvc;
using AnlandProject.Backend.Filters;

namespace AnlandProject.Backend.Controllers.Settings
{
    [Authorize]
    public class SMTPSetupController : BaseController
    {
        private ISMTPSetupService _smptSetupService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/Settings/MailSetting/SMTPSetting.cshtml");
            //return View("~/Views/Settings/MailSetting/Index.cshtml");
        }

        [HttpPost]
        public ActionResult SMTPSetupQueryBuID(string type)
        {
            using (_smptSetupService = new SMTPSetupService())
            {
                SMTPSetupModel result = _smptSetupService.SMTPSetupQuery(type);
                return Json(result);
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult SMTPSetupSave(SMTPSetupModel model)
        {
            bool saveStatus = false;
            using (_smptSetupService = new SMTPSetupService())
            {
                try
                {
                    saveStatus = _smptSetupService.SMPTSetupSave(model);
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