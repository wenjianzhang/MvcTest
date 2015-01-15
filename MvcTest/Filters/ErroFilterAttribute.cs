using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;

namespace MvcTest.Filters
{
    public class ErroFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //出错时间    

            string ErrTime = DateTime.Now.ToString();

            //错误类型       

            string ErrType = filterContext.Exception.GetType().ToString();

            //错误信息       

            string ErrMessage = filterContext.Exception.Message;

            //引发异常的方法   

            string targetsite = filterContext.Exception.TargetSite.ToString();

            //引发异常的对象     

            string source = filterContext.Exception.Source.ToString();

            //异常Controller名称       

            string controller = filterContext.RouteData.Values["controller"].ToString();

            //异常Action名称       

            string action = filterContext.RouteData.Values["action"].ToString();

            string ErrStr = string.Format("时间:{0} ,错误类型:{0},错误信息:{2},异常的方法:{3},异常的对象:{4},Controller名称:{5},Action名称:{6}", ErrTime, ErrType, ErrMessage, targetsite, source, controller, action);

            ErrorLog errolog = new ErrorLog();
            errolog.Ip = filterContext.HttpContext.Request.UserHostAddress;
            errolog.BrowserVersion = filterContext.HttpContext.Request.Browser.Version;
            errolog.BrowserType = filterContext.HttpContext.Request.Browser.Type;
            errolog.ErrSource = source;
            errolog.ErrMessage = ErrStr;
            errolog.PageUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
            errolog.StackTrace = filterContext.Exception.StackTrace;
            errolog.HelpLink = filterContext.Exception.HelpLink;
            errolog.ErrTime = DateTime.Now;
            errolog.IsDel = 0;
            //写日志
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                db.ErrorLogs.Add(errolog);
                int i = db.SaveChanges();
            }
        }
    }
}