using AnlandProject.Service;
using AnlandProject.Service.Interface;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace AnlandProject.Backend.Controllers.UnitItems
{
    [Authorize]
    public class NewsController : BaseController
    {
        private INewsService _newsService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/News/Index.cshtml");
        }

        public ActionResult CreateNews(int? id)
        {
            string subTitle = "新增";
            _commonService = new CommonService();

            NewsModel result = new NewsModel();
            if (id.HasValue && id.Value > 0)
            {
                _newsService = new NewsService();
                result = _newsService.NewsQueryByID(id.Value);
                subTitle = "編輯";
            }

            var themeData = _commonService.ThemeQueryAll();
            SelectList themeSelect = new SelectList(themeData, "TypeCode", "TypeName", result.Theme);
            ViewBag.Theme = themeSelect;

            var cakeData = _commonService.CakeQueryAll();
            SelectList cakeSelect = new SelectList(cakeData, "TypeCode", "TypeName", result.Cake);
            ViewBag.Cake = cakeSelect;

            var serviceData = _commonService.ServiceQueryAll();
            SelectList serviceSelect = new SelectList(serviceData, "TypeCode", "TypeName", result.Service);
            ViewBag.Service = serviceSelect;

            var authorData = _commonService.NewsCategoryQueryAll();
            SelectList authorSelect = new SelectList(authorData.OrderBy(a => a.ClassID), "ClassName", "ClassName", result.Author);
            ViewBag.Author = authorSelect;

            var deptData = _commonService.DeptQueryAll();
            SelectList deptSelect = new SelectList(deptData, "ID", "DeptName");
            ViewBag.CreatedDepID = deptSelect;

            ViewBag.Subtitle = subTitle;
            result.CreatedUser = UserInfo.UserName;
            result.CreatedUserPhone = deptData.FirstOrDefault().PhoneNo;
            return View("~/Views/UnitItems/News/NewsEdit.cshtml", result);
        }

        public ActionResult NewsView(int? id)
        {
            _commonService = new CommonService();

            NewsModel result = new NewsModel();
            if (id.HasValue && id.Value > 0)
            {
                _newsService = new NewsService();
                result = _newsService.NewsQueryByID(id.Value);
            }

            var deptData = _commonService.DeptQueryAll();
            string depName = deptData.FirstOrDefault(x => x.ID == result.CreatedDepID)?.DeptName;
            result.CreatedDepName = depName;

            return View("~/Views/UnitItems/News/NewsView.cshtml", result);
        }

        [HttpPost]
        public JsonResult NewsSave(NewsModel model)
        {
            string saveStatus = "";
            try
            {
                if (model.Files.Count > 0)
                {
                    foreach (HttpPostedFileBase item in model.Files)
                    {
                        if (item.ContentLength > 0)
                        {
                            string _FileName = Path.GetFileName(item.FileName);
                            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                            item.SaveAs(_path);
                        }                        
                    }
                }

                if (model.GroupFile.Count > 0)
                {
                    switch (model.GroupFile.Count)
                    {
                        case 1:
                            model.Homepage2 = model.GroupFile[0]?.fileName;
                            model.File1Momo = model.GroupFile[0]?.fileMemo;
                            break;
                        case 2:
                            model.Homepage2 = model.GroupFile[0]?.fileName;
                            model.File1Momo = model.GroupFile[0]?.fileMemo;
                            model.Homepage3 = model.GroupFile[1]?.fileName;
                            model.File2Momo = model.GroupFile[1]?.fileMemo;
                            break;
                        case 3:
                            model.Homepage2 = model.GroupFile[0]?.fileName;
                            model.File1Momo = model.GroupFile[0]?.fileMemo;
                            model.Homepage3 = model.GroupFile[1]?.fileName;
                            model.File2Momo = model.GroupFile[1]?.fileMemo;
                            model.Homepage4 = model.GroupFile[2]?.fileName;
                            model.File3Momo = model.GroupFile[2]?.fileMemo;
                            break;
                        case 4:
                            model.Homepage2 = model.GroupFile[0]?.fileName;
                            model.File1Momo = model.GroupFile[0]?.fileMemo;
                            model.Homepage3 = model.GroupFile[1]?.fileName;
                            model.File2Momo = model.GroupFile[1]?.fileMemo;
                            model.Homepage4 = model.GroupFile[2]?.fileName;
                            model.File3Momo = model.GroupFile[2]?.fileMemo;
                            model.Homepage5 = model.GroupFile[3]?.fileName;
                            model.File4Momo = model.GroupFile[3]?.fileMemo;
                            break;
                        case 5:
                            model.Homepage2 = model.GroupFile[0]?.fileName;
                            model.File1Momo = model.GroupFile[0]?.fileMemo;
                            model.Homepage3 = model.GroupFile[1]?.fileName;
                            model.File2Momo = model.GroupFile[1]?.fileMemo;
                            model.Homepage4 = model.GroupFile[2]?.fileName;
                            model.File3Momo = model.GroupFile[2]?.fileMemo;
                            model.Homepage5 = model.GroupFile[3]?.fileName;
                            model.File4Momo = model.GroupFile[3]?.fileMemo;
                            model.Homepage6 = model.GroupFile[4]?.fileName;
                            model.File5Momo = model.GroupFile[4]?.fileMemo;
                            break;
                        default:
                            break;
                    }                    
                }

                if (model.ID > 0)
                {
                    saveStatus = "News更新";
                }
                else
                {
                    saveStatus = "News新增";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }            

            return Json(saveStatus);
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