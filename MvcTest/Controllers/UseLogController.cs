
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
    public class UseLogController : BaseController
    {
        // GET: /UseLog/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                return View(db.UseLogs.Where(s => s.IsDel == 0).OrderByDescending(s => s.Id).ToPagedList(pageindex, 8));
            }
        }

        // GET: /UseLog/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UseLog UseLog = db.UseLogs.Find(id);
                if (UseLog == null)
                {
                    return HttpNotFound();
                }
                return View(UseLog);
            }
        }

        // GET: /UseLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UseLog/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AddDate,Manager_Id,Manager_CName,Ip,MenuInfo_Id,MenuInfo_Name,Notes")] UseLogR UseLog)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    UseLog tb = new UseLog();
                    tb.AddDate = UseLog.AddDate;
                    tb.Manager_Id = UseLog.Manager_Id;
                    tb.Manager_CName = UseLog.Manager_CName;
                    tb.Ip = UseLog.Ip;
                    tb.MenuInfo_Id = UseLog.MenuInfo_Id;
                    tb.MenuInfo_Name = UseLog.MenuInfo_Name;
                    tb.Notes = UseLog.Notes;
                    tb.IsDel = 0;
                    db.UseLogs.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(UseLog);
            }
        }

        // GET: /UseLog/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UseLog UseLog = db.UseLogs.Find(id);
                if (UseLog == null)
                {
                    return HttpNotFound();
                }
                return View(UseLog);
            }
        }

        // POST: /UseLog/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AddDate,Manager_Id,Manager_CName,Ip,MenuInfo_Id,MenuInfo_Name,Notes")] UseLogR UseLog)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(UseLog).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(UseLog);
            }
        }

        // GET: /UseLog/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UseLog UseLog = db.UseLogs.Find(id);
                if (UseLog == null)
                {
                    return HttpNotFound();
                }
                return View(UseLog);
            }
        }

        // POST: /UseLog/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                UseLog UseLog = db.UseLogs.Find(id);
                UseLog.IsDel = 1;
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

        // POST: /UseLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                UseLog UseLog = db.UseLogs.Find(id);
                db.UseLogs.Remove(UseLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
