using AnlandProject.Service;
using AnlandProject.Service.Interface;
using NLog;
using System.Web.Mvc;

namespace AnlandProject.Backend.Controllers
{
    public class BaseController : Controller
    {
        protected static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            IMenuService _menuService = new MenuService();
            var menuItems = _menuService.MenuQuery();
            if (menuItems != null || menuItems.Count > 0)
            {
                return View(menuItems);
            }
            return View();
        }
    }
}