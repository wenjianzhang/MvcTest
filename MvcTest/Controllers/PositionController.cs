
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
    public class PositionController : BaseController
    {
        // GET: /Position/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<Position> Positionlist = new List<Position>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.Positions.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.PID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "Position");
                    if (ids.Length > 0)
                    {
                        return View(db.Positions.Where(s => s.IsDel == 0 && ids.Contains(s.PID)).Distinct().OrderByDescending(s => s.PID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(Positionlist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /Position/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Position Position = db.Positions.Find(id);
                if (Position == null)
                {
                    return HttpNotFound();
                }
                return View(Position);
            }
        }

        // GET: /Position/Create
        public ActionResult Create()
        {
            GetRoleAndManager();
            return View();
        }

        // POST: /Position/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PID,Name,Branch_Id,Branch_Code,Branch_Name,PagePower,ControlPower,IsSetBranchPower,SetBranchCode,Manager_Id,Manager_CName,UpdateDate")] PositionR Position)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    Position tb = new Position();
                    tb.Name = Position.Name;
                    tb.Branch_Id = Position.Branch_Id;
                    tb.Branch_Code = Position.Branch_Code;
                    tb.Branch_Name = Position.Branch_Name;
                    tb.PagePower = Position.PagePower;
                    tb.ControlPower = Position.ControlPower;
                    //tb.IsSetBranchPower = Position.IsSetBranchPower;
                    tb.SetBranchCode = Position.SetBranchCode;
                    tb.Manager_Id = Position.Manager_Id;
                    tb.Manager_CName = Position.Manager_CName;
                    tb.UpdateDate = Position.UpdateDate;
                    tb.IsDel = 0;
                    db.Positions.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.PID, "Position");
                    }
                    return RedirectToAction("Index");
                }

                return View(Position);
            }
        }

        // GET: /Position/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Position Position = db.Positions.Find(id);
                if (Position == null)
                {
                    return HttpNotFound();
                }
                GetRoleAndManager(Position.PID, "Position");
                return View(Position);
            }
        }

        // POST: /Position/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PID,Name,Branch_Id,Branch_Code,Branch_Name,PagePower,ControlPower,IsSetBranchPower,SetBranchCode,Manager_Id,Manager_CName,UpdateDate")] PositionR Position)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Position tb = new Position();
                if (ModelState.IsValid)
                {
                    tb = db.Positions.Find(Position.PID);
                    tb.PID = Position.PID;
                    tb.Name = Position.Name;
                    tb.Branch_Id = Position.Branch_Id;
                    tb.Branch_Code = Position.Branch_Code;
                    tb.Branch_Name = Position.Branch_Name;
                    tb.PagePower = Position.PagePower;
                    tb.ControlPower = Position.ControlPower;
                    // tb.IsSetBranchPower = Position.IsSetBranchPower;
                    tb.SetBranchCode = Position.SetBranchCode;
                    tb.Manager_Id = Position.Manager_Id;
                    tb.Manager_CName = Position.Manager_CName;
                    tb.UpdateDate = Position.UpdateDate;
                    tb.IsDel = Position.IsDel;
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == Position.PID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, Position.PID, "Position");

                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改Position id " + Position.PID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(Position);
            }
        }

        // GET: /Position/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Position Position = db.Positions.Find(id);
                if (Position == null)
                {
                    return HttpNotFound();
                }
                return View(Position);
            }
        }

        // POST: /Position/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Position Position = db.Positions.Find(id);
                Position.IsDel = 1;
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

        // POST: /Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Position Position = db.Positions.Find(id);
                db.Positions.Remove(Position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
