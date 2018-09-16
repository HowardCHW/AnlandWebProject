using AnlandProject.Service;
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

namespace AnlandProject.Backend.Controllers.UnitItems
{
    [Authorize]
    public class ProposalController : BaseController
    {
        private IProposalService _proposalService;
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/UnitItems/Proposal/Index.cshtml");
        }

        public ActionResult ProposalView(int? id)
        {
            using (_proposalService = new ProposalService())
            {
                ProposalDataModel result = new ProposalDataModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _proposalService.PropoaslQueryByID(id.Value);
                }

                return View("~/Views/UnitItems/Proposal/ProposalView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult ProposalQuery()
        {
            using (_proposalService = new ProposalService())
            {
                List<ProposalDataModel> result = _proposalService.PropoaslQueryAll();
                return Json(new { data = result });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProposalDelete(int id)
        {
            bool delStatus = false;
            using (_proposalService = new ProposalService())
            {
                try
                {
                    delStatus = _proposalService.ProposalDelete(id);
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
                Title = "刪除民眾參與創新提案",
                Content = @"您確定要刪除 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Proposal",
                ActionName = "ProposalDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}