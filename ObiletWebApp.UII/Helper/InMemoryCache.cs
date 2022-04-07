using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ObiletWebApp.UII.Helper
{
    public static class InMemoryCache
    {
       
        public static void setKeyInMemory<T>(string key,T Data, IMemoryCache _memoryCache)
        {
           
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddDays(1);
                _memoryCache.Set<T>(key, Data, options);
               
            
        }
        public static T GetValueInMemory<T>(string key, IMemoryCache _memoryCache)
        {
            
            return _memoryCache.Get<T>(key);
        }
        private static void CachePriority<T>(string key, string Data, IMemoryCache _memoryCache)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.Priority = CacheItemPriority.High;
            //High : Ram dolarsa en son bunu sil 
            //Low : Ram dolarsa ilk bunu sil 
            //NeverRemove : Ram dolsa da silme Not: Exception'a düşme ihtimali var Ram dolarsa
            //Normal : High ile Low arasında
            _memoryCache.Set<string>(key, Data, options);
        }

        public static void DeleteInMemory(string key, IMemoryCache _memoryCache)
        {
            _memoryCache.Remove(key);
        }
    }
}
