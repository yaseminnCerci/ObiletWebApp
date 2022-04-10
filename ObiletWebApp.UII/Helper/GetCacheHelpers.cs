using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObiletWebApp.Models.Models;

namespace ObiletWebApp.UII.Helper
{
    public static  class GetCacheHelpers
    {
        //public void GetSession()
        //{
        //    var sessionData = new SessionResponseModel();

        //    var ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        //    if (!_memoryCache.TryGetValue(ipAdress, out sessionData))
        //    {
        //        SessionRequestModel request = new SessionRequestModel()
        //        {
        //            BrowserName = _detection.Browser.Type.GetDescription(),
        //            BrowserVersion = _detection.Browser.Version.ToString(),
        //            IpAdress = ipAdress,
        //            Port = _contextAccessor.HttpContext.Connection.RemotePort.ToString(),
        //            Type = 7,

        //        };
        //        var result = _obiletService.GetSession(request);
        //        sessionData = result.Data;
        //        if (result.Success)
        //        {
        //            InMemoryCache.setKeyInMemory<SessionResponseModel>(ipAdress, result.Data, _memoryCache);
        //        }
        //    }
        //    else
        //    {
        //        sessionData = InMemoryCache.GetValueInMemory<SessionResponseModel>(ipAdress, _memoryCache);
        //    }

        //}
    }
}
