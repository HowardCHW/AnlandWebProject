using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class HomeController : BaseController
    {
        private INewsService _newsService;
        private ILawsService _lawsService;

        public ActionResult Index()
        {
            IndexDataModel result = new IndexDataModel();
            using (_newsService = new NewsService())
            using (_lawsService = new LawsService())
            {
                List<DefaultDataModel> news = _newsService.NewsQueryAll();
                List<LawsModel> laws = _lawsService.LawsQueryAll();

                result.Top5News = news.Where(n => n.PostDate.Value.Date <= DateTime.Now && (!n.EndDate.HasValue || n.EndDate.Value.Date >= DateTime.Now.Date)).OrderByDescending(n => n.PostDate).Take(5).ToList();
                result.Top5Laws = laws.Where(n => n.LDate.Value.Date <= DateTime.Now.Date).OrderByDescending(n => n.LDate).Take(5).ToList();
            }

            return View(result);
        }

        public ActionResult SiteMap()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Cp()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

    }
}