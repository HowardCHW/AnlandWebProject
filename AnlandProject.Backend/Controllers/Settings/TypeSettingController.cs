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
using static AnlandProject.Service.BusinessModel.ClassificationTypeEnum;

namespace AnlandProject.Backend.Controllers.Settings
{
    [Authorize]
    public class TypeSettingController : BaseController
    {
        private ICommonService _commonService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/Settings/TypeSetting/TypeSetup.cshtml");
        }

        #region Query Area
        [HttpPost]
        public ActionResult ThemeQuery()
        {
            using (_commonService = new CommonService())
            {
                List<ClassificationModel> result = _commonService.ThemeQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult CakeQuery()
        {
            using (_commonService = new CommonService())
            {
                List<ClassificationModel> result = _commonService.CakeQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult ServiceQuery()
        {
            using (_commonService = new CommonService())
            {
                List<ClassificationModel> result = _commonService.ServiceQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult NewsClassQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.NewsCategoryQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult LawClassQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.LawsCategoryQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult QaClassQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.QaCategoryQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult BoardClassQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.BoardCategoryQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult MeetingClassQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.MeetingCategoryQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult DocumentClassQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.DocumentCategoryQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        public ActionResult PGQuery()
        {
            using (_commonService = new CommonService())
            {
                List<CategoryModel> result = _commonService.PostGroupQueryAll();
                return Json(new { data = result });
            }
        }
        #endregion


        #region Save Area
        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult ClassificationSearchSave(ClassificationModel model, string type)
        {
            bool saveStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    model.CreUser = UserInfo.UserName;
                    model.CreDate = DateTime.Now;
                    switch (type)
                    {
                        case "Theme":
                            saveStatus = _commonService.ClassificationSearchSave(model, ClassificationSearch.Theme);
                            break;
                        case "Cake":
                            saveStatus = _commonService.ClassificationSearchSave(model, ClassificationSearch.Cake);
                            break;
                        case "Service":
                            saveStatus = _commonService.ClassificationSearchSave(model, ClassificationSearch.Service);
                            break;
                        default:
                            break;
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
        [AjaxValidateAntiForgeryToken]
        public JsonResult ClassificationSave(CategoryModel model, string type)
        {
            bool saveStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    model.CreUser = UserInfo.UserName;
                    model.CreDate = DateTime.Now;
                    switch (type)
                    {
                        case "NC":
                            saveStatus = _commonService.ClassificationSave(model, Classification.NewsClass);
                            break;
                        case "LC":
                            saveStatus = _commonService.ClassificationSave(model, Classification.LawClass);
                            break;
                        case "QC":
                            saveStatus = _commonService.ClassificationSave(model, Classification.QaClass);
                            break;
                        case "BC":
                            saveStatus = _commonService.ClassificationSave(model, Classification.BoardClass);
                            break;
                        case "MC":
                            saveStatus = _commonService.ClassificationSave(model, Classification.MeetingClass);
                            break;
                        case "DC":
                            saveStatus = _commonService.ClassificationSave(model, Classification.DocumentClass);
                            break;
                        default:
                            break;
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
        [AjaxValidateAntiForgeryToken]
        public JsonResult PGSave(CategoryModel model)
        {
            bool saveStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    saveStatus = _commonService.PostgroupSave(model);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return Json(saveStatus);
        }
        #endregion


        #region Delete Area
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ClassificationSearchDelete(int id, string type)
        {
            bool delStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    switch (type)
                    {
                        case "Theme":
                            delStatus = _commonService.ClassificationSearchDelete(id, ClassificationSearch.Theme);
                            break;
                        case "Cake":
                            delStatus = _commonService.ClassificationSearchDelete(id, ClassificationSearch.Cake);
                            break;
                        case "Service":
                            delStatus = _commonService.ClassificationSearchDelete(id, ClassificationSearch.Service);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(delStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ClassificationDelete(int id, string type)
        {
            bool delStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    switch (type)
                    {
                        case "NC":
                            delStatus = _commonService.ClassificationDelete(id, Classification.NewsClass);
                            break;
                        case "LC":
                            delStatus = _commonService.ClassificationDelete(id, Classification.LawClass); ;
                            break;
                        case "QC":
                            delStatus = _commonService.ClassificationDelete(id, Classification.QaClass);
                            break;
                        case "BC":
                            delStatus = _commonService.ClassificationDelete(id, Classification.BoardClass);
                            break;
                        case "MC":
                            delStatus = _commonService.ClassificationDelete(id, Classification.MeetingClass);
                            break;
                        case "DC":
                            delStatus = _commonService.ClassificationDelete(id, Classification.DocumentClass);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(delStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PGDelete(int id)
        {
            bool delStatus = false;
            using (_commonService = new CommonService())
            {
                try
                {
                    delStatus = _commonService.PostgroupDelete(id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(delStatus);
        }
        #endregion


        #region Delete Confirm Dialog Area
        public ActionResult ClassificationSearchDeleteConfirmDialog()
        {
            //設定對話框
            DialogViewModel model = new DialogViewModel()
            {
                ID = "ClassificationSearchDeleteConfirmDialog",
                Title = "刪除分類檢索設定",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "TypeSetting",
                ActionName = "ClassificationSearchDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }

        public ActionResult ClassificationDeleteConfirmDialog()
        {
            //設定對話框
            DialogViewModel model = new DialogViewModel()
            {
                ID = "ClassificationDeleteConfirmDialog",
                Title = "刪除分類設定",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "TypeSetting",
                ActionName = "ClassificationDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }

        public ActionResult PGDeleteConfirmDialog()
        {
            //設定對話框
            DialogViewModel model = new DialogViewModel()
            {
                ID = "PGDeleteConfirmDialog",
                Title = "刪除分類設定",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "TypeSetting",
                ActionName = "PGDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
        #endregion

    }
}