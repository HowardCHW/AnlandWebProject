﻿using AnlandProject.Service;
using AnlandProject.Service.Interface;
using AnlandProject.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AnlandProject.Backend.Filters;
using AnlandProject.Backend.Models;

namespace AnlandProject.Backend.Controllers.EmpArea
{
    [Authorize]
    public class DocumentController : BaseController
    {
        private IDocumentService _documentService;
        private ICommonService _commonService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/EmpArea/Document/Index.cshtml");
        }

        public ActionResult CreateDocument(int? id)
        {
            string subTitle = "新增";
            using (_documentService = new DocumentService())
            using (_commonService = new CommonService())
            {
                DefaultDataModel result = new DefaultDataModel();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _documentService.DocumentQueryByID(id.Value);
                    subTitle = "編輯";
                }

                var authorData = _commonService.DocumentCategoryQueryAll();
                SelectList authorSelect = new SelectList(authorData.OrderBy(a => a.ClassID), "ClassName", "ClassName", result.Author);
                ViewBag.Author = authorSelect;
 
                ViewBag.Subtitle = subTitle;
                 return View("~/Views/EmpArea/Document/DocumentEdit.cshtml", result);
            }
        }

        public ActionResult DocumentView(int? id)
        {
            using (_documentService = new DocumentService())
            using (_commonService = new CommonService())
            {
                DefaultDataModel result = new DefaultDataModel();
                if (id.HasValue && id.Value > 0)
                {                    
                    result = _documentService.DocumentQueryByID(id.Value);
                }

                return View("~/Views/EmpArea/Document/DocumentView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult DocumentQuery()
        {
            using (_documentService = new DocumentService())
            {
                List<DefaultDataModel> result = _documentService.DocumentQueryAll();
                return Json(new { data = result.OrderByDescending(r => r.PostDate) });
            }
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        public JsonResult DocumentSave(DefaultDataModel model)
        {
            bool saveStatus = false;
            using (_documentService = new DocumentService())
            {
                try
                {
                    if (model.Files != null && model.Files.Count > 0)
                    {
                        foreach (HttpPostedFileBase item in model.Files)
                        {
                            if (item.ContentLength > 0)
                            {
                                string _FileName = Path.GetFileName(item.FileName);
                                string _path = Path.Combine(Server.MapPath("~/UploadedFiles/Document"), _FileName);
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
                    model.CreatedUser = UserInfo.UserName;
                    saveStatus = _documentService.DocumentSave(model);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return Json(saveStatus);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DocumentDelete(int id)
        {
            bool delStatus = false;
            using (_documentService = new DocumentService())
            {
                try
                {
                    delStatus = _documentService.DocumentDelete(id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return Json(delStatus);
        }

        public ActionResult DeleteConfirmDialog()
        {
            //設定對話框
            DialogViewModel model = new DialogViewModel()
            {
                ID = "DeleteConfirmDialog",
                Title = "刪除表單下載",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Document",
                ActionName = "DocumentDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}