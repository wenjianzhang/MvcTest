using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;

namespace MvcTest.Filters
{
    public class ShowPostWindowFilterAttribute : FilterAttribute
    {
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    //base.OnResultExecuted(filterContext);
        //    filterContext.HttpContext.Response.Write("<script>$.layer.closeAll();</script>");
        //}

        //void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    throw new NotImplementedException();
        //}

        //void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    throw new NotImplementedException();
        //}


        //public void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    filterContext.HttpContext.Response.Write("<script>$.layer.closeAll();</script>");
        //}

        //void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //}
    }
}