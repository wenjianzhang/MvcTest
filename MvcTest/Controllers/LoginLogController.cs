
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
using DataModel.Extensions;

namespace MvcTest.Controllers
{
    public class LoginLogController : BaseController
    {
        // GET: /LoginLog/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                return View(db.LoginLogs.Where(s => s.IsDel == 0).OrderByDescending(s => s.LoginLogID).ToPagedList(pageindex, 8));
            }
        }

        // GET: /LoginLog/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LoginLog LoginLog = db.LoginLogs.Find(id);
                if (LoginLog == null)
                {
                    return HttpNotFound();
                }
                return View(LoginLog);
            }
        }

        // GET: /LoginLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /LoginLog/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginLogID,AddDate,Manager_Id,Manager_CName,Ip,Notes")] LoginLogR LoginLog)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    LoginLog tb = new LoginLog();
                    tb.AddDate = LoginLog.AddDate;
                    tb.Manager_Id = LoginLog.Manager_Id;
                    tb.Manager_CName = LoginLog.Manager_CName;
                    tb.Ip = LoginLog.Ip;
                    tb.Notes = LoginLog.Notes;
                    tb.IsDel = 0;
                    db.LoginLogs.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(LoginLog);
            }
        }

        // GET: /LoginLog/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LoginLog LoginLog = db.LoginLogs.Find(id);
                if (LoginLog == null)
                {
                    return HttpNotFound();
                }
                return View(LoginLog);
            }
        }

        // POST: /LoginLog/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginLogID,AddDate,Manager_Id,Manager_CName,Ip,Notes")] LoginLogR LoginLog)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(LoginLog).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(LoginLog);
            }
        }

        // GET: /LoginLog/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LoginLog LoginLog = db.LoginLogs.Find(id);
                if (LoginLog == null)
                {
                    return HttpNotFound();
                }
                return View(LoginLog);
            }
        }

        // POST: /LoginLog/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                LoginLog LoginLog = db.LoginLogs.Find(id);
                LoginLog.IsDel = 1;
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

        // POST: /LoginLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                LoginLog LoginLog = db.LoginLogs.Find(id);
                db.LoginLogs.Remove(LoginLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
