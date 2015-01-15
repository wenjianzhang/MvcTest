
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
    public class BranchController : BaseController
    {
        // GET: /Branch/

        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<Branch> Branchlist = new List<Branch>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.Branches.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.BID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "Branch");
                    if (ids.Length > 0)
                    {
                        return View(db.Branches.Where(s => s.IsDel == 0 && ids.Contains(s.BID)).Distinct().OrderByDescending(s => s.BID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(Branchlist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }
            }
        }


        // GET: /Branch/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Branch Branch = db.Branches.Find(id);
                if (Branch == null)
                {
                    return HttpNotFound();
                }
                return View(Branch);
            }
        }

        // GET: /Branch/Create
        public ActionResult Create()
        {

            List<Branch> Branches = new List<Branch>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Branches = db.Branches.Where(s => s.IsDel == 0).ToList();
            }
            List<SelectListItem> sl = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "请选择";
            sli1.Value = "0";
            sli1.Selected = true;
            sl.Add(sli1);
            foreach (Branch item in Branches)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.Name;
                sli.Value = item.BID.ToString();
                sl.Add(sli);
            }
            ViewBag.list = sl;
            GetRoleAndManager();
            return View();
        }



        // POST: /Branch/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Name,Notes,ParentId,Sort,Depth")] BranchR Branch)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    Branch tb = new Branch();
                    tb.Code = Branch.Code;
                    tb.Name = Branch.Name;
                    tb.Notes = Branch.Notes;
                    tb.ParentId = Branch.ParentId;
                    tb.Sort = Branch.Sort;
                    tb.Depth = Branch.Depth;
                    tb.Manager_Id = CurrentUser.MID;
                    tb.Manager_CName = CurrentUser.CName;
                    tb.UpdateDate = DateTime.Now;
                    tb.IsDel = 0;
                    db.Branches.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.BID, "Branch");
                    }
                    return RedirectToAction("Index");
                }
                return View(Branch);
            }
        }


        // GET: /Branch/Edit/5
        public ActionResult Edit(int? id)
        {
            List<Branch> Branches = new List<Branch>();

            Branch Branch = new Branch();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {

                Branch = db.Branches.Find(id);
                Branches = db.Branches.Where(s => s.BID != id).ToList();
            }


            List<SelectListItem> sl = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "根目录";
            sli1.Value = "0";
            sli1.Selected = true;
            sl.Add(sli1);
            foreach (Branch item in Branches)
            {
                SelectListItem sli = new SelectListItem();
                if (item.BID == Branch.BID)
                {
                    sli.Selected = true;
                    sli1.Selected = false;
                }
                sli.Text = item.Name;
                sli.Value = item.BID.ToString();
                sl.Add(sli);
            }
            ViewBag.list = sl;


            GetRoleAndManager(Branch.BID, "Branch");

            if (Branch == null)
            {
                return HttpNotFound();
            }
            return View(Branch);
        }



        // POST: /Branch/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "BID,Code,Name,Notes,ParentId,Sort,Depth")] BranchR Branch)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    Branch tb = db.Branches.Find(id);
                    tb.Code = Branch.Code;
                    tb.Name = Branch.Name;
                    tb.Notes = Branch.Notes;
                    tb.ParentId = Branch.ParentId;
                    tb.Sort = Branch.Sort;
                    tb.Depth = Branch.Depth;
                    tb.Manager_Id = CurrentUser.MID;
                    tb.Manager_CName = CurrentUser.CName;
                    tb.UpdateDate = DateTime.Now;
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == id && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, tb.BID, "Branch");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改Branch id " + id, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }

                    return RedirectToAction("Index");
                }
                return View(Branch);
            }
        }

        // GET: /Branch/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Branch Branch = db.Branches.Find(id);
                if (Branch == null)
                {
                    return HttpNotFound();
                }
                return View(Branch);
            }
        }

        // POST: /Branch/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Branch Branch = db.Branches.Find(id);
                Branch.IsDel = 1;
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

        // POST: /Branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Branch Branch = db.Branches.Find(id);
                db.Branches.Remove(Branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
