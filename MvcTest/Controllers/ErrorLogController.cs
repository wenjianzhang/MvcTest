
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;
using PagedList;

namespace MvcTest.Controllers
{
    public class ErrorLogController : BaseController
    {
        // GET: /ErrorLog/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                return View(db.ErrorLogs.Where(s => s.IsDel == 0).OrderByDescending(s => s.Id).ToPagedList(pageindex, 8));
            }
        }

        // GET: /ErrorLog/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ErrorLog ErrorLog = db.ErrorLogs.Find(id);
                if (ErrorLog == null)
                {
                    return HttpNotFound();
                }
                return View(ErrorLog);
            }
        }

        // GET: /ErrorLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ErrorLog/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ErrTime,BrowserVersion,BrowserType,Ip,PageUrl,ErrMessage,ErrSource,StackTrace,HelpLink,Type")] ErrorLogR ErrorLog)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    ErrorLog tb = new ErrorLog();
                    tb.ErrTime = ErrorLog.ErrTime;
                    tb.BrowserVersion = ErrorLog.BrowserVersion;
                    tb.BrowserType = ErrorLog.BrowserType;
                    tb.Ip = ErrorLog.Ip;
                    tb.PageUrl = ErrorLog.PageUrl;
                    tb.ErrMessage = ErrorLog.ErrMessage;
                    tb.ErrSource = ErrorLog.ErrSource;
                    tb.StackTrace = ErrorLog.StackTrace;
                    tb.HelpLink = ErrorLog.HelpLink;
                    tb.IsDel = 0;
                    //tb.Type = ErrorLog.Type;
                    db.ErrorLogs.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(ErrorLog);
            }
        }

        // GET: /ErrorLog/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ErrorLog ErrorLog = db.ErrorLogs.Find(id);
                if (ErrorLog == null)
                {
                    return HttpNotFound();
                }
                return View(ErrorLog);
            }
        }

        // POST: /ErrorLog/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ErrTime,BrowserVersion,BrowserType,Ip,PageUrl,ErrMessage,ErrSource,StackTrace,HelpLink,Type")] ErrorLog ErrorLog)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ErrorLog).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ErrorLog);
            }
        }

        // GET: /ErrorLog/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ErrorLog ErrorLog = db.ErrorLogs.Find(id);
                if (ErrorLog == null)
                {
                    return HttpNotFound();
                }
                return View(ErrorLog);
            }
        }

        // POST: /ErrorLog/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ErrorLog ErrorLog = db.ErrorLogs.Find(id);
                ErrorLog.IsDel = 1;
                int i = db.SaveChanges();
                if (i > 0)
                {
                    return Content("OK");
                }
                else
                {
                    return Content("NO");
                }
            }
        }

        // POST: /ErrorLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ErrorLog ErrorLog = db.ErrorLogs.Find(id);
                db.ErrorLogs.Remove(ErrorLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
