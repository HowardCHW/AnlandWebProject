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
            //this._accountService = new AccountService();
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
                    string returnUrl = HttpUtility.UrlEncode((Session["URL"] ?? "").ToString());
                    returnUrl = HttpUtility.UrlDecode(returnUrl);

                    Session.Remove("URL");
                    if (!Url.IsLocalUrl(returnUrl)) returnUrl = "~";

                    AccountModel userInfo = null;
                    using (_accountService = new AccountService())
                    {
                        userInfo = _accountService.AccountQuery(logonModel);

                        if (userInfo == null)
                        {
                            ModelState.AddModelError("", "帳號錯誤!!");
                            return View();
                        } 
                        else
                        {
                            //登入錯誤次數超過三次鎖定帳號
                            if (userInfo.LoginFailCount >= 3)
                            {
                                ModelState.AddModelError("", "該帳號已被鎖定請洽系統管理員!!");
                                return View();
                            }
                            else
                            {
                                if (userInfo.PWD != logonModel.Password)
                                {
                                    if (userInfo.LoginFailCount == 2)
                                    {
                                        ModelState.AddModelError("", "登入失敗 3 次, 該帳號已被鎖定請洽系統管理員!!");
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", $"密碼錯誤,登入失敗 {userInfo.LoginFailCount + 1} 次!!");
                                    }
                                    //更新登入錯誤次數
                                    _accountService.UpdateLoginError(logonModel.Account, true);
                                    return View();
                                }
                            }                            
                        }

                        //將登入錯誤次數歸零
                        if (userInfo.LoginFailCount > 0)
                        {
                            _accountService.UpdateLoginError(logonModel.Account, false);
                        }

                        SetIdentity(userInfo);
                    }

                    if (userInfo.IsFirstLogin || userInfo.PWDExpired)
                    {
                        return RedirectToAction("UserCreate", "UserManagement", new { id = userInfo.ID });
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
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
            identity.AddClaim(new Claim("MenuRight", userModel.Rights)); //Menu 權限
            identity.AddClaim(new Claim("UID", userModel.ID.ToString())); //使用者ID
            identity.AddClaim(new Claim("IsAdm", userModel.IsAdmin ? "Y" : "N")); //是否為管理者
            identity.AddClaim(new Claim("IsFirsttime", userModel.IsFirstLogin ? "Y" : "N")); //是否為首次登入
            identity.AddClaim(new Claim("PWDExpired", userModel.PWDExpired ? "Y" : "N")); //密碼是否到期

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(identity, new AuthenticationProperties() { IsPersistent = false });
        }

        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(SiteAuthenticationSettings.ApplicationAuthenticationType);

            Session.Clear();

            return RedirectToAction("Login");
        }
    }
}