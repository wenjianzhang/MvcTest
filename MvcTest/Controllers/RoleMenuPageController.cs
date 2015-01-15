
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
    public class RoleMenuPageController : BaseController
    {
        // GET: /RoleMenuPage/
        public ActionResult Index(int pageindex = 1)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.MenuInfo = db.MenuInfoes.OrderByDescending(s => s.MIID).ToList();
                return View(db.RoleMenuPages.OrderByDescending(s => s.id).ToPagedList(pageindex, 8));
            }
        }

        // GET: /RoleMenuPage/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleMenuPage RoleMenuPage = db.RoleMenuPages.Find(id);
                if (RoleMenuPage == null)
                {
                    return HttpNotFound();
                }
                return View(RoleMenuPage);
            }
        }

        // GET: /RoleMenuPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RoleMenuPage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,MIID,PPSID,RID")] RoleMenuPageR RoleMenuPage)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    RoleMenuPage tb = new RoleMenuPage();
                    tb.MIID = RoleMenuPage.MIID;
                    tb.PPSID = RoleMenuPage.PPSID;
                    tb.RID = RoleMenuPage.RID;
                    db.RoleMenuPages.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(RoleMenuPage);
            }
        }

        // GET: /RoleMenuPage/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleMenuPage RoleMenuPage = db.RoleMenuPages.Find(id);
                if (RoleMenuPage == null)
                {
                    return HttpNotFound();
                }
                return View(RoleMenuPage);
            }
        }

        // POST: /RoleMenuPage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,MIID,PPSID,RID")] RoleMenuPageR RoleMenuPage)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(RoleMenuPage).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(RoleMenuPage);
            }
        }

        // GET: /RoleMenuPage/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RoleMenuPage RoleMenuPage = db.RoleMenuPages.Find(id);
                if (RoleMenuPage == null)
                {
                    return HttpNotFound();
                }
                return View(RoleMenuPage);
            }
        }

        // POST: /RoleMenuPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                RoleMenuPage RoleMenuPage = db.RoleMenuPages.Find(id);
                db.RoleMenuPages.Remove(RoleMenuPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
