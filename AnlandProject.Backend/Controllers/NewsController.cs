using AnlandProject.Service;
using AnlandProject.Service.Interface;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Backend.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private INewsService _newsService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateNews()
        {
            _commonService = new CommonService();

            var themeData = _commonService.ThemeQueryAll();
            SelectList themeSelect = new SelectList(themeData, "TypeCode", "TypeName");
            ViewBag.ThemeSelect = themeSelect;

            var cakeData = _commonService.CakeQueryAll();
            SelectList cakeSelect = new SelectList(cakeData, "TypeCode", "TypeName");
            ViewBag.CakeSelect = cakeSelect;

            var serviceData = _commonService.ServiceQueryAll();
            SelectList serviceSelect = new SelectList(serviceData, "TypeCode", "TypeName");
            ViewBag.ServiceSelect = serviceSelect;

            var authorData = _commonService.NewsCategoryQueryAll();
            SelectList authorSelect = new SelectList(authorData.OrderBy(a => a.ClassID), "ClassName", "ClassName");
            ViewBag.AuthorSelect = authorSelect;

            return View("~/Views/News/NewsEdit.cshtml");
        }

        [HttpPost]
        public ActionResult NewsQuery()
        {
            _newsService = new NewsService();
            List<NewsModel> result = _newsService.NewsQueryAll();
            return Json(new { data = result });
        }
    }
}