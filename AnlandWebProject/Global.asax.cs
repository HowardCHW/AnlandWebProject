using AnlandProject.Service;
using AnlandProject.Service.Interface;
using AnlandProject.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AnlandWebProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            using (ICommonService common = new CommonService())
            {
                common.VisitorsUpdate();
                Session["visitor"] = common.VisitorsQuery();
            }
        }
    }
}
