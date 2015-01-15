using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common.Configurations;

namespace MvcTest.Controllers
{
    public class C_ClassController : BaseController
    {
        // GET: /C_Class/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
            List<C_Class> C_Classlist = new List<C_Class>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.C_Class.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.ClassID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "C_Class");
                    if (ids.Length > 0)
                    {
                        return View(db.C_Class.Where(s => s.IsDel == 0 && ids.Contains(s.ClassID)).Distinct().OrderByDescending(s => s.ClassID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(C_Classlist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }
            }
        }

        // GET: /C_Class/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_Class C_Class = db.C_Class.Find(id);
                if (C_Class == null)
                {
                    return HttpNotFound();
                }
                return View(C_Class);
            }
        }

        // GET: /C_Class/Create
        public ActionResult Create()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.C_Channel = db.C_Channel.ToList();


                ViewBag.C_Class = db.C_Class.ToList();

            }
            List<SelectListItem> sl = new List<SelectListItem>();
            SelectListItem sli1 = new SelectListItem();
            sli1.Text = "根目录";
            sli1.Value = "0";
            sli1.Selected = true;
            sl.Add(sli1);
            foreach (C_Channel item in ViewBag.C_Channel)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.ChannelName;
                sli.Value = item.ChannelID.ToString();
                sl.Add(sli);
            }
            ViewBag.C_Channellist = sl;
            List<SelectListItem> s2 = new List<SelectListItem>();
            SelectListItem sli2 = new SelectListItem();
            sli2.Text = "根目录";
            sli2.Value = "0";
            sli2.Selected = true;
            s2.Add(sli2);
            foreach (C_Class item in ViewBag.C_Class)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.ClassName;
                sli.Value = item.ClassID.ToString();
                s2.Add(sli);
            }
            ViewBag.C_Classllist = s2;
            GetRoleAndManager();
            return View();
        }




        // POST: /C_Class/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,CID,ClassName,ClassEName,PCID,IsOutClass,OutClassUrl,Sort,BindDomain,ClassModel,Saveurl,ClassFile,Review,Namerules,PageIndex,NewsSaveurl,NewsLookName,ImageUploadurl,PublicTime,ClassDataTable,ChannelID,IsMenuShow,MenuImage,MenuTitle,MetaKeyword,MetaDescription,SCFileEXT,IsLock,IsDel,CustomCode,IsPage,IsHtml")] C_ClassR C_Class)
        {
            C_Class tb = new C_Class();
            //tb.CID = C_Class.CID;
            tb.ClassName = C_Class.ClassName;
            tb.ClassEName = C_Class.ClassEName;
            tb.ChannelID = int.Parse(C_Class.ChannelID);
            tb.PCID = int.Parse(C_Class.PCID);
            //tb.IsOutClass = C_Class.IsOutClass;
            //tb.OutClassUrl = C_Class.OutClassUrl;
            tb.Sort = C_Class.Sort;
            //tb.BindDomain = C_Class.BindDomain;
            //tb.ClassModel = C_Class.ClassModel;
            //tb.Saveurl = C_Class.Saveurl;
            //tb.ClassFile = C_Class.ClassFile;
            //tb.Review = C_Class.Review;
            //tb.Namerules = C_Class.Namerules;
            //tb.PageIndex = C_Class.PageIndex;
            //tb.NewsSaveurl = C_Class.NewsSaveurl;
            //tb.NewsLookName = C_Class.NewsLookName;
            //tb.ImageUploadurl = C_Class.ImageUploadurl;
            //tb.PublicTime = C_Class.PublicTime;
            //tb.ClassDataTable = C_Class.ClassDataTable;
            //tb.IsMenuShow = C_Class.IsMenuShow;
            //tb.MenuImage = C_Class.MenuImage;
            //tb.MenuTitle = C_Class.MenuTitle;
            tb.MetaKeyword = C_Class.MetaKeyword;
            tb.MetaDescription = C_Class.MetaDescription;
            //tb.SCFileEXT = C_Class.SCFileEXT;
            tb.IsLock = 0;
            tb.IsDel = 0;
            //tb.CustomCode = C_Class.CustomCode;
            tb.CreateTime = DateTime.Now;
            //tb.IsPage = C_Class.IsPage;
            //tb.IsHtml = C_Class.IsHtml;
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {

                    db.C_Class.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.ClassID, "C_Class");
                    }
                    return RedirectToAction("Index");
                }

                return View(C_Class);
            }
        }

        // GET: /C_Class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {

                C_Class C_Class = db.C_Class.Find(id);

                ViewBag.C_Channel = db.C_Channel.ToList();
                List<SelectListItem> sl = new List<SelectListItem>();
                SelectListItem sli1 = new SelectListItem();
                sli1.Text = "根目录";
                sli1.Value = "0";
                sli1.Selected = true;
                sl.Add(sli1);
                foreach (C_Channel item in ViewBag.C_Channel)
                {
                    SelectListItem sli = new SelectListItem();
                    if (item.ChannelID == C_Class.ChannelID)
                    {
                        sli.Selected = true;
                        sli1.Selected = false;
                    }
                    sli.Text = item.ChannelName;
                    sli.Value = item.ChannelID.ToString();
                    sl.Add(sli);
                }
                ViewBag.C_Channellist = sl;

                ViewBag.C_Class = db.C_Class.ToList();
                List<SelectListItem> s2 = new List<SelectListItem>();
                SelectListItem sli2 = new SelectListItem();
                sli2.Text = "根目录";
                sli2.Value = "0";
                sli2.Selected = true;
                s2.Add(sli2);
                foreach (C_Class item in ViewBag.C_Class)
                {
                    SelectListItem sli = new SelectListItem();
                    if (item.ClassID == C_Class.PCID)
                    {
                        sli.Selected = true;
                        sli2.Selected = false;
                    }
                    sli.Text = item.ClassName;
                    sli.Value = item.ClassID.ToString();
                    s2.Add(sli);
                }
                ViewBag.C_Classllist = s2;


                if (C_Class == null)
                {
                    return HttpNotFound();
                }


                C_ClassR tb = new C_ClassR();
                tb.ClassID = C_Class.ClassID;
                tb.CID = C_Class.CID;
                tb.ClassName = C_Class.ClassName;
                tb.ClassEName = C_Class.ClassEName;
                //tb.PCID = C_Class.PCID ?? 0;
                //tb.IsOutClass = C_Class.IsOutClass ?? 0;
                //tb.OutClassUrl = C_Class.OutClassUrl;
                tb.Sort = C_Class.Sort ?? 0;
                //tb.BindDomain = C_Class.BindDomain;
                //tb.ClassModel = C_Class.ClassModel;
                //tb.Saveurl = C_Class.Saveurl;
                //tb.ClassFile = C_Class.ClassFile;
                //tb.Review = C_Class.Review;
                //tb.Namerules = C_Class.Namerules;
                //tb.PageIndex = C_Class.PageIndex;
                //tb.NewsSaveurl = C_Class.NewsSaveurl;
                //tb.NewsLookName = C_Class.NewsLookName;
                //tb.ImageUploadurl = C_Class.ImageUploadurl;
                //tb.PublicTime = C_Class.PublicTime ?? DateTime.Now;
                //tb.ClassDataTable = C_Class.ClassDataTable;
                //tb.ChannelID = C_Class.ChannelID ?? 0;
                //tb.IsMenuShow = C_Class.IsMenuShow;
                //tb.MenuImage = C_Class.MenuImage;
                //tb.MenuTitle = C_Class.MenuTitle;
                tb.MetaKeyword = C_Class.MetaKeyword;
                tb.MetaDescription = C_Class.MetaDescription;
                //tb.SCFileEXT = C_Class.SCFileEXT;
                tb.IsLock = C_Class.IsLock ?? 0;
                tb.IsDel = C_Class.IsDel ?? 0;
                //tb.CustomCode = C_Class.CustomCode ?? 0;
                tb.CreateTime = C_Class.CreateTime ?? DateTime.Now;
                //tb.IsPage = C_Class.IsPage ?? 0;
                //tb.IsHtml = C_Class.IsHtml ?? 0;
                GetRoleAndManager(C_Class.ClassID, "C_Class");
                return View(tb);
            }
        }

        // POST: /C_Class/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,CID,ClassName,ClassEName,PCID,IsOutClass,OutClassUrl,Sort,BindDomain,ClassModel,Saveurl,ClassFile,Review,Namerules,PageIndex,NewsSaveurl,NewsLookName,ImageUploadurl,PublicTime,ClassDataTable,ChannelID,IsMenuShow,MenuImage,MenuTitle,MetaKeyword,MetaDescription,SCFileEXT,IsLock,IsDel,CustomCode,CreateTime,IsPage,IsHtml")] C_ClassR C_Class)
        {
            C_Class tb = new C_Class();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {

                    tb = db.C_Class.Find(C_Class.ClassID);


                    tb.ClassID = C_Class.ClassID;
                    tb.CID = C_Class.CID;
                    tb.ClassName = C_Class.ClassName;
                    tb.ClassEName = C_Class.ClassEName;
                    tb.PCID = int.Parse(C_Class.PCID);
                    //tb.IsOutClass = C_Class.IsOutClass;
                    //tb.OutClassUrl = C_Class.OutClassUrl;
                    //tb.Sort = C_Class.Sort;
                    //tb.BindDomain = C_Class.BindDomain;
                    //tb.ClassModel = C_Class.ClassModel;
                    //tb.Saveurl = C_Class.Saveurl;
                    //tb.ClassFile = C_Class.ClassFile;
                    //tb.Review = C_Class.Review;
                    //tb.Namerules = C_Class.Namerules;
                    //tb.PageIndex = C_Class.PageIndex;
                    //tb.NewsSaveurl = C_Class.NewsSaveurl;
                    //tb.NewsLookName = C_Class.NewsLookName;
                    //tb.ImageUploadurl = C_Class.ImageUploadurl;
                    //tb.PublicTime = C_Class.PublicTime;
                    //tb.ClassDataTable = C_Class.ClassDataTable;
                    tb.ChannelID = int.Parse(C_Class.ChannelID);
                    //tb.IsMenuShow = C_Class.IsMenuShow;
                    //tb.MenuImage = C_Class.MenuImage;
                    //tb.MenuTitle = C_Class.MenuTitle;
                    tb.MetaKeyword = C_Class.MetaKeyword;
                    tb.MetaDescription = C_Class.MetaDescription;
                    //tb.SCFileEXT = C_Class.SCFileEXT;
                    tb.IsLock = C_Class.IsLock;
                    tb.IsDel = C_Class.IsDel;
                    //tb.CustomCode = C_Class.CustomCode;
                    //tb.CreateTime = C_Class.CreateTime;
                    //tb.IsPage = C_Class.IsPage;
                    //tb.IsHtml = C_Class.IsHtml;
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == C_Class.ClassID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, C_Class.ClassID, "C_Class");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改C_Class id " + C_Class.ClassID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(tb);
            }
        }

        // GET: /C_Class/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_Class C_Class = db.C_Class.Find(id);
                if (C_Class == null)
                {
                    return HttpNotFound();
                }
                return View(C_Class);
            }
        }

        // POST: /C_Class/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                C_Class C_Class = db.C_Class.Find(id);
                C_Class.IsDel = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // POST: /C_Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                C_Class C_Class = db.C_Class.Find(id);
                C_Class.IsDel = 1;
                //db.C_Class.Remove(C_Class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
