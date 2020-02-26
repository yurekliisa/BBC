using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string cacheKey) where T : class;
        void SetOrUpdate<TItem>(string cacheKey, TItem item) where TItem : class;
        void RemoveItems(string cacheKey);
        Dictionary<string, List<object>> GetAllCache();
        List<string> GetAllCacheKeys();
    }
}
