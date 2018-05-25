using AnlandProject.Backend.App_Start;
using AnlandProject.Service;
using AnlandProject.Service.BusinessModel;
using AnlandProject.Service.Interface;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            if (ModelState.IsValid)
            {
                try
                {
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

                    //var now = DateTime.Now;
                    //var ticket = new FormsAuthenticationTicket(
                    //        version: 1,
                    //        name: userInfo.Account,
                    //        issueDate: now,
                    //        expiration: now.AddMinutes(30),
                    //        isPersistent: false,
                    //        userData: "",
                    //        cookiePath: FormsAuthentication.FormsCookiePath
                    //    );
                    SetIdentity(userInfo);

                    //FormsAuthentication.SetAuthCookie(userInfo.Account, false);

                    //var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    //Response.Cookies.Add(cookie);

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Login Error:");
                    logger.Error(ex);
                }                
            }
            return View();
        }

        private void SetIdentity(AccountModel userModel)
        {
            ClaimsIdentity identity = new ClaimsIdentity(SiteAuthenticationSettings.ApplicationAuthenticationType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userModel.Name)); //使用者帳號全名
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userModel.Account)); //AD 帳號

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(identity, new AuthenticationProperties() { IsPersistent = false });
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();

            ////清除所有的 session
            //Session.Abandon();
            //Session.RemoveAll();

            //Request.Cookies.Remove(FormsAuthentication.FormsCookieName);

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(SiteAuthenticationSettings.ApplicationAuthenticationType);

            Session.Clear();

            return RedirectToAction("Login");
        }
    }
}