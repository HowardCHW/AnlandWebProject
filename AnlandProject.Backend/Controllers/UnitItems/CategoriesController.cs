using AnlandProject.Backend.Filters;
using AnlandProject.Backend.Models;
using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Backend.Controllers.UnitItems
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        private ICategoritesService _categoriesService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/Categories/Index.cshtml");
        }

        public ActionResult CreateCategories(int? id)
        {
            string subTitle = "新增";
            using (_categoriesService = new CategoritesService())
            using (_commonService = new CommonService())
            {
                CategoritesDataModel result = new CategoritesDataModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _categoriesService.CategoritesQueryByID(id.Value);
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

                ViewBag.Subtitle = subTitle;
                return View("~/Views/UnitItems/Categories/CategoriesEdit.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult CategoriesQuery()
        {
            using (_categoriesService = new CategoritesService())
            {
                string frontendUrl = ConfigurationManager.AppSettings["FrontEndUrl"];

                List<CategoritesDataModel> result = _categoriesService.CategoritesQueryAll();

                result.ForEach(r => r.Url = string.Format("{0}{1}", frontendUrl, r.Url));

                return Json(new { data = result.OrderBy(r => r.ID) });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CategoriesSave(CategoritesDataModel model)
        {
            bool saveStatus = false;
            using (_categoriesService = new CategoritesService())
            {
                try
                {
                    saveStatus = _categoriesService.CategoritesSave(model);
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
        public JsonResult CategoriesDelete(int id)
        {
            bool delStatus = false;
            using (_categoriesService = new CategoritesService())
            {
                try
                {
                    delStatus = _categoriesService.CategoritesDelete(id);
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
                Title = "刪除分類檢索",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Categories",
                ActionName = "CategoriesDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}