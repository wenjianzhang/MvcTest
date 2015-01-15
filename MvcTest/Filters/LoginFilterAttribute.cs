using MvcTest.Extensions;
using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Extensions;
using Common.Extensions;

namespace MvcTest.Filters
{
    public class LoginFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string LoginName = "未知";
            string status = "";
            LoginLog loginlog = new LoginLog();
            loginlog.Ip = filterContext.HttpContext.Request.UserHostAddress;
            loginlog.Notes = "controller:" + filterContext.RouteData.Values["controller"].ToString() + "    Action:" + filterContext.RouteData.Values["action"].ToString();
            ContentResult contentResult = filterContext.Result as ContentResult;
            if (contentResult.Content.ToString() == "OK")
            {
                HttpCookie ss = filterContext.RequestContext.HttpContext.Response.Cookies["name"];
                status = "登陆成功！";
                LoginName = Uri.UnescapeDataString(ss.Value);

            }
            else
            {
                status = "登陆失败！";
            }
            Manager managers = new Manager();
            IpInfo ipInfo = new IpInfo();
            //写日志
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                managers = db.Managers.FirstOrDefault(s => s.LoginName == LoginName);
                if (managers != null)
                {
                    loginlog.Manager_Id = managers.MID;
                    loginlog.Manager_CName = managers.CName;
                }
                else
                {
                    loginlog.Manager_CName = "不存在的用户：" + filterContext.RequestContext.HttpContext.Request.Form["LoginName"];
                }

                ipInfo = Common.Extensions.Extensions.GetIpCityMessage(loginlog.Ip, "json");


                loginlog.AddDate = DateTime.Now;
                loginlog.City = ipInfo.data.country + ipInfo.data.city;
                loginlog.Notes = "" + status;
                loginlog.IsDel = 0;
                db.LoginLogs.Add(loginlog);
                int i = db.SaveChanges();
            }
            //base.OnActionExecuted(filterContext);
            // filterContext.HttpContext.Response.Write("Action执行之后<br />");
        }


    }
}