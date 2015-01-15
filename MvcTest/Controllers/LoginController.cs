using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;
using System.Security.Principal;
using System.Web.Security;
using MvcTest.Filters;
using Common;
using Common.Loger;
using MvcTest.Extensions;

namespace MvcTest.Controllers
{
    public class LoginController : BaseController
    {


        // GET: /Login/
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["name"] == null)
            { }
            return View();
        }

        [HttpPost]
        [LoginFilterAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult Logins([Bind(Include = "LoginName,LoginPass")] ManagerR manager)
        {
            if (Request.Params["geetest_challenge"] == null && Request.Params["geetest_validate"] == null && Request.Params["geetest_seccode"] == null)
            {
                return Content("请验证码");
            }
            if (!Verification())
            {
                return Content("验证码错误");
            }
            if (ModelState.IsValid)
            {
                using (ToolsDBEntities db = new ToolsDBEntities())
                {
                    bool isPersistent = true;
                    Manager managers = db.Managers.FirstOrDefault(s => s.LoginName == manager.LoginName && s.LoginPass == manager.LoginPass);
                    if (managers != null)
                    {
                        managers.LoginCount = managers.LoginCount + 1;
                        managers.LoginTime = DateTime.Now;
                        managers.LoginIp = HttpContext.Request.UserHostAddress;
                        db.SaveChanges();
                        SetFormAuthentication(isPersistent, managers);

                        return Content("OK");
                    }
                    else
                    {
                        return Content("NO");
                    }
                }
            }
            return Content("NO");
        }

        private void SetFormAuthentication(bool isPersistent, Manager managers)
        {
            #region
            ////////////////////////////////////////////////////////////////////
            DateTime utcNow = DateTime.UtcNow;
            DateTime cookieUtc = isPersistent ? utcNow.AddDays(7) : utcNow.Add(FormsAuthentication.Timeout);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, managers.CName,
                utcNow,
                cookieUtc,
                isPersistent,
                managers.MID.ToString(), FormsAuthentication.FormsCookiePath);
            string ticketEncrypted = FormsAuthentication.Encrypt(ticket);
            if (string.IsNullOrEmpty(ticketEncrypted))
            {
                throw new InvalidOperationException("FormsAuthentication.Encrypt failed.");
            }
            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypted)
            {
                Domain = FormsAuthentication.CookieDomain,
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath,
            };

            var name = Uri.EscapeDataString(managers.LoginName);
            HttpCookie nameCookie = new HttpCookie("name", name)
            {
                Domain = FormsAuthentication.CookieDomain,
                HttpOnly = false,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath,
            };
            //if (isPersistent)
            {
                httpCookie.Expires = cookieUtc;
                nameCookie.Expires = cookieUtc;
            }
            HttpContext.Response.Cookies.Add(httpCookie);
            HttpContext.Response.Cookies.Add(nameCookie);
            ////////////////////////////////////////////////////////////

            #endregion
        }


        [HttpPost]
        [LoginFilterAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult Retrievepassword([Bind(Include = "Email")] ManagerR manager)
        {
            if (ModelState.IsValid)
            {
                using (ToolsDBEntities db = new ToolsDBEntities())
                {
                    Manager managers = db.Managers.FirstOrDefault(s => s.Email == manager.Email);
                    if (manager == null)
                    {
                        return Content("NO");
                    }
                    managers.LoginCount = managers.LoginCount + 1;
                    managers.LoginTime = DateTime.Now;
                    db.SaveChanges();
                    WebConfig webconfig = db.WebConfigs.FirstOrDefault(s => s.WebDomain == "www.zhangwj.com");

                    bool bl = SendEmailHelper.SendEmailDefault(managers.Email, managers.MID.ToString(), webconfig.WebEmail, webconfig.EmailUserName, webconfig.EmailSmtp, webconfig.EmailPassWord, webconfig.WebDomain);

                    if (!bl)
                    {
                        return Content("NO");
                    }
                    return Content("OK");
                }
            }
            return Content("NO");
        }


        // GET: /Login/LoginOut
        public ActionResult LoginOut()
        {

            return RedirectToAction("Index", "Login");
        }

        // GET: /Login/ClearCache
        public ActionResult ClearCache()
        {
            Common.Caching.CacheManager.ClearAll();
            return Content("OK");
        }

        public ActionResult VerificationCode()
        {
            String privateKey = "d7d13ef64e44006f1f6fb1b6122850ec";
            GeetestLib geetest = new GeetestLib(privateKey);
            bool result = geetest.validate(
                Request.Params["geetest_challenge"],
                Request.Params["geetest_validate"],
                Request.Params["geetest_seccode"]
             );
            if (result)
            {
                //验证正确后的操作
                return Content("OK");

            }
            else
            {
                //验证错误后的操作
                return Content("NO");
            }
        }

        public bool Verification()
        {
            String privateKey = "d7d13ef64e44006f1f6fb1b6122850ec";
            GeetestLib geetest = new GeetestLib(privateKey);
            return geetest.validate(
                Request.Params["geetest_challenge"],
                Request.Params["geetest_validate"],
                Request.Params["geetest_seccode"]
             );
        }

        }
}
