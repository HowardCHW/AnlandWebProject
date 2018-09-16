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
    public class ClassificationSearchController : BaseController
    {
        private ICategoritesService _categoritesService;
        private ICommonService _commonService;

        // GET: ClassificationSearch
        public ActionResult Index()
        {
            using (_commonService = new CommonService())
            {
                var themeData = _commonService.ThemeQueryAll().Select(t => new
                {
                    Name = string.Format("{0} / {1}", t.TypeCode, t.TypeName),
                    Code = t.TypeCode
                }).ToList();
                SelectList themeSelect = new SelectList(themeData, "Code", "Name");
                ViewBag.Theme = themeSelect;

                var cakeData = _commonService.CakeQueryAll().Select(c => new
                {
                    Name = string.Format("{0} / {1}", c.TypeCode, c.TypeName),
                    Code = c.TypeCode
                }).ToList();
                SelectList cakeSelect = new SelectList(cakeData, "Code", "Name");
                ViewBag.Cake = cakeSelect;

                var serviceData = _commonService.ServiceQueryAll().Select(s => new
                {
                    Name = string.Format("{0} / {1}", s.TypeCode, s.TypeName),
                    Code = s.TypeCode
                }).ToList();
                SelectList serviceSelect = new SelectList(serviceData, "Code", "Name");
                ViewBag.Service = serviceSelect;
            }

            return View();
        }

        [HttpPost]
        public ActionResult CategoritesQuery()
        {
            using (_categoritesService = new CategoritesService())
            {
                List<CategoritesDataModel> result = _categoritesService.CategoritesQueryAll();
                return Json(new { data = result });
            }
        }
    }
}