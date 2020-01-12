using System;

namespace Zz.Core.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        T Get<T>(string key, double cacheSeconds, Func<T> acquire);
        void Set(string key, object data, double cacheTime);
        bool IsSet(string key);
        void Remove(string key);

        /*
         * Todo, Hash methods
         *      HGet,HSet,....
         * 
         */
    }
}
