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
    public class IntroController : BaseController
    {
        private IIntroService _introService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/Intro/Index.cshtml");
        }

        public ActionResult CreateIntro(int? id)
        {
            string subTitle = "新增";
            using (_introService = new IntroService())
            using (_commonService = new CommonService())
            {
                Intro10Model result = new Intro10Model();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _introService.IntroQueryByID(id.Value);
                    subTitle = "編輯";
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

                var authorData = _commonService.PostGroupQueryAll();
                SelectList authorSelect = new SelectList(authorData.OrderBy(a => a.ClassID), "ClassName", "ClassName", result.Author);
                ViewBag.Author = authorSelect;

                var deptData = _commonService.DeptQueryAll();
                SelectList deptSelect = new SelectList(deptData, "ID", "DeptName");
                ViewBag.CreatedDeptID = deptSelect;

                ViewBag.Subtitle = subTitle;
                result.CreatedUser = UserInfo.UserName;
                result.CreatedUserPhone = deptData.FirstOrDefault().PhoneNo;
                return View("~/Views/UnitItems/Intro/IntroEdit.cshtml", result);
            }
        }

        public ActionResult IntroView(int? id)
        {
            using (_introService = new IntroService())
            using (_commonService = new CommonService())
            {
                Intro10Model result = new Intro10Model();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _introService.IntroQueryByID(id.Value);
                }

                var themeData = _commonService.ThemeQueryAll();
                result.ThemeName = themeData.FirstOrDefault(t => t.TypeCode == result.Theme)?.TypeName;

                var cakeData = _commonService.CakeQueryAll();
                result.CakeName = cakeData.FirstOrDefault(c => c.TypeCode == result.Cake)?.TypeName;

                var serviceData = _commonService.ServiceQueryAll();
                result.ServiceName = serviceData.FirstOrDefault(s => s.TypeCode == result.Service)?.TypeName;
                
                var deptData = _commonService.DeptQueryAll();
                string depName = deptData.FirstOrDefault(x => x.ID == result.CreatedDeptID)?.DeptName;
                result.CreatedDeptName = depName;

                return View("~/Views/UnitItems/Intro/IntroView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult IntroQuery()
        {
            using (_introService = new IntroService())
            {
                List<Intro10Model> result = _introService.IntroQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult IntroSave(Intro10Model model)
        {
            bool saveStatus = false;
            using (_introService = new IntroService())
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
                                string _path = Path.Combine(Server.MapPath("~/UploadedFiles/intro10"), _FileName);
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

                    saveStatus = _introService.IntroSave(model);
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
        public JsonResult IntroDelete(int id)
        {
            bool delStatus = false;
            using (_introService = new IntroService())
            {
                try
                {
                    delStatus = _introService.IntroDelete(id);
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
                Title = "刪除行政規章",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Intro",
                ActionName = "IntroDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}