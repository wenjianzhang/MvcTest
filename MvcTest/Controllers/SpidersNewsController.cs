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
    public class SpidersNewsController : BaseController
    {
        // GET: SpidersNews
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<NewsTab> NewsTablist = new List<NewsTab>();
            using (var db = new SpidersDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.NewsTabs.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.Id).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS("NewsTab");
                    if (ids.Length > 0)
                    {
                        
                        return View(db.NewsTabs.Where(s => ids.Contains((int)s.Id)).Distinct().OrderByDescending(s => s.Id).ToPagedList(pageindex, Configuration.Default.PageSize));

                    }
                    else
                    {
                        return View(NewsTablist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /NewsTab/Details/5
        public ActionResult Details(int? id)
        {
            using (var db = new SpidersDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NewsTab NewsTab = db.NewsTabs.Find(id);
                if (NewsTab == null)
                {
                    return HttpNotFound();
                }
                return View(NewsTab);
            }
        }

        // GET: /NewsTab/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /NewsTab/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SourceSiteTag,SourceSiteName,Title,TypeName,NewPublishTime,BaseUrl,Url,ImgUrl,Content,CreateTime,IsDel")] Spiders_NewsR NewsTab)
        {
            using (var db = new SpidersDBEntities())
            {
                if (ModelState.IsValid)
                {
                    NewsTab tb = new NewsTab();
                    tb.SourceSiteTag = NewsTab.SourceSiteTag;
                    tb.SourceSiteName = NewsTab.SourceSiteName;
                    tb.Title = NewsTab.Title;
                    tb.TypeName = NewsTab.TypeName;
                    tb.NewPublishTime = NewsTab.NewPublishTime;
                    tb.BaseUrl = NewsTab.BaseUrl;
                    tb.Url = NewsTab.Url;
                    tb.ImgUrl = NewsTab.ImgUrl;
                    tb.Content = NewsTab.Content;
                    tb.CreateTime = DateTime.Now;
                    tb.IsDel = 0;
                    db.NewsTabs.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(NewsTab);
            }
        }

        // GET: /NewsTab/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var db = new SpidersDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NewsTab NewsTab = db.NewsTabs.Find(id);
                if (NewsTab == null)
                {
                    return HttpNotFound();
                }
                var tb = new Spiders_NewsR();
                tb.Id = NewsTab.Id;
                tb.SourceSiteTag = NewsTab.SourceSiteTag;
                tb.SourceSiteName = NewsTab.SourceSiteName;
                tb.Title = NewsTab.Title;
                tb.TypeName = NewsTab.TypeName;
                tb.NewPublishTime = NewsTab.NewPublishTime ?? DateTime.Now;
                tb.BaseUrl = NewsTab.BaseUrl;
                tb.Url = NewsTab.Url;
                tb.ImgUrl = NewsTab.ImgUrl;
                tb.Content = NewsTab.Content;
                return View(tb);
            }
        }

        // POST: /NewsTab/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SourceSiteTag,SourceSiteName,Title,TypeName,NewPublishTime,BaseUrl,Url,ImgUrl,Content,CreateTime,IsDel")] Spiders_NewsR NewsTab)
        {
            NewsTab tb = new NewsTab();
            using (var db = new SpidersDBEntities())
            {
                if (ModelState.IsValid)
                {
                    tb = db.NewsTabs.Find(NewsTab.Id);
                    tb.Id = NewsTab.Id;
                    tb.SourceSiteTag = NewsTab.SourceSiteTag;
                    tb.SourceSiteName = NewsTab.SourceSiteName;
                    tb.Title = NewsTab.Title;
                    tb.TypeName = NewsTab.TypeName;
                    tb.NewPublishTime = NewsTab.NewPublishTime;
                    tb.BaseUrl = NewsTab.BaseUrl;
                    tb.Url = NewsTab.Url;
                    tb.ImgUrl = NewsTab.ImgUrl;
                    tb.Content = NewsTab.Content;
                    tb.CreateTime = NewsTab.CreateTime;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tb);
            }
        }

        // GET: /NewsTab/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var db = new SpidersDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NewsTab NewsTab = db.NewsTabs.Find(id);
                if (NewsTab == null)
                {
                    return HttpNotFound();
                }
                return View(NewsTab);
            }
        }

        // POST: /NewsTab/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (var db = new SpidersDBEntities())
            {
                NewsTab NewsTab = db.NewsTabs.Find(id);
                NewsTab.IsDel = 1;
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

        // POST: /NewsTab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new SpidersDBEntities())
            {
                NewsTab NewsTab = db.NewsTabs.Find(id);
                db.NewsTabs.Remove(NewsTab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
