using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ObiletWebApp.Api.Models;
using ObiletWebApp.Core.Extentions;
using ObiletWebApp.Models.Models;
using ObiletWebApp.Services.Abstract;
using ObiletWebApp.UII.Helper;
using ObiletWebApp.UII.Models;
using Wangkanai.Detection;

namespace ObiletWebApp.UII.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _memoryCache;
        private readonly IObiletService _obiletService;
        private static IHttpContextAccessor _contextAccessor;
        private readonly IBrowserResolver _detection;
        private readonly string _ipAdress;
        private readonly INotyfService _notify;
        public HomeController(ILogger<HomeController> logger, IObiletService obiletService, IMemoryCache memoryCache, IHttpContextAccessor contextAccessor, IBrowserResolver detection, INotyfService notifyService)
        {
            _obiletService = obiletService;
            _logger = logger;
            _memoryCache = memoryCache;
            _contextAccessor = contextAccessor;
            _detection = detection;
            _notify = notifyService;
            _ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        public IActionResult Index(BusLocationViewModel model)
        {
            model.Date = DateTime.Now.AddDays(1);
            var sessionData = SessionDataSet();
            if (sessionData.Success)
            {
                var informationModels = UserBusLocationInformationGet(sessionData.Data.SessionId);
                if (informationModels != null)
                {
                    model.Date = informationModels.Date;
                    model.OriginId = informationModels.OriginId;
                    model.DestinationId = informationModels.DestinationId;
                }

                BusLocationRequestModel busrequest = new BusLocationRequestModel()
                {
                    DeviceSession = sessionData.Data,
                    Date = DateTime.Now,
                    Language = "tr-TR",
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId
                };
                var resultBusLocation = _obiletService.GetBusLocation(busrequest);
                if (resultBusLocation.Success) model.BusLocationList = resultBusLocation.Data;
                else _notify.Error(resultBusLocation.Message);
            }
            else
            {
                _notify.Error(sessionData.Message);

            }

            return View(model);
        }

        public IActionResult BusLocationSearch(string Search)
        {
            var sessionData = InMemoryCache.GetValueInMemory<SessionResponseModel>(_ipAdress, _memoryCache);
            BusLocationRequestModel busrequest = new BusLocationRequestModel()
            {
                DeviceSession = sessionData,
                Date = DateTime.Now,
                Language = "tr-TR",
                Data = Search

            };
            var resultBusLocation = _obiletService.GetBusLocation(busrequest);
            var data = string.Empty;
            if (resultBusLocation.Success)
            {
                foreach (var item in resultBusLocation.Data)
                {
                    data += "<option value=" + item.Id + "> " + item.Name + "</option>";
                }
            }
            return Json(new
            {
                result = true,
                message = "İşlem Başarılı",
                Object = data,
            });
        }

        public IActionResult BusJourney(BusJourneryRequestModel model)
        {
            var viewmodel = new BusJourneysViewModel();

            model.DeviceSession = InMemoryCache.GetValueInMemory<SessionResponseModel>(_ipAdress, _memoryCache);
            model.Language = "tr-TR";
            var resultBusJourneys = _obiletService.GetBusJourneys(model);
            if (resultBusJourneys.Success)
            {
                var informationModels = new UserBusLocationInformationModels()
                {
                    Date = model.Date,
                    OriginId = model.OriginId,
                    DestinationId = model.DestinationId
                };
                UserBusLocationInformationSet(model.DeviceSession.SessionId, informationModels);
                viewmodel.BusJourneysList = resultBusJourneys.Data;
                viewmodel.DestinationLocation = resultBusJourneys.Data.First().DestinationLocation;
                viewmodel.OriginLocation = resultBusJourneys.Data.First().OriginLocation;
                viewmodel.Date = model.Date;
            }
            else
            {
                _notify.Error(resultBusJourneys.Message);
            }
            return View(viewmodel);
        }



        public ViewResultModel<SessionResponseModel> SessionDataSet()
        {
            ViewResultModel<SessionResponseModel> model = new ViewResultModel<SessionResponseModel>() { Success = true };
            
               var sessionData = new SessionResponseModel();
            if (!_memoryCache.TryGetValue(_ipAdress, out sessionData))
            {
                SessionRequestModel request = new SessionRequestModel()
                {
                    BrowserName = _detection.Browser.Type.GetDescription(),
                    BrowserVersion = _detection.Browser.Version.ToString(),
                    IpAdress = _ipAdress,
                    Port = _contextAccessor.HttpContext.Connection.RemotePort.ToString(),
                    Type = 7,

                };
                var result = _obiletService.GetSession(request);
                sessionData = result.Data;
                if (result.Success)
                {
                    InMemoryCache.setKeyInMemory<SessionResponseModel>(_ipAdress, result.Data, _memoryCache);
                    model.Data = sessionData;
                    return model;
                }
                else
                {
                    model.Success = false;
                    model.Message = result.Message;
                    return model;
              
                }
            }
            else
            {
                model.Data = InMemoryCache.GetValueInMemory<SessionResponseModel>(_ipAdress, _memoryCache);
                return model;
             
            }

           
        }
        public UserBusLocationInformationModels UserBusLocationInformationGet(string sessionId)
        {
            var informationModels = new UserBusLocationInformationModels();
            if (_memoryCache.TryGetValue(sessionId, out informationModels))
            {
                return InMemoryCache.GetValueInMemory<UserBusLocationInformationModels>(sessionId, _memoryCache);
            }
            return null;
        }
        public void UserBusLocationInformationSet(string sessionId, UserBusLocationInformationModels informationModels)
        {
            var informationModelsmemory = new UserBusLocationInformationModels();
            if (!_memoryCache.TryGetValue(sessionId, out informationModelsmemory))
            {

                InMemoryCache.setKeyInMemory<UserBusLocationInformationModels>(sessionId,
                    informationModels, _memoryCache);
            }
            else
            {
                InMemoryCache.DeleteInMemory(sessionId, _memoryCache);
                InMemoryCache.setKeyInMemory<UserBusLocationInformationModels>(sessionId,
                    informationModels, _memoryCache);
            }

        }
        ~HomeController()
        {
            InMemoryCache.DeleteInMemory(_ipAdress, _memoryCache);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
