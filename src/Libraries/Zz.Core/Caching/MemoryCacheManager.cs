using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zz.Core.Caching
{
    public partial class SurfMemoryCache : ICacheManager
    {
        #region Fields
        private readonly IMemoryCache _cache;
        #endregion

        #region Ctor
        public SurfMemoryCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        #endregion

        // Get
        public virtual T Get<T>(string key)
        {
            _cache.TryGetValue(key, out T data);
            return data;
        }

        public virtual T Get<T>(string key, double cacheSeconds, Func<T> acquire)
        {
            if (_cache.TryGetValue(key, out T data))
                return data;

            data = acquire();
            if (cacheSeconds > 0)
                _cache.Set(key, data, TimeSpan.FromSeconds(cacheSeconds));

            return data;
        }

        // Set
        public virtual void Set(string key, object data, double cacheSeconds)
        {
            if (data == null)
                return;

            _cache.Set(key, data, TimeSpan.FromSeconds(cacheSeconds));
            //_cache.Set(key, data, DateTime.Now + TimeSpan.FromMinutes(cacheTime));
        }

        public virtual bool IsSet(string key)
        {
            return _cache.TryGetValue(key, out object obj);
        }


        public virtual void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
