using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnlandProject.Backend.Helpers
{
    public static class ActiveMenuHelper
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName)
        {
            ViewContext viewContext = htmlHelper.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = htmlHelper.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();
            var currentArea = htmlHelper.ViewContext.RouteData.DataTokens["area"];

            string hrefLink = (string.IsNullOrWhiteSpace(actionName) || string.IsNullOrWhiteSpace(controllerName)) ? "#" : new UrlHelper(htmlHelper.ViewContext.RequestContext).Action(actionName, controllerName, new { area = areaName }).ToString();
            var builder = new TagBuilder("li")
            {
                InnerHtml = "<a href=\"" + hrefLink + "\"><i class='site-menu-icon icon fa-dot-circle-o' aria-hidden='true'></i><span class='site-menu-title'>" + linkText + "</span></a>"
            };
            builder.AddCssClass("site-menu-item");

            //if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase) && String.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase))
            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString());
        }
    }
}