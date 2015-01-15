
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
using MvcTest.Filters;
using Common.Configurations;

namespace MvcTest.Controllers
{
    public class RoleController : BaseController
    {
        // GET: /Role/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<Role> Rolelist = new List<Role>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.Roles.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.RID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "Role");
                    if (ids.Length > 0)
                    {
                        return View(db.Roles.Where(s => s.IsDel == 0 && ids.Contains(s.RID)).Distinct().OrderByDescending(s => s.RID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(Rolelist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /Role/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role Role = db.Roles.Find(id);
                if (Role == null)
                {
                    return HttpNotFound();
                }
                return View(Role);
            }
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            GetRoleAndManager();
            return View();
        }

        // POST: /Role/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RID,Name")] RoleR Role)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    Role tb = new Role();
                    tb.Name = Role.Name;
                    tb.IsDel = 0;
                    db.Roles.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.RID, "Role");
                    }
                    return RedirectToAction("Index");
                }
               
                return View(Role);
            }
        }

        // GET: /Role/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role Role = db.Roles.Find(id);
                if (Role == null)
                {
                    return HttpNotFound();
                }
                GetRoleAndManager(Role.RID, "Role");
                return View(Role);
            }
        }

        // POST: /Role/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RID,Name")] RoleR Role)
        {
            Role tb = new Role();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    tb = db.Roles.Find(Role.RID);
                    tb.RID = Role.RID;
                    tb.Name = Role.Name;
                    tb.IsDel = Role.IsDel;
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == Role.RID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, Role.RID, "Role");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改Role id " + Role.RID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(Role);
            }
        }

        // GET: /Role/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role Role = db.Roles.Find(id);
                if (Role == null)
                {
                    return HttpNotFound();
                }
                return View(Role);
            }
        }

        // POST: /Role/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Role Role = db.Roles.Find(id);
                Role.IsDel = 1;
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

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Role Role = db.Roles.Find(id);

                db.Roles.Remove(Role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult MenuInfoTable(int? rid)
        {
            ViewBag.setRID = rid;
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (rid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Roles.Find(rid);
                ViewBag.rolemenurpages = db.RoleMenuPages.Where(s => s.RID == rid).ToList();
                ViewBag.setRNname = role.Name;
                
                return View(db.MenuInfoes.OrderByDescending(s => s.MIID).ToList());
            }
        }

        public ActionResult MenuInfoTableAction(int? rid, int? mid)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (rid == null || mid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                MenuInfo meninfo = db.MenuInfoes.Find(mid);
                ViewBag.setMID = mid;
                ViewBag.setMNname = meninfo.Name;
                Role role = db.Roles.Find(rid);
                ViewBag.setRID = rid;
                ViewBag.setRNname = role.Name;

                return View(db.PagePowerSignPublics.OrderByDescending(s => s.PPSPID).ToList());
            }
        }

        public ActionResult UpdateMenuInfoTableAction(int? rid, int? mid)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (rid == null || mid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.RoleMenuPages.RemoveRange(db.RoleMenuPages.Where(s => s.MIID == mid && s.RID == rid));

                MenuInfo meninfo = db.MenuInfoes.Find(mid);
                ViewBag.setMID = mid;
                ViewBag.setMNname = meninfo.Name;
                List<PagePowerSignPublic> list = db.PagePowerSignPublics.OrderByDescending(s => s.PPSPID).ToList();


                foreach (var item in list)
                {
                    RoleMenuPage newrmg = new RoleMenuPage();
                    string str = Request.Form[item.PPSPID.ToString()].ToString().Split(',')[0];
                    if (str != "false")
                    {
                        newrmg.RID = rid;
                        newrmg.MIID = mid;
                        newrmg.PPSID = item.PPSPID;
                        db.RoleMenuPages.Add(newrmg);
                        db.SaveChanges();
                    }
                }

                return View();
            }
        }

        [HttpPost]
        [ShowPostWindowFilterAttribute]
        public ActionResult UpdateMenuInfoTableActions(int? rid)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (rid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.RoleMenuPages.RemoveRange(db.RoleMenuPages.Where(s => s.RID == rid));

                //MenuInfo meninfo = db.MenuInfoes.Find(mid);
                //ViewBag.setMID = mid;
                //ViewBag.setMNname = meninfo.Name;

                List<MenuInfo> meninfo = db.MenuInfoes.ToList();
                List<PagePowerSignPublic> list = db.PagePowerSignPublics.OrderByDescending(s => s.PPSPID).ToList();

                foreach (var itemmi in meninfo)
                {
                    if (db.MenuInfoes.Where(s => s.ParentId == itemmi.MIID).Count() == 0)
                    {
                        int[] PPSPIDs = db.PagePowerSigns.Where(s => s.MIID == itemmi.MIID).Select(s => s.PPSPID ?? 0).ToArray();
                        foreach (var item in list.Where(s=>PPSPIDs.Contains(s.PPSPID)))
                        {
                            RoleMenuPage newrmg = new RoleMenuPage();
                            string str = Request.Form["MA_" + itemmi.MIID + "_" + item.PPSPID.ToString()].ToString().Split(',')[0];
                            if (str != "false")
                            {
                                newrmg.RID = rid;
                                newrmg.MIID = itemmi.MIID;
                                newrmg.PPSID = item.PPSPID;
                                db.RoleMenuPages.Add(newrmg);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return Content("OK");
            }
        }
    }
}
