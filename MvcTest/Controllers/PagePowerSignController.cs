
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
    public class PagePowerSignController : BaseController
    {
        // GET: /PagePowerSign/
        public ActionResult Index(int pageindex = 1)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
            return View(db.PagePowerSigns.OrderByDescending(s => s.PPSID).ToPagedList(pageindex, 8));
            }
        }

        // GET: /PagePowerSign/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PagePowerSign PagePowerSign = db.PagePowerSigns.Find(id);
                if (PagePowerSign == null)
                {
                    return HttpNotFound();
                }
                return View(PagePowerSign);
            }
        }

        // GET: /PagePowerSign/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PagePowerSign/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public ActionResult Create([Bind(Include = "PPSID,MIID,PPSPID,CName,EName")] PagePowerSignR PagePowerSign)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    PagePowerSign tb = new PagePowerSign();
tb.MIID = PagePowerSign.MIID;
tb.PPSPID = PagePowerSign.PPSPID;
tb.CName = PagePowerSign.CName;
tb.EName = PagePowerSign.EName;
                    db.PagePowerSigns.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(PagePowerSign);
            }
        }

        // GET: /PagePowerSign/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PagePowerSign PagePowerSign = db.PagePowerSigns.Find(id);
                if (PagePowerSign == null)
                {
                    return HttpNotFound();
                }
                return View(PagePowerSign);
            }
        }

        // POST: /PagePowerSign/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PPSID,MIID,PPSPID,CName,EName")] PagePowerSignR PagePowerSign)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(PagePowerSign).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(PagePowerSign);
            }
        }

        // GET: /PagePowerSign/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PagePowerSign PagePowerSign = db.PagePowerSigns.Find(id);
                if (PagePowerSign == null)
                {
                    return HttpNotFound();
                }
                return View(PagePowerSign);
            }
        }

        // POST: /PagePowerSign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                PagePowerSign PagePowerSign = db.PagePowerSigns.Find(id);
                db.PagePowerSigns.Remove(PagePowerSign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
