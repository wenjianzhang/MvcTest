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
using MvcTest.Extensions;
using System.Collections;
using ThirdPartyApi.Models;
using System.Text.RegularExpressions;

namespace MvcTest.Controllers
{
    public class C_NewsController : BaseController
    {
        // GET: /C_News/
        public ActionResult Index(int pageindex = 1, string p = "")
        {
            ViewBag.p = p;
            ViewBag.EName = CurrentUserPermissions;
            List<C_News> C_Newslist = new List<C_News>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (CurrentUserRole.Where(s => s.Name == "超级管理员").Count() > 0)
                {
                    if (p != "")
                    {
                        return View(db.C_News.Where(s => s.IsDel == 0 && s.NewsTitle.IndexOf(p) != -1).Distinct().OrderBy(s=>s.Sorc).ThenByDescending(s => s.NID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                    else
                    {
                        return View(db.C_News.Where(s => s.IsDel == 0).Distinct().OrderBy(s => s.Sorc).ThenByDescending(s => s.NID).ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }
                else
                {
                    int[] ids = GetIDS(db, "C_News");
                    if (ids.Length > 0)
                    {
                        if (p != "")
                        {
                            return View(db.C_News.Where(s => s.IsDel == 0 && ids.Contains(s.NID) && s.NewsTitle.IndexOf(p) != -1).Distinct().OrderBy(s => s.Sorc).ThenByDescending(s => s.NID).ToPagedList(pageindex, Configuration.Default.PageSize));
                        }
                        else
                        {
                            return View(db.C_News.Where(s => s.IsDel == 0 && ids.Contains(s.NID)).Distinct().OrderBy(s => s.Sorc).ThenByDescending(s => s.NID).ToPagedList(pageindex, Configuration.Default.PageSize));
                        }
                    }
                    else
                    {
                        return View(C_Newslist.ToPagedList(pageindex, Configuration.Default.PageSize));
                    }
                }

            }
        }

        // GET: /C_News/Details/5
        public ActionResult Details(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_News C_News = db.C_News.Find(id);
                if (C_News == null)
                {
                    return HttpNotFound();
                }
                return View(C_News);
            }
        }

        // GET: /C_News/Create
        public ActionResult Create()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
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
                    sli.Text = item.ChannelName;
                    sli.Value = item.ChannelID.ToString();
                    sl.Add(sli);
                }
                ViewBag.C_Channellist = sl.ToList(); ;

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
                    sli.Text = item.ClassName;
                    sli.Value = item.ClassID.ToString();
                    s2.Add(sli);
                }
                ViewBag.C_Classllist = s2.ToList(); ;
            }
            GetRoleAndManager();
            return View();
        }

        // POST: /C_News/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChannelID,ClassID,OrderID,NewsTitle,SNewsTitle,TitleColor,TitleITF,TitleBIT,URLLaddress,PicURL,inPicURL,Auther,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,MetaKeywords,Metadesc,UpdateTime,SavePath,FileName,FileEXName,TopNum,IsLock,IsDel,DataLib,DefineID,IsHTML,CheckStat,Sorc,PublicTime")] C_NewsR C_News)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {
                    C_News tb = new C_News();
                    tb.NewsID = C_News.NewsID;
                    tb.ChannelID = int.Parse(C_News.ChannelID);
                    tb.ClassID = int.Parse(C_News.ClassID);
                    tb.OrderID = C_News.OrderID;
                    tb.NewsTitle = C_News.NewsTitle;
                    //tb.SNewsTitle = C_News.SNewsTitle;
                    //tb.TitleColor = C_News.TitleColor;
                    //tb.TitleITF = C_News.TitleITF;
                    //tb.TitleBIT = C_News.TitleBIT;
                    tb.URLLaddress = C_News.URLLaddress;
                    //tb.inPicURL = C_News.inPicURL;
                    tb.Auther = C_News.Auther;
                    tb.Souce = C_News.Souce;
                    tb.Tags = "";
                    String result = Regex.Replace(Request.Form["editorValue"], @"<[^>]*>", String.Empty);
                    Exception error = null;
                    ThirdPartyApi.BosonnlpApi api = new ThirdPartyApi.BosonnlpApi();
                    List<KeywordsModel> ss = api.GetKeyword(result, "", "utf-8", out error);
                    foreach (var item in ss.Take(5))
                    {
                        tb.Tags += item.Keyword + ",";
                    }
                    //tb.NewsProperty = C_News.NewsProperty;
                    //tb.NewsPicTopline = C_News.NewsPicTopline;
                    //tb.Templet = C_News.Templet;
                    tb.Content = Request.Form["editorValue"];// C_News.Content;
                    string[] imgUrls = Extension.GetHtmlImageUrlList(tb.Content);
                    if (imgUrls.Count() > 0)
                    {
                        tb.PicURL = imgUrls.First();
                    }
                    tb.MetaKeywords = C_News.MetaKeywords;
                    tb.Metadesc = C_News.Metadesc;
                    tb.Click = 0;
                    tb.CreateTime = DateTime.Now;
                    tb.UpdateTime = DateTime.Now;
                    DateTime datetime = DateTime.Now;
                    DateTime.TryParse(string.IsNullOrEmpty(C_News.PublicTime) ? DateTime.Now.ToString() : C_News.PublicTime, out datetime);
                    tb.PublicTime = Convert.ToDateTime(datetime);
                    //tb.SavePath = C_News.SavePath;
                    //tb.FileName = C_News.FileName;
                    //tb.FileEXName = C_News.FileEXName;
                    tb.TopNum = C_News.TopNum;
                    tb.IsLock = 0;
                    tb.IsDel = 0;
                    tb.DataLib = C_News.DataLib;
                    tb.DefineID = C_News.DefineID;
                    tb.IsHTML = C_News.IsHTML;
                    tb.CheckStat = 0;
                    tb.Sorc = C_News.Sorc;
                    db.C_News.Add(tb);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateMasterData(db, tb.NID, "C_News");
                    }
                    return RedirectToAction("Index");
                }

                return View(C_News);
            }
        }

        // GET: /C_News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ToolsDBEntities db = new ToolsDBEntities())
            {

                C_News C_News = db.C_News.Find(id);

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
                    if (item.ChannelID == C_News.ChannelID)
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
                    if (item.ClassID == C_News.ClassID)
                    {
                        sli.Selected = true;
                        sli2.Selected = false;
                    }
                    sli.Text = item.ClassName;
                    sli.Value = item.ClassID.ToString();
                    s2.Add(sli);
                }
                ViewBag.C_Classllist = s2;


                if (C_News == null)
                {
                    return HttpNotFound();
                }

                C_NewsR tb = new C_NewsR();
                tb.NID = C_News.NID;
                tb.NewsID = C_News.NewsID;
                //tb.ChannelID = C_News.ChannelID ?? 0;
                //tb.ClassID = C_News.ClassID ?? 0;
                tb.OrderID = C_News.OrderID ?? 0;
                tb.NewsTitle = C_News.NewsTitle;
                //tb.SNewsTitle = C_News.SNewsTitle;
                //tb.TitleColor = C_News.TitleColor;
                //tb.TitleITF = C_News.TitleITF ?? 0;
                //tb.TitleBIT = C_News.TitleBIT ?? 0;
                tb.URLLaddress = C_News.URLLaddress;
                //tb.PicURL = C_News.PicURL;
                //tb.inPicURL = C_News.inPicURL;
                tb.Auther = C_News.Auther;
                tb.Souce = C_News.Souce;
                tb.Tags = C_News.Tags;
                //tb.NewsProperty = C_News.NewsProperty ?? 0;
                //tb.NewsPicTopline = C_News.NewsPicTopline ?? 0;
                //tb.Templet = C_News.Templet;
                tb.Content = C_News.Content;
                tb.MetaKeywords = C_News.MetaKeywords;
                tb.Metadesc = C_News.Metadesc;
                tb.Click = C_News.Click ?? 0;
                //tb.CreateTime = C_News.CreateTime ?? DateTime.Now;
                tb.UpdateTime = C_News.UpdateTime ?? DateTime.Now;

                DateTime datetime = DateTime.Now;
                DateTime.TryParse((C_News.PublicTime ?? DateTime.Now).ToString("MM-dd-yyyy"), out datetime);
                tb.PublicTime = datetime.ToString("MM-dd-yyyy");

                tb.SavePath = C_News.SavePath;
                tb.FileName = C_News.FileName;
                tb.FileEXName = C_News.FileEXName;
                tb.TopNum = C_News.TopNum ?? 99999;
                tb.IsLock = C_News.IsLock ?? 0;
                tb.IsDel = C_News.IsDel ?? 0;
                tb.Sorc = C_News.Sorc ?? 0;
                //tb.DataLib = C_News.DataLib;
                //tb.DefineID = C_News.DefineID;
                //tb.IsHTML = C_News.IsHTML ?? 0;
                tb.CheckStat = C_News.CheckStat ?? 0;
                GetRoleAndManager(C_News.NID, "C_News");
                return View(tb);
            }
        }

        // POST: /C_News/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NID,NewsID,ChannelID,ClassID,OrderID,NewsTitle,SNewsTitle,TitleColor,TitleITF,TitleBIT,URLLaddress,PicURL,inPicURL,Auther,Souce,Tags,NewsProperty,NewsPicTopline,Templet,Content,MetaKeywords,Metadesc,Click,CreateTime,UpdateTime,SavePath,FileName,FileEXName,TopNum,IsLock,IsDel,DataLib,DefineID,IsHTML,CheckStat,Sorc,PublicTime")] C_NewsR C_News)
        {
            C_News tb = new C_News();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (ModelState.IsValid)
                {

                    tb = db.C_News.Find(C_News.NID);
                    tb.NID = C_News.NID;
                    tb.NewsID = C_News.NewsID;
                    tb.ChannelID = int.Parse(C_News.ChannelID);
                    tb.ClassID = int.Parse(C_News.ClassID);
                    tb.OrderID = C_News.OrderID;
                    tb.NewsTitle = C_News.NewsTitle;
                    //tb.SNewsTitle = C_News.SNewsTitle;
                    //tb.TitleColor = C_News.TitleColor;
                    //tb.TitleITF = C_News.TitleITF;
                    //tb.TitleBIT = C_News.TitleBIT;
                    tb.URLLaddress = C_News.URLLaddress;
                    //tb.PicURL = C_News.PicURL;
                    //tb.inPicURL = C_News.inPicURL;
                    tb.Auther = C_News.Auther;
                    tb.Souce = C_News.Souce;

                    //tb.NewsProperty = C_News.NewsProperty;
                    //tb.NewsPicTopline = C_News.NewsPicTopline;
                    //tb.Templet = C_News.Templet;
                    tb.Content = Request.Form["editorValue"];// C_News.Content; 
                    tb.Tags = "";
                    String result = Regex.Replace(Request.Form["editorValue"], @"<[^>]*>", String.Empty);
                    Exception error = null;
                    ThirdPartyApi.BosonnlpApi api = new ThirdPartyApi.BosonnlpApi();
                    List<KeywordsModel> ss = api.GetKeyword(result, "", "utf-8", out error);
                    foreach (var item in ss.Take(5))
                    {
                        tb.Tags += item.Keyword + ",";
                    }
                    string[] imgUrls = Extension.GetHtmlImageUrlList(tb.Content);
                    if (imgUrls.Count() > 0)
                    {
                        tb.PicURL = imgUrls.First();
                    }
                    tb.MetaKeywords = C_News.MetaKeywords;
                    tb.Metadesc = C_News.Metadesc;
                    //tb.Click = C_News.Click;
                    //tb.CreateTime = Convert.ToDateTime(C_News.CreateTime);
                    tb.UpdateTime = DateTime.Now;

                    DateTime datetime = DateTime.Now;
                    DateTime.TryParse(string.IsNullOrEmpty(C_News.PublicTime) ? DateTime.Now.ToString() : C_News.PublicTime, out datetime);
                    tb.PublicTime = Convert.ToDateTime(datetime);
                    //tb.SavePath = C_News.SavePath;
                    //tb.FileName = C_News.FileName;
                    //tb.FileEXName = C_News.FileEXName;
                    tb.TopNum = C_News.TopNum;
                    tb.IsLock = C_News.IsLock;
                    tb.IsDel = C_News.IsDel;
                    //tb.DataLib = C_News.DataLib;
                    //tb.DefineID = C_News.DefineID;
                    //tb.IsHTML = C_News.IsHTML;
                    tb.CheckStat = C_News.CheckStat;
                    tb.Sorc = C_News.Sorc;
                    db.Entry(tb).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        List<MasterData> MasterData = db.MasterDatas.Where(s => s.DataID == C_News.NID && !(s.ManagerID == null && s.RoleID == null)).ToList();
                        db.MasterDatas.RemoveRange(MasterData);
                        try
                        {
                            CreateMasterData(db, C_News.NID, "C_News");
                        }
                        catch (Exception ex)
                        {
                            Common.Loger.LogerManager.Error("修改C_News id " + C_News.NID, ex);
                            db.MasterDatas.AddRange(MasterData);
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(tb);
            }
        }

        // GET: /C_News/Delete/5
        public ActionResult Delete(int? id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                C_News C_News = db.C_News.Find(id);
                if (C_News == null)
                {
                    return HttpNotFound();
                }
                return View(C_News);
            }
        }

        // POST: /C_News/DeleteConfirmedCom/5
        [HttpPost]
        public ActionResult DeleteConfirmedCom(int id)
        {

            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                C_News C_News = db.C_News.Find(id);
                C_News.IsDel = 1;
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

        // POST: /C_News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                C_News C_News = db.C_News.Find(id);
                C_News.IsDel = 1;
                //db.C_News.Remove(C_News);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
