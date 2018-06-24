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

namespace AnlandProject.Backend.Controllers.OnlineApplication
{
    [Authorize]
    public class ReservationController : BaseController
    {
        private IOnlineApplicationService _reservationService;
        
        // GET: News
        public ActionResult Index()
        {
            return View("~/Views/OnlineApplication/Reservation/Index.cshtml");
        }        

        public ActionResult ReservationView(int? id)
        {
            using (_reservationService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id.HasValue && id.Value > 0)
                {
                    result = _reservationService.OnlineAppQueryByID(id.Value);
                }

                return View("~/Views/OnlineApplication/Reservation/ReservationView.cshtml", result);
            }
        }

        [HttpPost]
        public ActionResult ReservationQuery()
        {
            using (_reservationService = new OnlineApplicationService())
            {
                List<OnlineApplicationModel> result = _reservationService.OnlineAppQueryAll();
                return Json(new { data = result });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ReservationDelete(int id)
        {
            bool delStatus = false;
            using (_reservationService = new OnlineApplicationService())
            {
                try
                {
                    delStatus = _reservationService.OnlineAppDelete(id);
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
                Title = "刪除線上預約服務",
                Content = @"您確定要刪除編號 [{0}] 嗎？",
                ConfirmText = "確定",
                ControllerName = "Reservation",
                ActionName = "ReservationDelete",
                OnFailureFunction = "RequestFail",
                OnSuccessFunction = "DeleteSuccess",
            };

            return AjaxDialog(model);
        }
    }
}