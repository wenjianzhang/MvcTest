using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Common.Configurations;
using MvcTest.Models;


namespace MvcTest.Controllers
{
    public class SpidersCollectionRulesController : BaseController
    {
        // GET: /SpidersCollectionRules/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<CollectionRulesTab> CollectionRulesTablist = new List<CollectionRulesTab>();
            using (var db = new SpidersDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.CollectionRulesTabs.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.Id).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS("CollectionRulesTab");
                    if (ids.Length > 0)
                    {
                        return View(db.CollectionRulesTabs.Where(s => ids.Contains(s.Id)).Distinct().OrderByDescending(s => s.Id).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(CollectionRulesTablist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /SpidersCollectionRules/Details/5
        public ActionResult Details(int? id)
        {
            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CollectionRulesTab CollectionRulesTab = db.CollectionRulesTabs.Find(id);
                if (CollectionRulesTab == null)
                {
                    return HttpNotFound();
                }
                return View(CollectionRulesTab);
            }
        }

        // GET: /SpidersCollectionRules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CollectionRulesTab/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CollectionName,SourceSiteTag,SourceSiteName,BaseUrl,CollectionListRulesBig,CollectionListRulesMin,CollectionUrl,Contentleft,Contentright,Titleleft,Titleright,Imgleft,Imgright,NewPublishTimeLeft,NewPublishTimeRight,TypeNameLeft,TypeNameRight,Encoding")] Spiders_CollectionRulesR CollectionRulesTab)
        {
            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                if (ModelState.IsValid)
                {
                    var tb = new CollectionRulesTab();
                    tb.CollectionName = CollectionRulesTab.CollectionName;
                    tb.SourceSiteTag = CollectionRulesTab.SourceSiteTag;
                    tb.SourceSiteName = CollectionRulesTab.SourceSiteName;
                    tb.BaseUrl = CollectionRulesTab.BaseUrl;
                    tb.CollectionListRulesBig = CollectionRulesTab.CollectionListRulesBig;
                    tb.CollectionListRulesMin = CollectionRulesTab.CollectionListRulesMin;
                    tb.CollectionUrl = CollectionRulesTab.CollectionUrl;
                    tb.Contentleft = CollectionRulesTab.Contentleft;
                    tb.Contentright = CollectionRulesTab.Contentright;
                    tb.Titleleft = CollectionRulesTab.Titleleft;
                    tb.Titleright = CollectionRulesTab.Titleright;
                    tb.Imgleft = CollectionRulesTab.Imgleft;
                    tb.Imgright = CollectionRulesTab.Imgright;
                    tb.NewPublishTimeLeft = CollectionRulesTab.NewPublishTimeLeft;
                    tb.NewPublishTimeRight = CollectionRulesTab.NewPublishTimeRight;
                    tb.TypeNameLeft = CollectionRulesTab.TypeNameLeft;
                    tb.TypeNameRight = CollectionRulesTab.TypeNameRight;
                    tb.Encoding = CollectionRulesTab.Encoding;
                    tb.IsDel = 0;
                    db.CollectionRulesTabs.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(CollectionRulesTab);
            }
        }

        // GET: /CollectionRulesTab/Edit/5
        public ActionResult Edit(int? id)
        {
            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CollectionRulesTab CollectionRulesTab = db.CollectionRulesTabs.Find(id);
                if (CollectionRulesTab == null)
                {
                    return HttpNotFound();
                }
                Spiders_CollectionRulesR tb = new Spiders_CollectionRulesR();
                tb.Id = CollectionRulesTab.Id;
                tb.CollectionName = CollectionRulesTab.CollectionName;
                tb.SourceSiteTag = CollectionRulesTab.SourceSiteTag;
                tb.SourceSiteName = CollectionRulesTab.SourceSiteName;
                tb.BaseUrl = CollectionRulesTab.BaseUrl;
                tb.CollectionListRulesBig = CollectionRulesTab.CollectionListRulesBig;
                tb.CollectionListRulesMin = CollectionRulesTab.CollectionListRulesMin;
                tb.CollectionUrl = CollectionRulesTab.CollectionUrl;
                tb.Contentleft = CollectionRulesTab.Contentleft;
                tb.Contentright = CollectionRulesTab.Contentright;
                tb.Titleleft = CollectionRulesTab.Titleleft;
                tb.Titleright = CollectionRulesTab.Titleright;
                tb.Imgleft = CollectionRulesTab.Imgleft;
                tb.Imgright = CollectionRulesTab.Imgright;
                tb.NewPublishTimeLeft = CollectionRulesTab.NewPublishTimeLeft;
                tb.NewPublishTimeRight = CollectionRulesTab.NewPublishTimeRight;
                tb.TypeNameLeft = CollectionRulesTab.TypeNameLeft;
                tb.TypeNameRight = CollectionRulesTab.TypeNameRight;
                tb.Encoding = CollectionRulesTab.Encoding;
                return View(tb);
            }
        }

        // POST: /CollectionRulesTab/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CollectionName,SourceSiteTag,SourceSiteName,BaseUrl,CollectionListRulesBig,CollectionListRulesMin,CollectionUrl,Contentleft,Contentright,Titleleft,Titleright,Imgleft,Imgright,NewPublishTimeLeft,NewPublishTimeRight,TypeNameLeft,TypeNameRight,Encoding")] Spiders_CollectionRulesR CollectionRulesTab)
        {
            CollectionRulesTab tb = new CollectionRulesTab();
            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                if (ModelState.IsValid)
                {
                    tb = db.CollectionRulesTabs.Find(CollectionRulesTab.Id);
                    tb.Id = CollectionRulesTab.Id;
                    tb.CollectionName = CollectionRulesTab.CollectionName;
                    tb.SourceSiteTag = CollectionRulesTab.SourceSiteTag;
                    tb.SourceSiteName = CollectionRulesTab.SourceSiteName;
                    tb.BaseUrl = CollectionRulesTab.BaseUrl;
                    tb.CollectionListRulesBig = CollectionRulesTab.CollectionListRulesBig;
                    tb.CollectionListRulesMin = CollectionRulesTab.CollectionListRulesMin;
                    tb.CollectionUrl = CollectionRulesTab.CollectionUrl;
                    tb.Contentleft = CollectionRulesTab.Contentleft;
                    tb.Contentright = CollectionRulesTab.Contentright;
                    tb.Titleleft = CollectionRulesTab.Titleleft;
                    tb.Titleright = CollectionRulesTab.Titleright;
                    tb.Imgleft = CollectionRulesTab.Imgleft;
                    tb.Imgright = CollectionRulesTab.Imgright;
                    tb.NewPublishTimeLeft = CollectionRulesTab.NewPublishTimeLeft;
                    tb.NewPublishTimeRight = CollectionRulesTab.NewPublishTimeRight;
                    tb.TypeNameLeft = CollectionRulesTab.TypeNameLeft;
                    tb.TypeNameRight = CollectionRulesTab.TypeNameRight;
                    tb.Encoding = CollectionRulesTab.Encoding;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tb);
            }
        }

        // GET: /CollectionRulesTab/Delete/5
        public ActionResult Delete(int? id)
        {
            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CollectionRulesTab CollectionRulesTab = db.CollectionRulesTabs.Find(id);
                if (CollectionRulesTab == null)
                {
                    return HttpNotFound();
                }
                return View(CollectionRulesTab);
            }
        }

        // POST: /CollectionRulesTab/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                CollectionRulesTab CollectionRulesTab = db.CollectionRulesTabs.Find(id);
                CollectionRulesTab.IsDel = 1;
                db.SaveChanges();
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

        // POST: /CollectionRulesTab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SpidersDBEntities db = new SpidersDBEntities())
            {
                CollectionRulesTab CollectionRulesTab = db.CollectionRulesTabs.Find(id);
                db.CollectionRulesTabs.Remove(CollectionRulesTab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
