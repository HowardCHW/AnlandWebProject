using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnlandProject.Backend.App_Start
{
    /// <summary>
    /// 網站驗證相關設定值 類別
    /// </summary>
    public static class SiteAuthenticationSettings
    {
        /// <summary>
        /// 網站驗證方式
        /// </summary>
        public const string ApplicationAuthenticationType = "Anland_BackendAuthenticationType";

        /// <summary>
        /// 網站 Cookie 名稱
        /// </summary>
        public const string ApplicationCookieName = "Anland_Web_Backend";

        /// <summary>
        /// 登入頁面路徑
        /// </summary>
        public const string LoginPath = "/Account/Login";
    }

    public partial class Startup
    {
        /// <summary>
        /// 設定網站驗證資訊
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = SiteAuthenticationSettings.ApplicationAuthenticationType,
                LoginPath = new PathString(SiteAuthenticationSettings.LoginPath),
                Provider = new CookieAuthenticationProvider(),
                CookieName = SiteAuthenticationSettings.ApplicationCookieName,
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(1)
            });
        }
    }
}