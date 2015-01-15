using Glav.CacheAdapter;
using Glav.CacheAdapter.Core;
using Glav.CacheAdapter.Core.DependencyInjection;
using Glav.CacheAdapter.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Caching
{
    public static class CacheManager
    {
        #region init
        private static readonly ICacheProvider CacheProvider;

        public static ICacheProvider InnerCacheProvider
        {
            get
            {
                return CacheProvider;
            }
        }

        static CacheManager()
        {
            var logger = new LoggerAdapter();
            try
            {
                AppServices.PreStartInitialise(
                    config: ReadSettingsFromDefaultConfig(),
                    logger: logger
                );
                CacheProvider = AppServices.Cache;
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
            }
        }

        private static CacheConfig ReadSettingsFromDefaultConfig()
        {
            return new CacheConfig
            {
                IsCacheEnabled = Configurations.Cache.Default.IsCacheEnabled,
                IsCacheDependencyManagementEnabled = Configurations.Cache.Default.IsCacheDependencyManagementEnabled,
                CacheSpecificData = Configurations.Cache.Default.CacheSpecificData,
                CacheToUse = !string.IsNullOrWhiteSpace(Configurations.Cache.Default.CacheToUse) ? Configurations.Cache.Default.CacheToUse.ToLowerInvariant() : string.Empty,
                DependencyManagerToUse = Configurations.Cache.Default.DependencyManagerToUse,
                DistributedCacheServers = Configurations.Cache.Default.DistributedCacheServers
            };
        }
        #endregion

        #region ICacheProvider

        public static T Get<T>(string cacheKey, DateTime absoluteExpiryDate, Func<T> getData, bool autoAddToCache = true, string parentKey = null, CacheDependencyAction actionForDependency = CacheDependencyAction.ClearDependentItems)
        {
            Func<object> getDataWrapper = () =>
            {
                var data = (object)getData();
                if (autoAddToCache && data != null)
                    Add(cacheKey, absoluteExpiryDate, data, parentKey, actionForDependency);
                return data;
            };
            var result = CacheProvider.Get(cacheKey, absoluteExpiryDate, getDataWrapper, parentKey, actionForDependency);
            return (T)result;
        }

        public static T Get<T>(string cacheKey, TimeSpan slidingExpiryWindow, Func<T> getData, bool autoAddToCache = true, string parentKey = null, CacheDependencyAction actionForDependency = CacheDependencyAction.ClearDependentItems)
        {
            Func<object> getDataWrapper = () =>
            {
                var data = (object)getData();
                if (autoAddToCache && data != null)
                    Add(cacheKey, slidingExpiryWindow, data, parentKey, actionForDependency);
                return data;
            };
            var result = CacheProvider.Get(cacheKey, slidingExpiryWindow, getDataWrapper, parentKey, actionForDependency);
            return (T)result;
        }

        public static void InvalidateCacheItem(string cacheKey)
        {
            CacheProvider.InvalidateCacheItem(cacheKey);
        }

        public static void Add(string cacheKey, DateTime absoluteExpiryDate, object dataToAdd, string parentKey = null, CacheDependencyAction actionForDependency = CacheDependencyAction.ClearDependentItems)
        {
            CacheProvider.Add(cacheKey, absoluteExpiryDate, dataToAdd, parentKey, actionForDependency);
        }

        public static void Add(string cacheKey, TimeSpan slidingExpiryWindow, object dataToAdd, string parentKey = null, CacheDependencyAction actionForDependency = CacheDependencyAction.ClearDependentItems)
        {
            CacheProvider.Add(cacheKey, slidingExpiryWindow, dataToAdd, parentKey, actionForDependency);
        }

        public static void AddToPerRequestCache(string cacheKey, object dataToAdd)
        {
            CacheProvider.AddToPerRequestCache(cacheKey, dataToAdd);
        }

        public static void ClearAll()
        {
            CacheProvider.ClearAll();
        }
        #endregion

        public static IEnumerable<object> Gets(params string[] keys)
        {
            return keys.Select(k => CacheProvider.InnerCache.Get<object>(k));
        }

        public static T Get<T>(string key)
        {
            return (T)CacheProvider.InnerCache.Get<object>(key);
        }
    }
}
