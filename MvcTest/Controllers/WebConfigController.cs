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
    public class WebConfigController : BaseController
    {
        // GET: /WebConfig/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<WebConfig> WebConfiglist = new List<WebConfig>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.WebConfigs.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.WCID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "WebConfig");
                    if (ids.Length > 0)
                    {
                        return View(db.WebConfigs.Where(s => s.IsDel == 0 && ids.Contains(s.WCID)).Distinct().OrderByDescending(s => s.WCID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(WebConfiglist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /WebConfig/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WebConfig webconfig = db.WebConfigs.Find(id);
                if (webconfig == null)
                {
                    return HttpNotFound();
                }
                return View(webconfig);
            }
        }

        // GET: /WebConfig/Create
        public ActionResult Create()
        {
            GetRoleAndManager();
            return View();
        }

        // POST: /WebConfig/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WebName,WebDomain,WebEmail,EmailSmtp,EmailUserName,EmailPassWord,EmailDomain")] WebConfigR webconfig)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    WebConfig tb = new WebConfig();
                    tb.WebName = webconfig.WebName;
                    tb.WebDomain = webconfig.WebDomain;
                    tb.WebEmail = webconfig.WebEmail;
                    tb.EmailSmtp = webconfig.EmailSmtp;
                    tb.EmailUserName = webconfig.EmailUserName;
                    tb.EmailPassWord = webconfig.EmailPassWord;
                    tb.EmailDomain = webconfig.EmailDomain;
                    tb.IsDel = 0;
                    db.WebConfigs.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.WCID, "WebConfig");
                    }
                    return RedirectToAction("Index");
                }

                return View(webconfig);
            }
        }

        // GET: /WebConfig/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WebConfig webconfig = db.WebConfigs.Find(id);
                WebConfigR WC = new WebConfigR();
                WC.WCID = webconfig.WCID;
                WC.WebName = webconfig.WebName;
                WC.WebDomain = webconfig.WebDomain;
                WC.WebEmail = webconfig.WebEmail;
                WC.EmailSmtp = webconfig.EmailSmtp;
                WC.EmailUserName = webconfig.EmailUserName;
                WC.EmailPassWord = webconfig.EmailPassWord;
                WC.EmailDomain = webconfig.EmailDomain;
                if (webconfig == null)
                {
                    return HttpNotFound();
                }
                GetRoleAndManager(webconfig.WCID, "WebConfig");
                return View(WC);
            }
        }

        // POST: /WebConfig/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WCID,WebName,WebDomain,WebEmail,LoginLogReserveTime,UseLogReserveTime,EmailSmtp,EmailUserName,EmailPassWord,EmailDomain")] WebConfigR WebConfig)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    WebConfig WC = db.WebConfigs.Find(WebConfig.WCID);
                    WC.WCID = WebConfig.WCID;
                    WC.WebName = WebConfig.WebName;
                    WC.WebDomain = WebConfig.WebDomain;
                    WC.WebEmail = WebConfig.WebEmail;
                    WC.EmailSmtp = WebConfig.EmailSmtp;
                    WC.EmailUserName = WebConfig.EmailUserName;
                    WC.EmailPassWord = WebConfig.EmailPassWord;
                    WC.EmailDomain = WebConfig.EmailDomain;
                    db.Entry(WC).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == WebConfig.WCID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, WebConfig.WCID, "WebConfig");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改WebConfig id " + WebConfig.WCID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(WebConfig);
            }
        }

        // GET: /WebConfig/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                WebConfig webconfig = db.WebConfigs.Find(id);
                if (webconfig == null)
                {
                    return HttpNotFound();
                }
                return View(webconfig);
            }
        }

        // POST: /WebConfig/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                WebConfig WebConfig = db.WebConfigs.Find(id);
                WebConfig.IsDel = 1;
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

        // POST: /WebConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                WebConfig webconfig = db.WebConfigs.Find(id);
                db.WebConfigs.Remove(webconfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
