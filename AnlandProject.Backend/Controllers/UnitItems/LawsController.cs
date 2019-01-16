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

namespace AnlandProject.Backend.Controllers.UnitItems
{
    [Authorize]
    public class LawsController : BaseController
    {
        private ILawsService _lawsService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/Laws/Index.cshtml");
        }

        public ActionResult CreateLaws(int? id)
        {
            string subTitle = "新增";
            using (_lawsService = new LawsService())
            using (_commonService = new CommonService())
            {
                LawsModel result = new LawsModel();
                var deptData = _commonService.DeptQueryAll();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _lawsService.LawsQueryByID(id.Value);
                    subTitle = "編輯";
                }
                else
                {
                    result.CreatedUser = UserInfo.UserName;
                    result.CreatedUserPhone = deptData.FirstOrDefault().PhoneNo;
                }

                var themeData = _commonService.ThemeQueryAll();
                SelectList themeSelect = new SelectList(themeData, "TypeCode", "TypeName", result.Theme);
                ViewBag.Theme = themeSelect;

                var cakeData = _commonService.CakeQueryAll();
                SelectList cakeSelect = new SelectList(cakeData, "TypeCode", "TypeName", result.Cake);
                ViewBag.Cake = cakeSelect;

                var serviceData = _commonService.ServiceQueryAll();
                SelectList serviceSelect = new SelectList(serviceData, "TypeCode", "TypeName", result.Service);
                ViewBag.Service = serviceSelect;

                var classfyData = _commonService.LawsCategoryQueryAll();
                SelectList classfySelect = new SelectList(classfyData.OrderBy(a => a.ID), "ClassID", "ClassName", result.Classfy);
                ViewBag.Classfy = classfySelect;
                                
                SelectList deptSelect = new SelectList(deptData, "ID", "DeptName");
                ViewBag.CreatedDeptID = deptSelect;

                ViewBag.Subtitle = subTitle;                
                return View("~/Views/UnitItems/Laws/LawsEdit.cshtml", result);
            }
        }

        public ActionResult LawsView(int? id)
        {
            using (_lawsService = new LawsService())
            using (_commonService = new CommonService())
            {
                LawsModel result = new LawsModel();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _lawsService.LawsQueryByID(id.Value);
                }

                var classfyData = _commonService.LawsCategoryQueryAll();
                result.ClassfyName = classfyData.FirstOrDefault(c => c.ClassID == result.Classfy)?.ClassName;

                var deptData = _commonService.DeptQueryAll();
                string depName = deptData.FirstOrDefault(x => x.ID == result.CreatedDeptID)?.DeptName;
                result.CreatedDeptName = depName;

                return View("~/Views/UnitItems/Laws/LawsView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult LawsQuery()
        {
            using (_lawsService = new LawsService())
            using (_commonService = new CommonService())
            {
                List<LawsModel> result = _lawsService.LawsQueryAll();
                var classfyData = _commonService.LawsCategoryQueryAll();

                result.ForEach(r => r.ClassfyName = classfyData.FirstOrDefault(c => c.ClassID == r.Classfy).ClassName);

                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult LawsSave(LawsModel model)
        {
            bool saveStatus = false;
            using (_lawsService = new LawsService())
            {
                try
                {
                    if (model.Files != null && model.Files.Count > 0)
                    {
                        foreach (HttpPostedFileBase item in model.Files)
                        {
                            if (item.ContentLength > 0)
                            {
                                string _FileName = Path.GetFileName(item.FileName);
                                string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Laws"), _FileName);
                                item.SaveAs(_path);
                            }
                        }
                    }

                    if (model.GroupFile.Count > 0)
                    {
                        switch (model.GroupFile.Count)
                        {
                            case 1:
                                model.Homepage2 = model.GroupFile[0]?.fileName;
                                model.File1Momo = model.GroupFile[0]?.fileMemo;
                                break;
                            case 2:
                                model.Homepage2 = model.GroupFile[0]?.fileName;
                                model.File1Momo = model.GroupFile[0]?.fileMemo;
                                model.Homepage3 = model.GroupFile[1]?.fileName;
                                model.File2Momo = model.GroupFile[1]?.fileMemo;
                                break;
                            case 3:
                                model.Homepage2 = model.GroupFile[0]?.fileName;
                                model.File1Momo = model.GroupFile[0]?.fileMemo;
                                model.Homepage3 = model.GroupFile[1]?.fileName;
                                model.File2Momo = model.GroupFile[1]?.fileMemo;
                                model.Homepage4 = model.GroupFile[2]?.fileName;
                                model.File3Momo = model.GroupFile[2]?.fileMemo;
                                break;
                            case 4:
                                model.Homepage2 = model.GroupFile[0]?.fileName;
                                model.File1Momo = model.GroupFile[0]?.fileMemo;
                                model.Homepage3 = model.GroupFile[1]?.fileName;
                                model.File2Momo = model.GroupFile[1]?.fileMemo;
                                model.Homepage4 = model.GroupFile[2]?.fileName;
                                model.File3Momo = model.GroupFile[2]?.fileMemo;
                                model.Homepage5 = model.GroupFile[3]?.fileName;
                                model.File4Momo = model.GroupFile[3]?.fileMemo;
                                break;
                            case 5:
                                model.Homepage2 = model.GroupFile[0]?.fileName;
                                model.File1Momo = model.GroupFile[0]?.fileMemo;
                                model.Homepage3 = model.GroupFile[1]?.fileName;
                                model.File2Momo = model.GroupFile[1]?.fileMemo;
                                model.Homepage4 = model.GroupFile[2]?.fileName;
                                model.File3Momo = model.GroupFile[2]?.fileMemo;
                                model.Homepage5 = model.GroupFile[3]?.fileName;
                                model.File4Momo = model.GroupFile[3]?.fileMemo;
                                model.Homepage6 = model.GroupFile[4]?.fileName;
                                model.File5Momo = model.GroupFile[4]?.fileMemo;
                                break;
                            default:
                                break;
                        }
                    }

                    saveStatus = _lawsService.LawsSave(model);
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
        public JsonResult LawDelete(int id)
        {
            bool delStatus = false;
            using (_lawsService = new LawsService())
            {
                try
                {
                    delStatus = _lawsService.LawDelete(id);
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
                Title = "刪除法令新訊",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Laws",
                ActionName = "LawDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}