using NLog;
using System.Web.Mvc;

namespace AnlandProject.Web.Controllers
{
    public class BaseController : Controller
    {
        protected static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();
    }
}