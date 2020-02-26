using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Infrastructure.Cache.Base
{
    public class BaseCache
    {
        protected List<string> CacheKeyManager { get; set; }

        protected bool Contains(string cacheKey) => CacheKeyManager.Contains(cacheKey);

        protected void Add(string cacheKey) => CacheKeyManager.Add(cacheKey);

        protected void Remove(string cacheKey) => CacheKeyManager.Remove(cacheKey);
    }
}
