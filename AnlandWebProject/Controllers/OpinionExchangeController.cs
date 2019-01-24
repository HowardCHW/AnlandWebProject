using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using AnlandProject.Web.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class OpinionExchangeController : BaseController
    {
        private IFAQService _faqService;
        private IDirectorFAQService _directorFAQService;
        private IQuestionnaireService _questionnaireService;
        private IProposalService _proposalService;
        private ISMTPSetupService _smptSetupService;
        
        #region 地政答客問
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
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = true;
            if (status)
            {
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
        #endregion
        
        #region 主任信箱
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
                return Json(new { data = result.Where(n => n.Attach3 != "否").OrderByDescending(n => n.MsgDate) });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SuggestDirectorSave(FAQModel saveModel)
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
        #endregion

        #region 網路投票
        public ActionResult Questionnaire()
        {
            using(_questionnaireService = new QuestionnaireService())
            {
                List<QuestionnaireDataModel> temp = _questionnaireService.QuestionnaireQueryAll();
                List<QuestionnaireDataModel> results = temp.Where(q => q.QuestionStatus == "問卷調查進行中").OrderByDescending(q => q.QuestionID).ToList();
                return View(results);
            }            
        }

        public ActionResult QuestionnaireDetail(int id)
        {
            using (_questionnaireService = new QuestionnaireService())
            {
                QuestionnaireDataModel result = new QuestionnaireDataModel();
                if (id > 0)
                {
                    result = _questionnaireService.QuestionnaireQueryByID(id);
                }
                ViewBag.QuestionData = JsonConvert.SerializeObject(result);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult QuestionnaireAnswerSave(QuestionnaireAnswerModel saveModel)
        {
            bool saveStatus = false;
            using (_questionnaireService = new QuestionnaireService())
            {
                try
                {
                    string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                    }
                    saveModel.FillInDate = DateTime.Now;
                    saveModel.SenderIP = ipAddress;
                    List<string> allAnswer = new List<string>();
                    if (saveModel.SingleOpt != null && saveModel.SingleOpt.Count > 0)
                    {
                        allAnswer.AddRange(saveModel.SingleOpt);
                    }
                    if (saveModel.MultipleOpt != null && saveModel.MultipleOpt.Count > 0)
                    {
                        for (int i = 0; i < saveModel.MultipleOpt.Count; i++)
                        {
                            if (saveModel.MultipleOpt[i].Count > 0)
                            {
                                allAnswer.Add(string.Join(",", saveModel.MultipleOpt[i]));
                            }                            
                        }
                    }
                    if (saveModel.QandA != null && saveModel.QandA.Count > 0)
                    {
                        allAnswer.AddRange(saveModel.QandA);
                    }

                    saveModel.AllAnswers = string.Join("||", allAnswer);

                    saveStatus = _questionnaireService.QuestionnaireAnswerSave(saveModel);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(saveStatus);
        }

        public ActionResult QuestionnaireAnalyze(int id)
        {
            using (_questionnaireService = new QuestionnaireService())
            {
                QuestionnaireDataModel result = new QuestionnaireDataModel();
                if (id > 0)
                {
                    result = _questionnaireService.QuestionnaireAnswerByID(id);
                }

                ViewBag.AnalyzeViewData = JsonConvert.SerializeObject(result);
                return View();
            }
        }
        #endregion

        #region 民眾參與創新提案
        public ActionResult InnovationProposal()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProposalDataSave(ProposalDataModel saveModel)
        {
            //驗證 Google reCaptcha
            var response = Request["g-recaptcha-response"];
            var status = ValidateCaptcha(response);

            bool saveStatus = false;
            if (status)
            {
                using (_proposalService = new ProposalService())
                using (_smptSetupService = new SMTPSetupService())
                {
                    try
                    {
                        saveModel.ProposalDate = DateTime.Now;
                        saveStatus = _proposalService.ProposalSave(saveModel);

                        if (saveStatus && !string.IsNullOrWhiteSpace(saveModel.ContactEmail))
                        {
                            SMTPSetupModel smtpData = _smptSetupService.SMTPSetupQuery("director");
                            smtpData.SendMailBySMTP(saveModel.ContactEmail, saveModel.ProposalSubject, saveModel.ProposalContent);  //發MAIL給系統管理員並CC給填寫意見者
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
            string secretKey = "6LdAdIwUAAAAAMCncx3ugZnEzW9O-lDLzJjnSNyg";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            return status;
        }
        #endregion
    }
}