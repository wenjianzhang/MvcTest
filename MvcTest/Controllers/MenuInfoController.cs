
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
    public class MenuInfoController : BaseController
    {
        // GET: /MenuInfo/
        public ActionResult Index()
        {
            ViewBag.EName = CurrentUserPermissions;
            List<MenuInfo> MenuInfolist = new List<MenuInfo>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.MenuInfoes.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.MIID).ToList());
                }
                else
                {
                    int[] ids = GetIDS(db, "MenuInfo");
                    if (ids.Length > 0)
                    {
                        return View(db.MenuInfoes.Where(s => s.IsDel == 0 && ids.Contains(s.MIID)).Distinct().OrderByDescending(s => s.MIID).ToList());
                    }
                    else
                    {
                        return View(MenuInfolist.ToList());
                    }
                }

            }
        }

        // GET: /MenuInfo/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MenuInfo MenuInfo = db.MenuInfoes.Find(id);
                if (MenuInfo == null)
                {
                    return HttpNotFound();
                }
                return View(MenuInfo);
            }
        }

        // GET: /MenuInfo/Create
        public ActionResult Create()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.menuinfo = db.MenuInfoes.ToList();
            }
            List<SelectListItem> sl = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "根目录";
            sli1.Value = "0";
            sli1.Selected = true;
            sl.Add(sli1);
            foreach (MenuInfo item in ViewBag.menuinfo)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.Name;
                sli.Value = item.MIID.ToString();
                sl.Add(sli);
            }
            ViewBag.list = sl;
            GetRoleAndManager();
            return View();
        }

        // POST: /MenuInfo/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Url,ParentId,Sort,Depth,IsDisplay,IsMenu")] MenuInfoR MenuInfo)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    MenuInfo tb = new MenuInfo();
                    tb.Name = MenuInfo.Name;
                    tb.Url = MenuInfo.Url;
                    tb.ParentId = int.Parse(MenuInfo.ParentId);
                    tb.Sort = MenuInfo.Sort;
                    tb.Depth = MenuInfo.Depth;
                    //tb.IsDisplay = MenuInfo.IsDisplay;
                    //tb.IsMenu = MenuInfo.IsMenu;
                    tb.IsDel = 0;
                    db.MenuInfoes.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.MIID, "MenuInfo");
                    }
                    return RedirectToAction("Index");
                }

                return View(MenuInfo);
            }
        }

        // GET: /MenuInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MenuInfo MenuInfo = db.MenuInfoes.Find(id);


                ViewBag.menuinfo = db.MenuInfoes.ToList();
                List<SelectListItem> sl = new List<SelectListItem>();
                SelectListItem sli1 = new SelectListItem();
                sli1.Text = "根目录";
                sli1.Value = "0";
                sli1.Selected = true;
                sl.Add(sli1);
                foreach (MenuInfo item in ViewBag.menuinfo)
                {
                    SelectListItem sli = new SelectListItem();
                    if (item.MIID == id)
                    {
                        sli.Selected = true;
                        sli1.Selected = false;
                    }
                    sli.Text = item.Name;
                    sli.Value = item.MIID.ToString();
                    sl.Add(sli);
                }
                ViewBag.list = sl;

                GetRoleAndManager(MenuInfo.MIID, "MenuInfo");
                if (MenuInfo == null)
                {
                    return HttpNotFound();
                }
                return View(MenuInfo);
            }
        }

        // POST: /MenuInfo/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "MIID,Name,Url,ParentId,Sort,Depth,IsDisplay,IsMenu")] MenuInfoR MenuInfo)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (ModelState.IsValid)
                {
                    MenuInfo tb = db.MenuInfoes.Find(id);
                    tb.Name = MenuInfo.Name;
                    tb.Url = MenuInfo.Url;
                    tb.ParentId = int.Parse(MenuInfo.ParentId);
                    tb.Sort = MenuInfo.Sort;
                    tb.Depth = MenuInfo.Depth;
                    //tb.IsDisplay = MenuInfo.IsDisplay;
                    //tb.IsMenu = MenuInfo.IsMenu;

                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == MenuInfo.MIID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, MenuInfo.MIID, "MenuInfo");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改MenuInfo id " + MenuInfo.MIID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(MenuInfo);
            }
        }

        // GET: /MenuInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MenuInfo MenuInfo = db.MenuInfoes.Find(id);
                if (MenuInfo == null)
                {
                    return HttpNotFound();
                }
                return View(MenuInfo);
            }
        }

        // POST: /MenuInfo/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                MenuInfo MenuInfo = db.MenuInfoes.Find(id);
                MenuInfo.IsDel = 1;
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

        // POST: /MenuInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                MenuInfo MenuInfo = db.MenuInfoes.Find(id);
                db.MenuInfoes.Remove(MenuInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        // GET: /MenuInfo/SetAction/5
        public ActionResult SetAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                List<PagePowerSignPublic> PagePowerSignPublic = db.PagePowerSignPublics.ToList();
                ViewBag.setMIID = id;

                ViewBag.PagePowerSigns = db.PagePowerSigns.Where(s => s.MIID == id).ToList();
                return View(PagePowerSignPublic);
            }
        }
        [HttpPost]
        [ShowPostWindowFilterAttribute]
        public ActionResult UpdatePagePowerSignPublicTableActions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {

                db.PagePowerSigns.RemoveRange(db.PagePowerSigns.Where(s => s.MIID == id));


                List<PagePowerSignPublic> PagePowerSignPublic = db.PagePowerSignPublics.ToList();

                foreach (var item in PagePowerSignPublic)
                {
                    PagePowerSign PagePowerSign = new PagePowerSign();
                    string str = Request.Form["MP_" + item.PPSPID.ToString()].ToString().Split(',')[0];
                    if (str != "false")
                    {
                        PagePowerSign.MIID = id;
                        PagePowerSign.PPSPID = item.PPSPID;
                        db.PagePowerSigns.Add(PagePowerSign);

                    }

                }
                db.SaveChanges();
                return Content("OK");
            }
        }
    }
}