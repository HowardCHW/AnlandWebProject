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

namespace AnlandProject.Backend.Controllers.Settings
{
    [Authorize]
    public class UserManagementController : BaseController
    {
        private IAccountService _userManagementService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/Settings/UserManagement/Index.cshtml");
        }

        public PartialViewResult MenuRight()
        {
            IMenuService _menuService = new MenuService();
            var menuItems = _menuService.MenuQuery();
            return PartialView("~/Views/Settings/UserManagement/PersonnelRights.cshtml", menuItems);
        }

        public ActionResult UserCreate(int? id)
        {
            string subTitle = "新增";
            using (_userManagementService = new AccountService())
            {
                AccountModel result = new AccountModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _userManagementService.AccountQueryByID(id.Value);
                    subTitle = "編輯";
                }
                ViewBag.Subtitle = subTitle;
                return View("~/Views/Settings/UserManagement/UserEdit.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult UserQuery()
        {
            using (_userManagementService = new AccountService())
            {
                List<AccountModel> result = _userManagementService.AccountQueryAll();
                return Json(new { data = result });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult UserSave(AccountModel model)
        {
            bool saveStatus = false;
            using (_userManagementService = new AccountService())
            {
                try
                {
                    saveStatus = _userManagementService.AccountSave(model);
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
        public JsonResult UserDelete(int id)
        {
            bool delStatus = false;
            using (_userManagementService = new AccountService())
            {
                try
                {
                    delStatus = _userManagementService.AccountDelete(id);
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
                Title = "刪除人員權限管理",
                Content = @"您確定要刪除帳號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "UserManagement",
                ActionName = "UserDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}