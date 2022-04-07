using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ObiletWebApp.UII.Helper
{
    public static class InMemoryCache
    {
        private static IMemoryCache _memoryCache;

        static InMemoryCache()
        {
         
        }
        public static void Configure(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

        }
        public static void setKeyInMemory<T>(string key,T Data)
        {
           
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddDays(1);
                _memoryCache.Set<T>(key, Data, options);
               
            
        }
        private static T GetValueInMemory<T>(string key, T Data)
        {
            _memoryCache.TryGetValue<T> (key, out Data);  // varsa alır yoksa oluşturur.
            return _memoryCache.Get<T>(key);
        }
        private static void CachePriority<T>(string key, string Data)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.Priority = CacheItemPriority.High;
            //High : Ram dolarsa en son bunu sil 
            //Low : Ram dolarsa ilk bunu sil 
            //NeverRemove : Ram dolsa da silme Not: Exception'a düşme ihtimali var Ram dolarsa
            //Normal : High ile Low arasında
            _memoryCache.Set<string>(key, Data, options);
        }

        public static void DeleteInMemory(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
