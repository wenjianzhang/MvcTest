﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTest.Controllers
{
    public class UploadController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    string filepath = "~/Uploads/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath(filepath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = saveName, saveUrl = filepath + saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}



