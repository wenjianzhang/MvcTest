
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
using Common.Configurations;

namespace MvcTest.Controllers
{
    public class PagePowerSignPublicController : BaseController
    {
        // GET: /PagePowerSignPublic/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<PagePowerSignPublic> PagePowerSignPubliclist = new List<PagePowerSignPublic>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.PagePowerSignPublics.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.PPSPID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "PagePowerSignPublic");
                    if (ids.Length > 0)
                    {
                        return View(db.PagePowerSignPublics.Where(s => s.IsDel == 0 && ids.Contains(s.PPSPID)).Distinct().OrderByDescending(s => s.PPSPID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(PagePowerSignPubliclist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /PagePowerSignPublic/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PagePowerSignPublic PagePowerSignPublic = db.PagePowerSignPublics.Find(id);
                if (PagePowerSignPublic == null)
                {
                    return HttpNotFound();
                }
                return View(PagePowerSignPublic);
            }
        }

        // GET: /PagePowerSignPublic/Create
        public ActionResult Create()
        {
            GetRoleAndManager();
            return View();
        }

        // POST: /PagePowerSignPublic/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CName,EName,StyleClass,Type")] PagePowerSignPublicR PagePowerSignPublic)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    PagePowerSignPublic tb = new PagePowerSignPublic();
                    tb.CName = PagePowerSignPublic.CName;
                    tb.EName = PagePowerSignPublic.EName;
                    tb.StyleClass = PagePowerSignPublic.StyleClass;
                    tb.Type = PagePowerSignPublic.Type;
                    tb.IsDel = 0;
                    db.PagePowerSignPublics.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.PPSPID, "PagePowerSignPublic");
                    }
                    return RedirectToAction("Index");
                }

                return View(PagePowerSignPublic);
            }
        }

        // GET: /PagePowerSignPublic/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PagePowerSignPublic PagePowerSignPublic = db.PagePowerSignPublics.Find(id);
                if (PagePowerSignPublic == null)
                {
                    return HttpNotFound();
                }
                PagePowerSignPublicR tb = new PagePowerSignPublicR();
                tb.PPSPID = PagePowerSignPublic.PPSPID;
                tb.CName = PagePowerSignPublic.CName;
                tb.EName = PagePowerSignPublic.EName;
                tb.StyleClass = PagePowerSignPublic.StyleClass;
                tb.Type = PagePowerSignPublic.Type;
                GetRoleAndManager(PagePowerSignPublic.PPSPID, "PagePowerSignPublic");
                return View(tb);
            }
        }

        // POST: /PagePowerSignPublic/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "PPSPID,CName,EName,StyleClass,Type")] PagePowerSignPublicR PagePowerSignPublic)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    PagePowerSignPublic tb = db.PagePowerSignPublics.Find(id);
                    tb.CName = PagePowerSignPublic.CName;
                    tb.EName = PagePowerSignPublic.EName;
                    tb.StyleClass = PagePowerSignPublic.StyleClass;
                    tb.Type = PagePowerSignPublic.Type;
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == PagePowerSignPublic.PPSPID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, PagePowerSignPublic.PPSPID, "PagePowerSignPublic");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改PagePowerSignPublic id " + PagePowerSignPublic.PPSPID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(PagePowerSignPublic);
            }
        }

        // GET: /PagePowerSignPublic/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PagePowerSignPublic PagePowerSignPublic = db.PagePowerSignPublics.Find(id);
                if (PagePowerSignPublic == null)
                {
                    return HttpNotFound();
                }
                return View(PagePowerSignPublic);
            }
        }

        // POST: /PagePowerSignPublic/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                PagePowerSignPublic PagePowerSignPublic = db.PagePowerSignPublics.Find(id);
                PagePowerSignPublic.IsDel = 1;
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

        // POST: /PagePowerSignPublic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                PagePowerSignPublic PagePowerSignPublic = db.PagePowerSignPublics.Find(id);
                db.PagePowerSignPublics.Remove(PagePowerSignPublic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
