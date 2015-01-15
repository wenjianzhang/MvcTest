
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
    public class EmailSendHistoryController : BaseController
    {
        // GET: /EmailSendHistory/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                return View(db.EmailSendHistories.Where(s => s.IsDel == 0).OrderByDescending(s => s.Id).ToPagedList(pageindex, 8));
            }
        }

        // GET: /EmailSendHistory/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmailSendHistory EmailSendHistory = db.EmailSendHistories.Find(id);
                if (EmailSendHistory == null)
                {
                    return HttpNotFound();
                }
                return View(EmailSendHistory);
            }
        }

        // GET: /EmailSendHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EmailSendHistory/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SendUsers_Id,SendUsers_Name,SendUsers_Email,RecUsers_Id,RecUsers_Name,RecUsers_Email,RecUserLevel_Id,RecUserLevel_Name,EmailSubject,EmailContent,SendDate,Status,StatusName,ErrorMsg,Template_Id,Template_Name")] EmailSendHistoryR EmailSendHistory)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    EmailSendHistory tb = new EmailSendHistory();
                    tb.SendUsers_Id = EmailSendHistory.SendUsers_Id;
                    tb.SendUsers_Name = EmailSendHistory.SendUsers_Name;
                    tb.SendUsers_Email = EmailSendHistory.SendUsers_Email;
                    tb.RecUsers_Id = EmailSendHistory.RecUsers_Id;
                    tb.RecUsers_Name = EmailSendHistory.RecUsers_Name;
                    tb.RecUsers_Email = EmailSendHistory.RecUsers_Email;
                    tb.RecUserLevel_Id = EmailSendHistory.RecUserLevel_Id;
                    tb.RecUserLevel_Name = EmailSendHistory.RecUserLevel_Name;
                    tb.EmailSubject = EmailSendHistory.EmailSubject;
                    tb.EmailContent = EmailSendHistory.EmailContent;
                    tb.SendDate = EmailSendHistory.SendDate;
                    //tb.Status = EmailSendHistory.Status;
                    tb.StatusName = EmailSendHistory.StatusName;
                    tb.ErrorMsg = EmailSendHistory.ErrorMsg;
                    tb.Template_Id = EmailSendHistory.Template_Id;
                    tb.Template_Name = EmailSendHistory.Template_Name;
                    tb.IsDel = 0;
                    db.EmailSendHistories.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(EmailSendHistory);
            }
        }

        // GET: /EmailSendHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmailSendHistory EmailSendHistory = db.EmailSendHistories.Find(id);
                if (EmailSendHistory == null)
                {
                    return HttpNotFound();
                }
                return View(EmailSendHistory);
            }
        }

        // POST: /EmailSendHistory/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SendUsers_Id,SendUsers_Name,SendUsers_Email,RecUsers_Id,RecUsers_Name,RecUsers_Email,RecUserLevel_Id,RecUserLevel_Name,EmailSubject,EmailContent,SendDate,Status,StatusName,ErrorMsg,Template_Id,Template_Name")] EmailSendHistory EmailSendHistory)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(EmailSendHistory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(EmailSendHistory);
            }
        }

        // GET: /EmailSendHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmailSendHistory EmailSendHistory = db.EmailSendHistories.Find(id);
                if (EmailSendHistory == null)
                {
                    return HttpNotFound();
                }
                return View(EmailSendHistory);
            }
        }

        // POST: /EmailSendHistory/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                EmailSendHistory EmailSendHistory = db.EmailSendHistories.Find(id);
                EmailSendHistory.IsDel = 1;
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

        // POST: /EmailSendHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                EmailSendHistory EmailSendHistory = db.EmailSendHistories.Find(id);
                db.EmailSendHistories.Remove(EmailSendHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
