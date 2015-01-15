using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

namespace MvcTest.Extensions
{
    public class CacheManager
    {
        public static bool ExistIdentify(string cacheKey)
        {
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="principal"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<PagePowerSignPublic> Get(string cacheKey, IPrincipal principal, string path)
        {
            if (!ExistIdentify(cacheKey))
            {
                HttpRuntime.Cache.Insert(cacheKey, Extension.GetAction(principal, path));
            }
            List<PagePowerSignPublic> PagePowerSignPublic = HttpRuntime.Cache.Get(cacheKey) as List<PagePowerSignPublic>;
            return PagePowerSignPublic;
        }



        /// <summary>
        /// memCache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="principal"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<PagePowerSignPublic> memCaCheGet(string cacheKey, IPrincipal principal, string path)
        {
            var List =
                Common.Caching.CacheManager.Get<List<PagePowerSignPublic>>(cacheKey, DateTime.Now.AddHours(1), () =>
                {
                    return Extension.GetAction(principal, path);
                });
            return List;
        }

        public static List<MasterData> memCaCheGet()
        {
            var List =
                Common.Caching.CacheManager.Get<List<MasterData>>("MasterData", DateTime.Now.AddMinutes(10), () =>
                {
                    using (ToolsDBEntities db = new ToolsDBEntities())
                    {
                        List<MasterData> MasterData = db.MasterDatas.ToList();
                        return MasterData;
                    }
                });
            return List;
        }


        public static Manager memCaCheGet(string cacheKey, IPrincipal principal)
        {
            var List =
                Common.Caching.CacheManager.Get<Manager>(cacheKey, DateTime.Now.AddHours(1), () =>
                {
                    return Extension.GetUser(principal);
                });
            return List;
        }

        public static List<Role> memCaCheGetRole(string cacheKey, IPrincipal principal)
        {
            var List =
                Common.Caching.CacheManager.Get<List<Role>>(cacheKey, DateTime.Now.AddHours(1), () =>
                {
                    return Extension.GetUserRole(principal);
                });
            return List;
        }
    }
}