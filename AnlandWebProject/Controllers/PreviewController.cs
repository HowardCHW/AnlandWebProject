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
    public class PreviewController : Controller
    {
        private IOnlineApplicationService _copyService;
        private IOnlineApplicationService _LandPriceService;
        private IOnlineApplicationService _transcriptService;
        private IOnlineApplicationService _reservationService;

        public ActionResult AppBackup(int id)
        {
            using (_copyService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id > 0)
                {
                    result = _copyService.CopyQueryByID(id);
                }

                return View("CopyView", result);
            }
        }

        public ActionResult PriceInfo(int id)
        {
            using (_LandPriceService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id> 0)
                {
                    result = _LandPriceService.LandPriceQueryByID(id);
                }

                return View("LandPriceView", result);
            }
        }

        public ActionResult EngEstate(int id)
        {
            using (_transcriptService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id > 0)
                {
                    result = _transcriptService.EngCopyQueryByID(id);
                }

                return View("TranscriptView", result);
            }
        }

        public ActionResult OnlineBooking(int id)
        {
            using (_reservationService = new OnlineApplicationService())
            {
                OnlineApplicationModel result = new OnlineApplicationModel();
                if (id > 0)
                {
                    result = _reservationService.OnlineAppQueryByID(id);
                }

                return View("ReservationView", result);
            }
        }
    }
}