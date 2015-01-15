
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
    public class RoleManagerController : BaseController
    {
        // GET: /RoleManager/
        public ActionResult Index(int pageindex = 1)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
            return View(db.RoleManagers.OrderByDescending(s => s.ID).ToPagedList(pageindex, 8));
            }
        }

        // GET: /RoleManager/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleManager RoleManager = db.RoleManagers.Find(id);
                if (RoleManager == null)
                {
                    return HttpNotFound();
                }
                return View(RoleManager);
            }
        }

        // GET: /RoleManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RoleManager/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public ActionResult Create([Bind(Include = "ID,RID,MID")] RoleManagerR RoleManager)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    RoleManager tb = new RoleManager();
tb.RID = RoleManager.RID;
tb.MID = RoleManager.MID;
                    db.RoleManagers.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(RoleManager);
            }
        }

        // GET: /RoleManager/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleManager RoleManager = db.RoleManagers.Find(id);
                if (RoleManager == null)
                {
                    return HttpNotFound();
                }
                return View(RoleManager);
            }
        }

        // POST: /RoleManager/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RID,MID")] RoleManagerR RoleManager)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(RoleManager).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(RoleManager);
            }
        }

        // GET: /RoleManager/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleManager RoleManager = db.RoleManagers.Find(id);
                if (RoleManager == null)
                {
                    return HttpNotFound();
                }
                return View(RoleManager);
            }
        }

        // POST: /RoleManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                RoleManager RoleManager = db.RoleManagers.Find(id);
                db.RoleManagers.Remove(RoleManager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
