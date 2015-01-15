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
    public class C_ChannelController : BaseController
    {
        // GET: /C_Channel/
        public ActionResult Index(int pageindex = 1)
        {
            ViewBag.EName = CurrentUserPermissions;
          
            List<C_Channel> C_Channellist = new List<C_Channel>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    return View(db.C_Channel.Where(s => s.IsDel == 0).Distinct().OrderByDescending(s => s.ChannelID).ToPagedList(pageindex, Configuration.Default.PageSize));
                }
                else
                {
                    int[] ids = GetIDS(db, "C_Channel");
                    if (ids.Length > 0)
                    {
                        return View(db.C_Channel.Where(s => s.IsDel == 0 && ids.Contains(s.ChannelID)).Distinct().OrderByDescending(s => s.ChannelID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(C_Channellist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }
            }
        }

        // GET: /C_Channel/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_Channel C_Channel = db.C_Channel.Find(id);
                if (C_Channel == null)
                {
                    return HttpNotFound();
                }
                return View(C_Channel);
            }
        }

        // GET: /C_Channel/Create
        public ActionResult Create()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.C_Channel = db.C_Channel.ToList();
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
            ViewBag.list = sl;

            GetRoleAndManager();
            return View();
        }

        // POST: /C_Channel/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CID,ChannelName,ChannelEName,PCID,IsOutChannel,OutChannelUrl,Sorc,BindDomain,Review,MetaKeyword,MetaDescription")] C_ChannelR C_Channel)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    C_Channel tb = new C_Channel();
                    tb.CID = Guid.NewGuid().ToString();
                    tb.ChannelName = C_Channel.ChannelName;
                    tb.ChannelEName = C_Channel.ChannelEName;
                    tb.MetaDescription = C_Channel.MetaDescription;
                    tb.MetaKeyword = C_Channel.MetaKeyword;
                    tb.PCID = int.Parse(C_Channel.PCID);
                    tb.IsOutChannel = C_Channel.IsOutChannel;
                    tb.OutChannelUrl = C_Channel.OutChannelUrl;
                    tb.Sorc = C_Channel.Sorc;
                    tb.BindDomain = C_Channel.BindDomain;
                    tb.Review = C_Channel.Review;
                    tb.IsDel = 0;
                    db.C_Channel.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.ChannelID, "C_Channel");
                    }
                    return RedirectToAction("Index");
                }

                return View(C_Channel);
            }
        }

        // GET: /C_Channel/Edit/5
        public ActionResult Edit(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_Channel C_Channel = db.C_Channel.Find(id);

                ViewBag.C_Channel = db.C_Channel.Where(s => s.ChannelID != id).ToList();
                List<SelectListItem> sl = new List<SelectListItem>();
                SelectListItem sli1 = new SelectListItem();
                sli1.Text = "根目录";
                sli1.Value = "0";
                sli1.Selected = true;
                sl.Add(sli1);
                foreach (C_Channel item in ViewBag.C_Channel)
                {
                    SelectListItem sli = new SelectListItem();
                    if (item.ChannelID == C_Channel.PCID)
                    {
                        sli.Selected = true;
                        sli1.Selected = false;
                    }
                    sli.Text = item.ChannelName;
                    sli.Value = item.ChannelID.ToString();
                    sl.Add(sli);
                }
                ViewBag.list = sl;


                if (C_Channel == null)
                {
                    return HttpNotFound();
                }
                C_ChannelR tb = new C_ChannelR();
                tb.ChannelID = C_Channel.ChannelID;
                tb.CID = C_Channel.CID;
                tb.ChannelName = C_Channel.ChannelName;
                tb.ChannelEName = C_Channel.ChannelEName;
                //tb.PCID = C_Channel.PCID ?? 0;
                tb.IsOutChannel = C_Channel.IsOutChannel ?? 0;
                tb.OutChannelUrl = C_Channel.OutChannelUrl;
                tb.MetaDescription = C_Channel.MetaDescription;
                tb.MetaKeyword = C_Channel.MetaKeyword;
                tb.Sorc = C_Channel.Sorc ?? 99999;
                tb.BindDomain = C_Channel.BindDomain;
                tb.Review = C_Channel.Review;
                GetRoleAndManager(C_Channel.ChannelID, "C_Channel");
                return View(tb);
            }
        }

        // POST: /C_Channel/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChannelID,CID,ChannelName,ChannelEName,PCID,IsOutChannel,OutChannelUrl,Sorc,BindDomain,Review,MetaKeyword,MetaDescription")] C_ChannelR C_Channel)
        {
            C_Channel tb = new C_Channel();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    tb = db.C_Channel.Find(C_Channel.ChannelID);

                    tb.ChannelID = C_Channel.ChannelID;
                    tb.CID = C_Channel.CID;
                    tb.ChannelName = C_Channel.ChannelName;
                    tb.ChannelEName = C_Channel.ChannelEName;
                    tb.PCID = int.Parse(C_Channel.PCID);
                    tb.IsOutChannel = C_Channel.IsOutChannel;
                    tb.OutChannelUrl = C_Channel.OutChannelUrl;
                    tb.Sorc = C_Channel.Sorc;
                    tb.BindDomain = C_Channel.BindDomain;
                    tb.MetaDescription = C_Channel.MetaDescription;
                    tb.MetaKeyword = C_Channel.MetaKeyword;
                    tb.Review = C_Channel.Review;
                   
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == C_Channel.ChannelID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, C_Channel.ChannelID, "C_Channel");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改C_Channel id " + C_Channel.ChannelID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(tb);
            }
        }

        // GET: /C_Channel/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_Channel C_Channel = db.C_Channel.Find(id);
                if (C_Channel == null)
                {
                    return HttpNotFound();
                }
                return View(C_Channel);
            }
        }

        // POST: /C_Channel/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                C_Channel C_Channel = db.C_Channel.Find(id);
                C_Channel.IsDel = 1;
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


        // POST: /C_Channel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                C_Channel C_Channel = db.C_Channel.Find(id);
                db.C_Channel.Remove(C_Channel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
