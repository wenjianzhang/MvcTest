using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace MvcTest.Extensions
{
    public static partial class Extension
    {
        public static Manager GetUser(this IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal:掺入参数不能为null");
            }
            var memberID = GetManagerId(principal);
            if (memberID.HasValue)
            {
                //考虑从memCached中获取

                //从DB中获取
                using (var db = new ToolsDBEntities())
                {
                    var dbUser = db.Managers.SingleOrDefault(r => r.MID == memberID);
                    if (dbUser != null)
                        return dbUser;
                }
            }
            return null;
        }


        public static List<Role> GetUserRole(this IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal:掺入参数不能为null");
            }
            var memberID = GetManagerId(principal);
            if (memberID.HasValue)
            {
                //考虑从memCached中获取

                //从DB中获取
                using (var db = new ToolsDBEntities()) //.SingleOrDefault(r => r.MID == memberID);
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var dbUser = (from dd in db.Roles
                                  join dbs in db.RoleManagers on dd.RID equals dbs.RID
                                  where dbs.MID == memberID
                                  select dd).ToList();
                    if (dbUser != null)
                        return dbUser;
                }
            }
            return null;
        }

        public static int? GetUserRoleID(this IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal:掺入参数不能为null");
            }
            var memberID = GetManagerId(principal);
            if (memberID.HasValue)
            {
                //考虑从Cache中获取

                //从DB中获取
                using (var db = new ToolsDBEntities())
                {
                    var rolemanager = db.RoleManagers.SingleOrDefault(r => r.MID == memberID);
                    if (rolemanager != null)
                    {
                        return rolemanager.RID;
                    }
                }
            }
            return null;
        }

        public static List<PagePowerSignPublic> GetAction(this IPrincipal principal, string url)
        {
            if (url.ToLower().IndexOf("index") == -1)
            {
                url = (url + "/Index").Replace("//", "/");
            }
            if (principal == null)
            {
                throw new ArgumentNullException("principal:掺入参数不能为null");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url:掺入参数不能为null");
            }
            List<MenuInfo> MenuInfolistsF = new List<MenuInfo>();
            MenuInfo MenuInfolistsC = new MenuInfo();
            int roleid = -1;
            using (var db = new ToolsDBEntities())
            {
                try
                {
                    roleid = (int)Extension.GetUserRoleID(principal);

                }
                catch (Exception ex)
                {
                    return null;
                }
                if (roleid != -1)
                {
                    MenuInfolistsC = db.MenuInfoes.FirstOrDefault(s => s.Url == url);
                    int[] RoleMenuPages = (from rmp in db.RoleMenuPages
                                           where rmp.RID == roleid && rmp.MIID == MenuInfolistsC.MIID
                                           select rmp.PPSID ?? 0).ToArray();
                    db.Configuration.ProxyCreationEnabled = false;
                    List<PagePowerSignPublic> PagePowerSignPublics = db.PagePowerSignPublics.Where(s => RoleMenuPages.Contains(s.PPSPID)).ToList();
                    return PagePowerSignPublics;
                }
                else
                {
                    return null;
                }
            }
        }


        public static int? GetManagerId(this IPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal:掺入参数不能为null");
            }
            var identity = principal.Identity as FormsIdentity;
            if (identity != null)
            {
                int memberId = -1;
                if (int.TryParse(identity.Ticket.UserData, out memberId))
                    return memberId;
            }
            return null;
        }


        public static int? GetSqlServerVersion()
        {
            //using(var db=new ToolsDBEntities())
            //{
            //    db.Database.("select @@version");
            //}
            return null;
        }

        /// <summary>
        /// 取得HTML中所有图片的 URL。
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }
    }
}