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
    public class UserQuestionController : BaseController
    {
        private IQaService _qaService;
        private ICommonService _commonService;

        public ActionResult Index()
        {
            using (_commonService = new CommonService())
            {
                var classfyData = _commonService.QaCategoryQueryAll();
                SelectList classfySelect = new SelectList(classfyData.OrderBy(a => a.ID), "ClassID", "ClassName");
                ViewBag.Classfy = classfySelect;
            }

            return View();
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
                return Json(new { data = result.OrderByDescending(r => r.ID) });
            }
        }

        public ActionResult QuestionDetail(int? id)
        {
            using (_qaService = new QaService())
            using (_commonService = new CommonService())
            {
                QaModel result = new QaModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _qaService.QaQueryByID(id.Value, true);
                }

                var classfyData = _commonService.LawsCategoryQueryAll();
                result.ClassfyName = classfyData.FirstOrDefault(c => c.ClassID == result.Classfy)?.ClassName;

                return View(result);
            }
        }
    }
}