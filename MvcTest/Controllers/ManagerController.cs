
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
    public class ManagerController : BaseController
    {
        // GET: /Manager/
        public ActionResult Index(int pageindex = 1, string p = "")
        {
            ViewBag.p = p;
            ViewBag.EName = CurrentUserPermissions;
            List<Manager> Managerlist = new List<Manager>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    if (p != "")
                    {
                        return View(db.Managers.Where(s => s.IsDel == 0).Where(s => s.LoginName.IndexOf(p) != -1 || s.Manager_CName.IndexOf(p) != -1 || s.Mobile.IndexOf(p) != -1).Distinct().OrderByDescending(s => s.MID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(db.Managers.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.MID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }
                else
                {
                    int[] ids = GetIDS(db, "Manager");
                    if (ids.Length > 0)
                    {
                        if (p != "")
                        {
                            return View(db.Managers.Where(s => s.IsDel == 0 && (ids.Contains(s.MID) || s.MID == CurrentUser.MID)).Where(s => s.LoginName.IndexOf(p) != -1 || s.Manager_CName.IndexOf(p) != -1 || s.Mobile.IndexOf(p) != -1).Distinct().OrderByDescending(s => s.MID).ToPagedList(pageindex, Configuration.Default.PageSize));
                        }
                        else
                        {
                            return View(db.Managers.Where(s => s.IsDel == 0 && (ids.Contains(s.MID) || s.MID == CurrentUser.MID)).Distinct().OrderByDescending(s => s.MID).ToPagedList(pageindex, Configuration.Default.PageSize));
                        }
                    }
                    else
                    {
                        return View(Managerlist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /Manager/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Manager Manager = db.Managers.Find(id);
                if (Manager == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Branche = db.Branches.ToList();
                ViewBag.Position = db.Positions.ToList();
                List<SelectListItem> sl = new List<SelectListItem>();
                List<SelectListItem> s2 = new List<SelectListItem>();
                SelectListItem sli1 = new SelectListItem();
                sli1.Text = "选择部门";
                sli1.Value = "0";
                sli1.Selected = true;
                SelectListItem sli2 = new SelectListItem();
                sli2.Text = "选择职位";
                sli2.Value = "0";
                sli2.Selected = true;
                sl.Add(sli1);
                s2.Add(sli2);
                foreach (Branch item in ViewBag.Branche)
                {
                    SelectListItem sli = new SelectListItem();
                    if (item.BID == Manager.Branch_Id)
                    {
                        sli.Selected = true;
                        sli1.Selected = false;
                    }
                    sli.Text = item.Name + "-" + item.Code;
                    sli.Value = item.BID.ToString();
                    sl.Add(sli);
                }
                foreach (Position item in ViewBag.Position)
                {
                    SelectListItem sli = new SelectListItem();
                    if (item.PID == Manager.Position_Id)
                    {
                        sli.Selected = true;
                        sli1.Selected = false;
                    }
                    sli.Text = item.Name;
                    sli.Value = item.PID.ToString();
                    s2.Add(sli);
                }
                ViewBag.Branchlist = sl;
                ViewBag.Positionlist = s2;

                return View(Manager);
            }
        }

        // GET: /Manager/Details
        public ActionResult PersonalInfo()
        {
            if (this.User == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int mid = -1;
            try
            {
                mid = CurrentUser.MID;
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Login");
            }
            Manager Manager = new Manager();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {

                Manager = db.Managers.Find(mid);
                if (Manager == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Branche = db.Branches.ToList();
                ViewBag.Position = db.Positions.ToList();
            }
            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> s2 = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "选择部门";
            sli1.Value = "0";
            sli1.Selected = true;
            SelectListItem sli2 = new SelectListItem();
            sli2.Text = "选择职位";
            sli2.Value = "0";
            sli2.Selected = true;
            sl.Add(sli1);
            s2.Add(sli2);
            foreach (Branch item in ViewBag.Branche)
            {
                SelectListItem sli = new SelectListItem();
                if (item.BID == Manager.Branch_Id)
                {
                    sli.Selected = true;
                    sli1.Selected = false;
                }
                sli.Text = item.Name + "-" + item.Code;
                sli.Value = item.BID.ToString();
                sl.Add(sli);
            }
            foreach (Position item in ViewBag.Position)
            {
                SelectListItem sli = new SelectListItem();
                if (item.PID == Manager.Position_Id)
                {
                    sli.Selected = true;
                    sli1.Selected = false;
                }
                sli.Text = item.Name;
                sli.Value = item.PID.ToString();
                s2.Add(sli);
            }
            ViewBag.Branchlist = sl;
            ViewBag.Positionlist = s2;
            ManagerR tb = new ManagerR();
            tb.MID = Manager.MID;
            tb.LoginName = Manager.LoginName;
            tb.LoginPass = Manager.LoginPass;
            tb.LoginIp = Manager.LoginIp;
            tb.LoginCount = 0;
            tb.Branch_Id = Manager.Branch_Id;
            tb.Branch_Code = Manager.Branch_Code;
            tb.Branch_Name = Manager.Branch_Name;
            tb.Position_Id = Manager.Position_Id;
            tb.Position_Name = Manager.Position_Name;
            tb.Enable = (int)Manager.Enable;
            tb.CName = Manager.CName;
            tb.EName = Manager.EName;
            tb.PhotoImg = Manager.PhotoImg;
            tb.Sex = Manager.Sex;
            tb.Birthday = Manager.Birthday;
            tb.NativePlace = Manager.NativePlace;
            tb.NationalName = Manager.NationalName;
            tb.Record = Manager.Record;
            tb.GraduateCollege = Manager.GraduateCollege;
            tb.GraduateSpecialty = Manager.GraduateSpecialty;
            tb.Tel = Manager.Tel;
            tb.Mobile = Manager.Mobile;
            tb.Email = Manager.Email;
            tb.Qq = Manager.Qq;
            tb.Msn = Manager.Msn;
            tb.Address = Manager.Address;
            tb.Content = Manager.Content;
            tb.Manager_Id = Manager.Manager_Id;
            tb.Manager_CName = Manager.Manager_CName;
            return View(tb);

        }

        // GET: /Manager/Create
        public ActionResult Create()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.Branche = db.Branches.ToList();
                ViewBag.Position = db.Positions.ToList();

            }
            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> s2 = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "选择部门";
            sli1.Value = "0";
            sli1.Selected = true;
            SelectListItem sli2 = new SelectListItem();
            sli2.Text = "选择职位";
            sli2.Value = "0";
            sli2.Selected = true;
            sl.Add(sli1);
            s2.Add(sli2);
            foreach (Branch item in ViewBag.Branche)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.Name + "-" + item.Code;
                sli.Value = item.BID.ToString();
                sl.Add(sli);
            }
            foreach (Position item in ViewBag.Position)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.Name;
                sli.Value = item.PID.ToString();
                s2.Add(sli);
            }
            ViewBag.Branchlist = sl;
            ViewBag.Positionlist = s2;

            GetRoleAndManager();
            return View();
        }

        // POST: /Manager/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MID,LoginName,LoginPass,LoginTime,LoginIp,LoginCount,CreateTime,UpdateTime,Branch_Id,Branch_Code,Branch_Name,Position_Id,Position_Name,Enable,CName,EName,PhotoImg,Sex,Birthday,NativePlace,NationalName,Record,GraduateCollege,GraduateSpecialty,Tel,Mobile,Email,Qq,Msn,Address,Content,Manager_Id,Manager_CName")] ManagerR Manager)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    Manager tb = new Manager();
                    tb.LoginName = Manager.LoginName;
                    tb.LoginPass = Manager.LoginPass;
                    tb.LoginTime = null;
                    tb.LoginIp = Manager.LoginIp;
                    tb.LoginCount = 0;
                    tb.Branch_Id = Manager.Branch_Id;
                    if (tb.Branch_Id != 0)
                    {
                        Branch Branch = db.Branches.Find(Manager.Branch_Id);
                        tb.Branch_Code = Branch.Code;
                        tb.Branch_Name = Branch.Name;
                    }
                    else
                    {
                        tb.Branch_Code = "";
                        tb.Branch_Name = "";
                    }
                    tb.Position_Id = Manager.Position_Id;
                    if (tb.Position_Id != 0)
                    {
                        Position Position = db.Positions.Find(tb.Position_Id);
                        tb.Position_Name = Position.Name;
                    }
                    else
                    {
                        tb.Position_Name = "";
                    }
                    tb.Enable = (byte)Manager.Enable;
                    tb.CName = Manager.CName;
                    tb.EName = Manager.EName;
                    tb.PhotoImg = Manager.PhotoImg;
                    tb.Sex = Manager.Sex;
                    tb.Birthday = Manager.Birthday;
                    tb.NativePlace = Manager.NativePlace;
                    tb.NationalName = Manager.NationalName;
                    tb.Record = Manager.Record;
                    tb.GraduateCollege = Manager.GraduateCollege;
                    tb.GraduateSpecialty = Manager.GraduateSpecialty;
                    tb.Tel = Manager.Tel;
                    tb.Mobile = Manager.Mobile;
                    tb.Email = Manager.Email;
                    tb.Qq = Manager.Qq;
                    tb.Msn = Manager.Msn;
                    tb.Address = Manager.Address;
                    tb.Content = Manager.Content;
                    tb.Manager_Id = CurrentUser.MID;
                    tb.Manager_CName = CurrentUser.CName;
                    tb.CreateTime = DateTime.Now;
                    tb.UpdateTime = DateTime.Now;
                    tb.IsDel = 0;
                    db.Managers.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.MID, "Manager");
                    }
                    return RedirectToAction("Index");
                }

                return View(Manager);
            }
        }

        // GET: /Manager/Edit/5
        public ActionResult Edit(int? id)
        {
            Manager Manager = new Manager();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {

                Manager = db.Managers.Find(id);
                ViewBag.Branche = db.Branches.ToList();
                ViewBag.Position = db.Positions.ToList();
            }

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> s2 = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "选择部门";
            sli1.Value = "0";
            sli1.Selected = true;
            SelectListItem sli2 = new SelectListItem();
            sli2.Text = "选择职位";
            sli2.Value = "0";
            sli2.Selected = true;
            sl.Add(sli1);
            s2.Add(sli2);
            foreach (Branch item in ViewBag.Branche)
            {
                SelectListItem sli = new SelectListItem();
                if (item.BID == Manager.Branch_Id)
                {
                    sli.Selected = true;
                    sli1.Selected = false;
                }
                sli.Text = item.Name + "-" + item.Code;
                sli.Value = item.BID.ToString();
                sl.Add(sli);
            }
            foreach (Position item in ViewBag.Position)
            {
                SelectListItem sli = new SelectListItem();
                if (item.PID == Manager.Position_Id)
                {
                    sli.Selected = true;
                    sli1.Selected = false;
                }
                sli.Text = item.Name;
                sli.Value = item.PID.ToString();
                s2.Add(sli);
            }
            ViewBag.Branchlist = sl;
            ViewBag.Positionlist = s2;

            if (Manager == null)
            {
                return HttpNotFound();
            }
            ManagerR tb = new ManagerR();
            tb.MID = Manager.MID;
            tb.LoginName = Manager.LoginName;
            tb.LoginPass = Manager.LoginPass;
            tb.LoginIp = Manager.LoginIp;
            tb.LoginCount = 0;
            tb.Branch_Id = Manager.Branch_Id;
            tb.Branch_Code = Manager.Branch_Code;
            tb.Branch_Name = Manager.Branch_Name;
            tb.Position_Id = Manager.Position_Id;
            tb.Position_Name = Manager.Position_Name;
            tb.Enable = (int)Manager.Enable;
            tb.CName = Manager.CName;
            tb.EName = Manager.EName;
            tb.PhotoImg = Manager.PhotoImg;
            tb.Sex = Manager.Sex;
            tb.Birthday = Manager.Birthday;
            tb.NativePlace = Manager.NativePlace;
            tb.NationalName = Manager.NationalName;
            tb.Record = Manager.Record;
            tb.GraduateCollege = Manager.GraduateCollege;
            tb.GraduateSpecialty = Manager.GraduateSpecialty;
            tb.Tel = Manager.Tel;
            tb.Mobile = Manager.Mobile;
            tb.Email = Manager.Email;
            tb.Qq = Manager.Qq;
            tb.Msn = Manager.Msn;
            tb.Address = Manager.Address;
            tb.Content = Manager.Content;
            tb.Manager_Id = CurrentUser.MID;
            tb.Manager_CName = CurrentUser.CName;
            GetRoleAndManager(Manager.MID, "Manager");
            return View(tb);

        }

        // POST: /Manager/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "MID,LoginName,LoginPass,LoginIp,LoginCount,UpdateTime,Branch_Id,Branch_Code,Branch_Name,Position_Id,Position_Name,Enable,CName,EName,PhotoImg,Sex,Birthday,NativePlace,NationalName,Record,GraduateCollege,GraduateSpecialty,Tel,Mobile,Email,Qq,Msn,Address,Content,Manager_Id,Manager_CName")] ManagerR Manager)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    Manager tb = db.Managers.Find(id);
                    tb.LoginName = Manager.LoginName;
                    tb.LoginPass = Manager.LoginPass;
                    tb.LoginIp = Manager.LoginIp;
                    tb.LoginCount = Manager.LoginCount;
                    tb.Branch_Id = Manager.Branch_Id;
                    if (tb.Branch_Id != 0)
                    {
                        Branch Branch = db.Branches.Find(Manager.Branch_Id);
                        tb.Branch_Code = Branch.Code;
                        tb.Branch_Name = Branch.Name;
                    }
                    else
                    {
                        tb.Branch_Code = "";
                        tb.Branch_Name = "";
                        tb.Branch_Code = Manager.Branch_Code;
                        tb.Branch_Name = Manager.Branch_Name;
                    }
                    tb.Position_Id = Manager.Position_Id;
                    if (tb.Position_Id != 0)
                    {
                        Position Position = db.Positions.Find(tb.Position_Id);
                        tb.Position_Name = Position.Name;
                    }
                    else
                    {
                        tb.Position_Name = Manager.Position_Name;
                    }
                    tb.Enable = (byte)Manager.Enable;
                    tb.CName = Manager.CName;
                    tb.EName = Manager.EName;
                    tb.PhotoImg = Manager.PhotoImg;
                    tb.Sex = Manager.Sex;
                    tb.Birthday = Manager.Birthday;
                    tb.NativePlace = Manager.NativePlace;
                    tb.NationalName = Manager.NationalName;
                    tb.Record = Manager.Record;
                    tb.GraduateCollege = Manager.GraduateCollege;
                    tb.GraduateSpecialty = Manager.GraduateSpecialty;
                    tb.Tel = Manager.Tel;
                    tb.Mobile = Manager.Mobile;
                    tb.Email = Manager.Email;
                    tb.Qq = Manager.Qq;
                    tb.Msn = Manager.Msn;
                    tb.Address = Manager.Address;
                    tb.Content = Manager.Content;
                    tb.Manager_Id = CurrentUser.MID;
                    tb.Manager_CName = CurrentUser.CName;
                    tb.UpdateTime = DateTime.Now;

                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == Manager.MID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, Manager.MID, "Manager");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改Manager id " + Manager.MID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(Manager);
            }
        }

        // GET: /Manager/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Manager Manager = db.Managers.Find(id);
                if (Manager == null)
                {
                    return HttpNotFound();
                }
                return View(Manager);
            }
        }

        // POST: /Manager/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Manager Manager = db.Managers.Find(id);
                Manager.IsDel = 1;
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

        // POST: /Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Manager Manager = db.Managers.Find(id);
                db.Managers.Remove(Manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: /Manager/SetRole/5
        public ActionResult SetRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                List<Role> Role = db.Roles.ToList();
                ViewBag.SetMid = id;

                ViewBag.RoleManagers = db.RoleManagers.Where(s => s.MID == id).ToList();
                return View(Role);
            }
        }

        [HttpGet]
        // GET: /Manager/EditPassword/5
        public ActionResult EditPassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SetMid = id;
            return View();
        }

        [HttpPost]
        // GET: /Manager/SetRole/5
        public ActionResult EditPassword(int id)
        {
            string password = "";
            Manager Manager = new Manager();

            if (Request.Form["password"] == null)
            {
                throw new ArgumentNullException("修改密码：没有找到新密码！");
            }

            password = Request.Form["password"].ToString();

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Manager = db.Managers.Find(id);
                Manager.LoginPass = password;
                db.SaveChanges();
            }
            return View(Manager);
        }


        [HttpPost]
        [ShowPostWindowFilterAttribute]
        public ActionResult UpdateRoleTableActions(int? id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.RoleManagers.RemoveRange(db.RoleManagers.Where(s => s.MID == id));


                List<Role> Role = db.Roles.ToList();

                foreach (var item in Role)
                {
                    RoleManager RoleManager = new RoleManager();
                    string str = Request.Form["MR_" + item.RID.ToString()].ToString().Split(',')[0];
                    if (str != "false")
                    {
                        RoleManager.MID = id;
                        RoleManager.RID = item.RID;
                        db.RoleManagers.Add(RoleManager);

                    }

                }
                db.SaveChanges();
                return Content("OK");
            }
        }
    }
}
