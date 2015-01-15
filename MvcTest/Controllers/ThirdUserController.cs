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
using MvcTest.Model;
using Common.Configurations;

namespace MvcTest.Controllers
{
    public class ThirdUserController : BaseController
    {
        // GET: /ThirdUser/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<ThirdUser> ThirdUserlist = new List<ThirdUser>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.ThirdUsers.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.Id).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "ThirdUser");
                    if (ids.Length > 0)
                    {
                        return View(db.ThirdUsers.Where(s => s.IsDel == 0 &&
                        ids.Contains(s.Id)).Distinct().OrderByDescending(s => s.Id).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(ThirdUserlist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ThirdUser tUser = db.ThirdUsers.Find(id);
                if (tUser == null)
                {
                    return HttpNotFound();
                }
                return View(tUser);
            }
        }

        // GET: /ThirdUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ThirdUser/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,PassWord,CreatTime,LastTime,ThirdType,ThirdId,Access_token,NiceName,Sex,Province,City,Photo,Expires_in")] ThirdUserR ThirdUser)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    ThirdUser tb = new ThirdUser();
                    tb.UserName = ThirdUser.UserName;
                    tb.PassWord = ThirdUser.PassWord;
                    tb.CreatTime = DateTime.Now;
                    tb.LastTime = DateTime.Now;
                    tb.ThirdType = ThirdUser.ThirdType;
                    tb.ThirdId = ThirdUser.ThirdId;
                    tb.Access_token = ThirdUser.Access_token;
                    tb.NiceName = ThirdUser.NiceName;
                    tb.Sex = ThirdUser.Sex;
                    tb.Province = ThirdUser.Province;
                    tb.City = ThirdUser.City;
                    tb.Photo = ThirdUser.Photo;
                    tb.Expires_in = ThirdUser.Expires_in;
                    db.ThirdUsers.Add(tb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(User);
            }
        }

        // GET: /ThirdUser/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ThirdUser thirdUser = db.ThirdUsers.Find(id);
                if (thirdUser == null)
                {
                    return HttpNotFound();
                }
                ThirdUserR tb = new ThirdUserR();
                tb.Id = thirdUser.Id;
                tb.UserName = thirdUser.UserName;
                tb.PassWord = thirdUser.PassWord;
                tb.LastTime = thirdUser.LastTime ?? DateTime.Now;
                tb.ThirdType = thirdUser.ThirdType;
                tb.ThirdId = thirdUser.ThirdId;
                tb.Access_token = thirdUser.Access_token;
                tb.NiceName = thirdUser.NiceName;
                tb.Sex = thirdUser.Sex;
                tb.Province = thirdUser.Province;
                tb.City = thirdUser.City;
                tb.Photo = thirdUser.Photo;
                tb.Expires_in = thirdUser.Expires_in;
                return View(tb);
            }
        }

        // POST: /ThirdUser/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,PassWord,CreatTime,LastTime,ThirdType,ThirdId,Access_token,NiceName,Sex,Province,City,Photo,Expires_in")] ThirdUserR thirdUser)
        {
            ThirdUser tb = new ThirdUser();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {

                    tb = db.ThirdUsers.Find(thirdUser.Id);
                    tb.Id = thirdUser.Id;
                    tb.UserName = thirdUser.UserName;
                    tb.PassWord = thirdUser.PassWord;
                    tb.LastTime = thirdUser.LastTime;
                    tb.ThirdType = thirdUser.ThirdType;
                    tb.ThirdId = thirdUser.ThirdId;
                    tb.Access_token = thirdUser.Access_token;
                    tb.NiceName = thirdUser.NiceName;
                    tb.Sex = thirdUser.Sex;
                    tb.Province = thirdUser.Province;
                    tb.City = thirdUser.City;
                    tb.Photo = thirdUser.Photo;
                    tb.Expires_in = thirdUser.Expires_in;
                    db.Entry(tb).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tb);
            }
        }

        // GET: /ThirdUser/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ThirdUser User = db.ThirdUsers.Find(id);
                if (User == null)
                {
                    return HttpNotFound();
                }
                return View(User);
            }
        }

        // POST: /User/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ThirdUser User = db.ThirdUsers.Find(id);
                User.IsDel = 1;
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

        // POST: /ThirdUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ThirdUser User = db.ThirdUsers.Find(id);
                db.ThirdUsers.Remove(User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
