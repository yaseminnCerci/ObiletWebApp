using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ObiletWebApp.Core.Extentions;
using ObiletWebApp.Models.Models;
using ObiletWebApp.Services.Abstract;
using ObiletWebApp.UII.Helper;
using ObiletWebApp.UII.Models;
using Wangkanai.Detection;
using IDetectionBuilder = Microsoft.Extensions.DependencyInjection.IDetectionBuilder;

namespace ObiletWebApp.UII.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _memoryCache;
        private readonly IObiletService _obiletService;
        private static IHttpContextAccessor _contextAccessor;
        private readonly IBrowserResolver _detection;
        public HomeController(ILogger<HomeController> logger, IObiletService obiletService, IMemoryCache memoryCache, IHttpContextAccessor contextAccessor, IBrowserResolver detection)
        {
            _obiletService = obiletService;
            _logger = logger;
            _memoryCache = memoryCache;
            _contextAccessor = contextAccessor;
            _detection = detection;
        }

        public IActionResult Index(BusLocationViewModel model)
        {
            var sessionData = new SessionResponseModel();

            var ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!_memoryCache.TryGetValue(ipAdress, out sessionData))
            {
                SessionRequestModel request = new SessionRequestModel()
                {
                    BrowserName = _detection.Browser.Type.GetDescription(),
                    BrowserVersion = _detection.Browser.Version.ToString(),
                    IpAdress = ipAdress,
                    Port = _contextAccessor.HttpContext.Connection.RemotePort.ToString(),
                    Type = 7,

                };
                var result = _obiletService.GetSession(request);
                sessionData = result.Data;
                if (result.Success)
                {
                    InMemoryCache.setKeyInMemory<SessionResponseModel>(ipAdress, result.Data, _memoryCache);
                }
            }
            else
            {
                sessionData = InMemoryCache.GetValueInMemory<SessionResponseModel>(ipAdress, _memoryCache);
            }


            //var busLocationData = new List<BusLocationListItem>();
            //if (!_memoryCache.TryGetValue("busLocation", out busLocationData))
            //{
                BusLocationRequestModel busrequest = new BusLocationRequestModel()
                {
                    DeviceSession = sessionData,
                    Date = DateTime.Now,
                    Language = "tr-TR"
                }; 
                var resultBusLocation = _obiletService.GetBusLocation(busrequest);
            //    busLocationData = result.Data;
                
            //    if (result.Success)
            //    {
            //        InMemoryCache.setKeyInMemory<List<BusLocationListItem>>("busLocation", busLocationData, _memoryCache);
            //    }

            //}
            //else
            //{
            //    busLocationData = InMemoryCache.GetValueInMemory<List<BusLocationListItem>>("busLocation", _memoryCache);
            //}

            model.BusLocationList = resultBusLocation.Data;

            return View(model);
        }

        public IActionResult BusLocationSearch(string Search)
        {
           var  sessionData = InMemoryCache.GetValueInMemory<SessionResponseModel>(_contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), _memoryCache);
            BusLocationRequestModel busrequest = new BusLocationRequestModel()
            {
                DeviceSession = sessionData,
                Date = DateTime.Now,
                Language = "tr-TR",Data = Search
                
            };
            var resultBusLocation = _obiletService.GetBusLocation(busrequest);
            var data = "";
            foreach (var item in resultBusLocation.Data)
            {
                data += "<option value="+ item.Id +"> " + item.Name + "</option>";
            }
            return Json(new
            {
                result = true,//result.Success,
                message = "İşlem Başarılı",
                Object = data,// result.Object.Id
            });
        }
       public IActionResult Privacy(GetBusJourneryRequestModel model)
        {
          
             model.DeviceSession = InMemoryCache.GetValueInMemory<SessionResponseModel>(_contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), _memoryCache),
             model.Language = "tr-TR",
                
          
            var resultBusLocation = _obiletService.GetBusLocation(model);
            return View();
        }       
        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        ~HomeController()
        {
       
            //InMemoryCache.setKeyInMemory("aa","hh");
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
