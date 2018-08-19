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
using Newtonsoft.Json;

namespace AnlandProject.Backend.Controllers.UnitItems
{
    [Authorize]
    public class QuestionnaireController : BaseController
    {
        private IQuestionnaireService _questionnaireService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/Questionnaire/Index.cshtml");
        }

        public ActionResult CreateQuestionnaire()
        {
            return View("~/Views/UnitItems/Questionnaire/QuestionAdd.cshtml");
        }

        public ActionResult QuestionnaireView(int id)
        {
            using (_questionnaireService = new QuestionnaireService())
            {
                QuestionnaireDataModel result = new QuestionnaireDataModel();
                if (id > 0)
                {
                    result = _questionnaireService.QuestionnaireQueryByID(id);
                }

                return View("~/Views/UnitItems/Questionnaire/QuestionReview.cshtml", result);
            }
        }

        public ActionResult QuestionResult()
        {
            return View("~/Views/UnitItems/Questionnaire/QuestionAnswerView.cshtml");
        }

        public ActionResult QuestionEdit(int id)
        {
            using (_questionnaireService = new QuestionnaireService())
            {
                QuestionnaireDataModel result = new QuestionnaireDataModel();
                if (id > 0)
                {
                    result = _questionnaireService.QuestionnaireQueryByID(id);
                }
                ViewBag.QuestionViewData = JsonConvert.SerializeObject(result);
                return View("~/Views/UnitItems/Questionnaire/QuestionEdit.cshtml");
            }
        }

        public ActionResult QuestionnaireAnswer(int id)
        {
            using (_questionnaireService = new QuestionnaireService())
            {
                QuestionnaireDataModel result = new QuestionnaireDataModel();
                if (id > 0)
                {
                    result = _questionnaireService.QuestionnaireAnswerByID(id);
                }

                ViewBag.AnswerViewData = JsonConvert.SerializeObject(result);
                return View("~/Views/UnitItems/Questionnaire/QuestionAnswerView.cshtml");
            }
        }

        [HttpPost]
        public ActionResult QuestionnaireQuery()
        {
            using (_questionnaireService = new QuestionnaireService())
            {
                List<QuestionnaireDataModel> result = _questionnaireService.QuestionnaireQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult QuestionNameCheck(string qaName)
        {
            bool checkStatus = false;
            using (_questionnaireService = new QuestionnaireService())
            {
                checkStatus = _questionnaireService.QuestionNameCheck(qaName);
            }
            return Json(checkStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult QuestionnaireSave(QuestionnaireDataModel saveModel)
        {
            bool saveStatus = true;
            using (_questionnaireService = new QuestionnaireService())
            {
                try
                {
                    if (saveModel.QuestionID == 0)
                    {
                        saveModel.Who = UserInfo.UserName;
                        saveModel.CreateDate = DateTime.Now;
                    }                    
                    saveStatus = _questionnaireService.QuestionnaireSave(saveModel);
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
        public JsonResult QuestionnaireDelete(int id)
        {
            bool delStatus = true;
            using (_questionnaireService = new QuestionnaireService())
            {
                try
                {
                    delStatus = _questionnaireService.QuestionnaireDelete(id);
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
                Title = "刪除問卷調查",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Questionnaire",
                ActionName = "QuestionnaireDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}