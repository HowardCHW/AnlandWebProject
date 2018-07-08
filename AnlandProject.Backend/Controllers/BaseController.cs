using AnlandProject.Backend.Models;
using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using NLog;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Backend.Controllers
{
    public class BaseController : Controller
    {
        protected static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [ChildActionOnly]
        public ActionResult MainHeader()
        {
            ViewBag.UserName = UserInfo.UserName;
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            IMenuService _menuService = new MenuService();
            var menuItems = _menuService.MenuQuery();
            menuItems = menuItems.Where(m => m.ParentId == 0 || UserInfo.MenuPermissions.Contains(m.id)).ToList();
            if (menuItems != null || menuItems.Count > 0)
            {
                return View(menuItems);
            }
            return View();
        }

        protected BackendUserModel UserInfo
        {
            get
            {
                var userModel = new BackendUserModel();
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                if (authenticationManager != null && authenticationManager.User != null && authenticationManager.User.Identity != null)
                {
                    if (authenticationManager.User.Identity.IsAuthenticated)
                    { //是否已通過使用者驗證
                        ClaimsIdentity identity = new ClaimsIdentity(authenticationManager.User.Identity);

                        userModel.UserAccount = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                        userModel.UserName = identity.FindFirst(ClaimTypes.Name).Value;
                        var tempMenu = identity.FindFirst("MenuRight").Value;
                        userModel.MenuPermissions = tempMenu.Split(',').Select(int.Parse).ToList();
                    }
                }
                return userModel;
            }
        }

        public ActionResult AjaxDialog(DialogViewModel model)
        {
            return PartialView("~/Views/Shared/AjaxDialog.cshtml", model);
        }
    }
}