using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AnlandProject.Backend.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private IAccountService _accountService;

        public AccountController()
        {
            this._accountService = new AccountService();
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //logger.Trace("我是Trace");
            //logger.Debug("我是Debug");
            //logger.Info("我是Info");
            //logger.Warn("我是Warn");
            if (string.IsNullOrEmpty((Session["URL"] ?? "").ToString()))
            {
                Session["URL"] = Request["ReturnUrl"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LogonViewModel logonModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var returnUrl = HttpUtility.UrlEncode((Session["URL"] ?? "").ToString());
            returnUrl = HttpUtility.UrlDecode(returnUrl);

            Session.Remove("URL");
            if (!Url.IsLocalUrl(returnUrl)) returnUrl = "~";

            var userInfo = _accountService.AccountQuery(logonModel);

            if (userInfo == null)
            {
                ModelState.AddModelError("", "帳號或密碼錯誤!");
                return View();
            }

            //Session["UserRole"] = userInfo.USRDID;
            var now = DateTime.Now;
            var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: userInfo.Account,
                    issueDate: now,
                    expiration: now.AddMinutes(30),
                    isPersistent: false,
                    userData: "",
                    cookiePath: FormsAuthentication.FormsCookiePath
                );

            FormsAuthentication.SetAuthCookie(userInfo.Account, false);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            //清除所有的 session
            Session.Abandon();
            Session.RemoveAll();

            Request.Cookies.Remove(FormsAuthentication.FormsCookieName);

            return RedirectToAction("Login", "Account");
        }
    }
}