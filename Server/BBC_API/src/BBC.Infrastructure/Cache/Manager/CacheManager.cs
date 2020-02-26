using BBC.Core.Interfaces;
using BBC.Infrastructure.Cache.Base;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Infrastructure.Cache.Manager
{
    public class CacheManager : BaseCache, ICacheManager
    {
        private readonly IMemoryCache _cache;
        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string cacheKey) where T : class
        {
            if (!Contains(cacheKey))
            {
                base.Add(cacheKey);
                return new object() as T;
            }

            T item = _cache.Get<T>(cacheKey);
            return item;
        }

        public void SetOrUpdate<TItem>(string cacheKey, TItem item) where TItem : class
        {
            if (Contains(cacheKey))
            {
                base.Add(cacheKey);
            }
            _cache.Set<TItem>(cacheKey, item);
        }

        public void RemoveItems(string cacheKey)
        {
            if (Contains(cacheKey))
            {
                base.Remove(cacheKey);
            }
            _cache.Remove(cacheKey);
        }

        public Dictionary<string, List<object>> GetAllCache()
        {
            Dictionary<string, List<object>> resultAllCache = new Dictionary<string, List<object>>();
            if(base.CacheKeyManager == null)
            {
                return new Dictionary<string, List<object>>();
            }

            foreach (string cacheKey in base.CacheKeyManager)
            {
                List<object> cacheValues = Get<List<object>>(cacheKey);
                resultAllCache.Add(cacheKey, cacheValues);
            }

            return resultAllCache;

        }

        public List<string> GetAllCacheKeys()
        {
            return base.CacheKeyManager;
        }


    }
}
