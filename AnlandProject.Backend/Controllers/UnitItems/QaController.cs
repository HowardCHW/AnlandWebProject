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
    public class QaController : BaseController
    {
        private IQaService _qaService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/Qa/Index.cshtml");
        }

        public ActionResult CreateQa(int? id)
        {
            string subTitle = "新增";
            using (_qaService = new QaService())
            using (_commonService = new CommonService())
            {
                QaModel result = new QaModel();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _qaService.QaQueryByID(id.Value);
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

                var classfyData = _commonService.LawsCategoryQueryAll();
                SelectList classfySelect = new SelectList(classfyData.OrderBy(a => a.ID), "ClassID", "ClassName", result.Classfy);
                ViewBag.Classfy = classfySelect;

                var deptData = _commonService.DeptQueryAll();
                SelectList deptSelect = new SelectList(deptData, "ID", "DeptName");
                ViewBag.CreatedDeptID = deptSelect;

                ViewBag.Subtitle = subTitle;
                result.CreatedUser = UserInfo.UserName;
                result.CreatedUserPhone = deptData.FirstOrDefault().PhoneNo;
                return View("~/Views/UnitItems/Qa/QaEdit.cshtml", result);
            }
        }

        public ActionResult QaView(int? id)
        {
            using (_qaService = new QaService())
            using (_commonService = new CommonService())
            {
                QaModel result = new QaModel();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _qaService.QaQueryByID(id.Value);
                }

                var classfyData = _commonService.LawsCategoryQueryAll();
                result.ClassfyName = classfyData.FirstOrDefault(c => c.ClassID == result.Classfy)?.ClassName;

                var deptData = _commonService.DeptQueryAll();
                string depName = deptData.FirstOrDefault(x => x.ID == result.CreatedDeptID)?.DeptName;
                result.CreatedDeptName = depName;

                return View("~/Views/UnitItems/Qa/QaView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult QaQuery()
        {
            using (_qaService = new QaService())
            using (_commonService = new CommonService())
            {
                List<QaModel> result = _qaService.QaQueryAll();
                var classfyData = _commonService.QaCategoryQueryAll();

                result.ForEach(r => r.ClassfyName = classfyData.FirstOrDefault(c => c.ClassID == r.Classfy).ClassName);
                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult QaSave(QaModel model)
        {
            bool saveStatus = false;
            using (_qaService = new QaService())
            {
                try
                {
                    saveStatus = _qaService.QaSave(model);
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
        public JsonResult QaDelete(int id)
        {
            bool delStatus = false;
            using (_qaService = new QaService())
            {
                try
                {
                    delStatus = _qaService.QaDelete(id);
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
                Title = "刪除 Q & A",
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