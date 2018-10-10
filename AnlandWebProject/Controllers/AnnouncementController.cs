using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class AnnouncementController : BaseController
    {
        private INewsService _newsService;
        private ILawsService _lawsService;
        private ICommonService _commonService;

        // GET: Announcement
        public ActionResult News()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewsQuery()
        {
            using (_newsService = new NewsService())
            {
                List<DefaultDataModel> result = _newsService.NewsQueryAll();
                return Json(new { data = result.OrderByDescending(n => n.PostDate) });
            }
        }

        public ActionResult NewsDetail(int? id)
        {
            using (_newsService = new NewsService())
            {
                DefaultDataModel result = new DefaultDataModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _newsService.NewsQueryByID(id.Value, true);
                }

                string url = ConfigurationManager.AppSettings["BackEndUrl"];
                result.Homepage2ImgUrl = string.Format("{0}", url);
                result.Homepage3ImgUrl = string.Format("{0}", url);
                result.Homepage4ImgUrl = string.Format("{0}", url);
                result.Homepage5ImgUrl = string.Format("{0}", url);
                result.Homepage6ImgUrl = string.Format("{0}", url);

                return View(result);
            }
        }

        public ActionResult Laws()
        {
            return View();
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
                return Json(new { data = result.OrderByDescending(l => l.LDate) });
            }
        }

        public ActionResult LawsDetail(int? id)
        {
            using (_lawsService = new LawsService())
            using (_commonService = new CommonService())
            {
                LawsModel result = new LawsModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _lawsService.LawsQueryByID(id.Value, true);
                }
                var classfyData = _commonService.LawsCategoryQueryAll();
                result.ClassfyName = classfyData.FirstOrDefault(c => c.ClassID == result.Classfy).ClassName;
                return View(result);
            }
        }

        public FileResult Download(string type, string fileName)
        {
            string storagePath = ConfigurationManager.AppSettings["DownloadPath"];
            string path = string.Format("{0}\\{1}\\{2}", storagePath, type, fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }        
    }
}