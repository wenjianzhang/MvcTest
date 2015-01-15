using MvcTest.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;
using System.Security.Principal;
using System.Web.Security;
using MvcTest.Extensions;

namespace MvcTest.Controllers
{
    [useLogFilterAttribute]
    [ErroFilterAttribute]
    public class BaseController : Controller
    {
        public int MID { set; get; }
        public string LoginName { set; get; }



        public virtual Manager CurrentUser
        {
            get
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                return CacheManager.memCaCheGet(ticket.UserData + "CurrentUser", this.User);
            }
        }

        public List<MasterData> GetMasterData()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                List<MasterData> MasterData = db.MasterDatas.ToList();
                return MasterData;
            }
        }

        public int[] GetIDS(ToolsDBEntities db, string TableName)
        {
            int[] currentrole = CurrentUserRole.Select(o => o.RID).ToArray();

            int[] ids = GetMasterData().Where(s => s.TableName == TableName).Where(s => s.CreaterID == CurrentUser.MID || (currentrole.Contains(s.CreaterRoleID) || currentrole.Contains(s.RoleID ?? 0)) || s.ManagerID == CurrentUser.MID).Select(s => s.DataID).Distinct().ToArray();
            return ids;
        }

        public int[] GetIDS(string TableName)
        {
            using (var db = new ToolsDBEntities())
            {
                int[] currentrole = CurrentUserRole.Select(o => o.RID).ToArray();

                int[] ids = GetMasterData().Where(s => s.TableName == TableName).Where(s => s.CreaterID == CurrentUser.MID || (currentrole.Contains(s.CreaterRoleID) || currentrole.Contains(s.RoleID ?? 0)) || s.ManagerID == CurrentUser.MID).Select(s => s.DataID).Distinct().ToArray();
                return ids;
            }

        }


        public virtual List<Role> CurrentUserRole
        {
            get
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                return CacheManager.memCaCheGetRole(ticket.UserData + "CurrentUserRoles", this.User);
            }
        }

        public virtual string[] CurrentUserPermissions
        {
            get
            {
                string[] EName;
                List<PagePowerSignPublic> pagepowersignpublic = null;
                try
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    pagepowersignpublic = CacheManager.memCaCheGet(ticket.UserData + Request.Path.ToString(), this.User, Request.Path.ToString());
                    // Extension.GetAction(this.User, Request.Path.ToString());
                    if (pagepowersignpublic == null)
                    {
                        Response.Redirect("/Login");
                    }
                    EName = pagepowersignpublic.Select(s => s.EName).ToArray();
                    return EName;
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Login");
                }
                return null;
            }
        }


        // [HttpPost]
        public JsonResult GetC_Class(int id)
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.C_Classllist = db.C_Class.Where(s => s.ChannelID == id).ToList();
                List<SelectListItem> s2 = new List<SelectListItem>();
                SelectListItem sli2 = new SelectListItem();
                sli2.Text = "根目录";
                sli2.Value = "0";
                sli2.Selected = true;
                s2.Add(sli2);
                foreach (C_Class item in ViewBag.C_Classllist)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Text = item.ClassName;
                    sli.Value = item.ClassID.ToString();
                    s2.Add(sli);
                }
                return Json(s2);
            }
        }


        [HttpPost]
        public JsonResult GetC_Channel()
        {
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                ViewBag.C_Channellist = db.C_Channel.ToList();
                List<SelectListItem> s2 = new List<SelectListItem>();
                SelectListItem sli2 = new SelectListItem();
                sli2.Text = "根目录";
                sli2.Value = "0";
                sli2.Selected = true;
                s2.Add(sli2);
                foreach (C_Channel item in ViewBag.C_Channellist)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Text = item.ChannelName;
                    sli.Value = item.ChannelID.ToString();
                    s2.Add(sli);
                }
                return Json(s2);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ID"></param>
        /// <param name="TableName"></param>
        public void CreateMasterData(ToolsDBEntities db, int ID, string TableName)
        {
            if (RouteData.Values["action"].ToString().ToLower().Equals("create"))
            {
                MasterData masterdatas = new MasterData();
                masterdatas.TableName = TableName;
                masterdatas.DataID = ID;
                masterdatas.CreaterID = CurrentUser.MID;
                db.MasterDatas.Add(masterdatas);
            }
            if (Request.Form["Rolelist"] != null)
            {
                foreach (string item in Request.Form["Rolelist"].Split(','))
                {
                    if (item == null || item == "0")
                    {
                        continue;
                    }
                    MasterData masterdata = new MasterData();
                    masterdata.TableName = TableName;
                    masterdata.DataID = ID;
                    masterdata.RoleID = int.Parse(item);
                    masterdata.CreaterID = CurrentUser.MID;
                    db.MasterDatas.Add(masterdata);
                }
            }
            if (Request.Form["Managerlist"] != null)
            {
                foreach (string item in Request.Form["Managerlist"].Split(','))
                {
                    if (item == null || item == "0")
                    {
                        continue;
                    }
                    MasterData masterdata = new MasterData();
                    masterdata.TableName = TableName;
                    masterdata.DataID = ID;
                    masterdata.ManagerID = int.Parse(item);
                    masterdata.CreaterID = CurrentUser.MID;
                    db.MasterDatas.Add(masterdata);
                }
            }

            db.SaveChanges();
        }


        public void GetRoleAndManager()
        {
            List<Role> Roles = new List<Role>();
            List<Manager> Managers = new List<Manager>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {
                Roles = db.Roles.Where(s => s.IsDel == 0).ToList();
                Managers = db.Managers.Where(s => s.IsDel == 0).ToList();
            }

            ////////////////////////////////////////////////////////////////
            List<SelectListItem> s2 = new List<SelectListItem>();
            SelectListItem sli2 = new SelectListItem();
            //sli2.Text = "请选择";
            //sli2.Value = "0";
            //sli2.Selected = true;
            //s2.Add(sli2);
            foreach (Role item in Roles)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.Name;
                if (CurrentUserRole.Contains(item))
                {
                    sli.Selected = true;
                }
                sli.Value = item.RID.ToString();
                s2.Add(sli);
            }
            ViewBag.RoleSS = s2;
            ////////////////////////////////////////////////////////////////
            List<SelectListItem> s3 = new List<SelectListItem>();
            SelectListItem sli3 = new SelectListItem();
            //sli3.Text = "请选择";
            //sli3.Value = "0";
            //sli3.Selected = true;
            //s3.Add(sli3);
            foreach (Manager item in Managers)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.CName;
                if (CurrentUser.MID.Equals(item.MID))
                {
                    sli.Selected = true;
                }
                sli.Value = item.MID.ToString();
                s3.Add(sli);
            }
            ViewBag.ManagerSS = s3;
        }
        public void GetRoleAndManager(int ID, string TableName)
        {
            List<Role> Roles = new List<Role>();
            List<Manager> Managers = new List<Manager>();

            List<MasterData> MasterDatas = new List<MasterData>();
            using (ToolsDBEntities db = new ToolsDBEntities())
            {


                Roles = db.Roles.Where(s => s.IsDel == 0).ToList();
                Managers = db.Managers.Where(s => s.IsDel == 0).ToList();
                MasterDatas = db.MasterDatas.Where(s => s.DataID == ID && s.TableName == TableName).ToList();
            }
            ////////////////////////////////////////////////////////////////
            int[] roles = MasterDatas.Where(s => s.RoleID != null).Select(s => s.RoleID ?? 0).ToArray();
            List<SelectListItem> ss2 = new List<SelectListItem>();
            SelectListItem sli2 = new SelectListItem();
            sli2.Text = "请选择";
            sli2.Value = "0";
            //sli2.Selected = true;
            ss2.Add(sli2);
            foreach (Role item in Roles)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.Name;
                if (roles.Contains(item.RID))
                {
                    sli.Selected = true;
                }
                sli.Value = item.RID.ToString();
                ss2.Add(sli);
            }
            ViewBag.RoleSS = ss2;
            ////////////////////////////////////////////////////////////////
            int[] managers = MasterDatas.Where(s => s.ManagerID != null).Select(s => s.ManagerID ?? 0).ToArray();
            List<SelectListItem> ss3 = new List<SelectListItem>();
            SelectListItem sli3 = new SelectListItem();
            sli3.Text = "请选择";
            sli3.Value = "0";
            //sli3.Selected = true;
            ss3.Add(sli3);
            foreach (Manager item in Managers)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = item.CName;
                if (managers.Contains(item.MID))
                {
                    sli.Selected = true;
                }
                sli.Value = item.MID.ToString();
                ss3.Add(sli);
            }
            ViewBag.ManagerSS = ss3;
        }
    }
}