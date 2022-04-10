using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            var informationModels = new UserBusLocationInformationModels();
            if (_memoryCache.TryGetValue(sessionData.SessionId, out informationModels))
            {
                informationModels = InMemoryCache.GetValueInMemory<UserBusLocationInformationModels>(sessionData.SessionId, _memoryCache);
                model.Date = informationModels.Date;
                model.OriginId = informationModels.OriginId;
                model.DestinationId = informationModels.DestinationId;
            }
            else
            {
                model.Date = DateTime.Now.AddDays(1);
            }
            //var busLocationData = new List<BusLocationListItem>();
            //if (!_memoryCache.TryGetValue("busLocation", out busLocationData))
            //{
            BusLocationRequestModel busrequest = new BusLocationRequestModel()
                {
                    DeviceSession = sessionData,
                    Date = DateTime.Now,
                    Language = "tr-TR",
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId
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
                
            };  var resultBusLocation = _obiletService.GetBusLocation(busrequest);
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

        public IActionResult BusJourney(BusJourneryRequestModel model)
        {
            var viewmodel = new BusJourneysViewModel();
            var ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            model.DeviceSession = InMemoryCache.GetValueInMemory<SessionResponseModel>(ipAdress, _memoryCache);
            model.Language = "tr-TR";
            var resultBusLocation = _obiletService.GetBusJourneys(model);


            if (resultBusLocation.Success)
            {
                var informationModels = new UserBusLocationInformationModels();
            if (!_memoryCache.TryGetValue(model.DeviceSession.SessionId, out informationModels))
            {
                informationModels = new UserBusLocationInformationModels()
                {
                    Date = model.Date,
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId
                };
                    InMemoryCache.setKeyInMemory<UserBusLocationInformationModels>(model.DeviceSession.SessionId,
                    informationModels, _memoryCache);
            }
            else
            {
                informationModels = new UserBusLocationInformationModels()
                {
                    Date = model.Date,
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId
                };
                    InMemoryCache.DeleteInMemory(model.DeviceSession.SessionId, _memoryCache);
                InMemoryCache.setKeyInMemory<UserBusLocationInformationModels>(model.DeviceSession.SessionId,
                    informationModels, _memoryCache);
            }

            viewmodel.BusJourneysList = resultBusLocation.Data;
            viewmodel.DestinationLocation = resultBusLocation.Data.First().DestinationLocation;
            viewmodel.OriginLocation = resultBusLocation.Data.First().OriginLocation;
            viewmodel.Date = model.Date;
        }
        return View(viewmodel);
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
