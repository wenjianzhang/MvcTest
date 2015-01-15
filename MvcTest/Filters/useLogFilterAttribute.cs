using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;
using System.Security.Principal;

namespace MvcTest.Filters
{
    public class useLogFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //    //filterContext.Controller

        //    //filterContext.HttpContext.Response.Write("Action执行之前" + Message + "<br />");

        //    // filterContext.HttpContext.Response.Redirect("User", true);
        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpCookie ss = filterContext.RequestContext.HttpContext.Response.Cookies["name"];
            string LoginName = "";
            if (ss.Value != null)
            {
                LoginName = Uri.UnescapeDataString(ss.Value);
            }
            else
            {
                LoginName = "未实例";
            }
            UseLog uselog = new UseLog();
            uselog.Ip = filterContext.HttpContext.Request.UserHostAddress;
            uselog.MenuInfo_Name = filterContext.RouteData.Values["controller"].ToString();
            uselog.Notes = "controller:" + filterContext.RouteData.Values["controller"].ToString() + "    Action:" + filterContext.RouteData.Values["action"].ToString(); ;
            uselog.Manager_Id = 0;
            uselog.Manager_CName = LoginName;
            uselog.AddDate = DateTime.Now;
            uselog.IsDel = 0;
            //写日志
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                db.UseLogs.Add(uselog);
                int i = db.SaveChanges();
            }
            //base.OnActionExecuted(filterContext);
            //filterContext.HttpContext.Response.Write("Action执行之后" + Message + "<br />");
        }

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    //base.OnResultExecuting(filterContext);
        //    //filterContext.HttpContext.Response.Write("返回Result之前" + Message + "<br />");
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    //base.OnResultExecuted(filterContext);
        //    //filterContext.HttpContext.Response.Write("返回Result之后" + Message + "<br />");
        //}
    }
}