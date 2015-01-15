using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcTest.Extensions.ashx
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

            if (file != null)
            {
                //if (File.Exists(uploadPath + file.FileName))
                //{
                //    context.Response.Write("3");            //文件已经存在
                //    return;
                //}

                string[] fn = file.FileName.Split('.');
                string ext = fn[fn.Length - 1];
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + ext;
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(uploadPath + filename);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Session[context.Session["userName"].ToString()] = filename;

                context.Response.Write(filename);
            }
            else
            {
                context.Response.Write("0");
            }  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}