﻿using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
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
    }
}