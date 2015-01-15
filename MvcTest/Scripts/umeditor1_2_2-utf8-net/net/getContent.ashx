<%@ WebHandler  Language="C#"  Class="getContent" %>
/**
 * Created by visual studio 2010
 * User: xuheng
 * Date: 12-3-6
 * Time: 下午21:23
 * To get the value of editor and output the value .
 */
using System;
using System.Web;

public class getContent : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        //获取数据
        string content = context.Request.Form["myEditor"];

        
        //存入数据库或者其他操作
        //-------------

        //显示


        context.Response.Write("<script src='~/Scripts/jquery-1.10.2.min.js'></script>");
        context.Response.Write("<script src='~/Scripts/umeditor1_2_2-utf8-net/third-party/mathquill/mathquill.min.js'></script>");
        context.Response.Write("<link rel='stylesheet' href='~/Scripts/umeditor1_2_2-utf8-net/third-party/mathquill/mathquill.css'/>");
        context.Response.Write("<div class='content'>" + content + "</div>");

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}