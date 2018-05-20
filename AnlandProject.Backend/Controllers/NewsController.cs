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
        // GET: News
        public ActionResult Index()
        {
            //_newsService = new NewsService();
            //List<NewsModel> result = _newsService.NewsQueryAll();
            return View();
        }        

        public ActionResult NewsQuery()
        {
            _newsService = new NewsService();
            List<NewsModel> result = _newsService.NewsQueryAll();
            return Json(new { data = result });
        }
    }
}