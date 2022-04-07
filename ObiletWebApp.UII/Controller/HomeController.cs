using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ObiletWebApp.Core.Extentions;
using ObiletWebApp.Models.Models;
using ObiletWebApp.Services.Abstract;
using ObiletWebApp.UII.Helper;
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
        public HomeController(ILogger<HomeController> logger, IObiletService obiletService, IMemoryCache memoryCache,IHttpContextAccessor contextAccessor, IBrowserResolver detection)
        {
            _obiletService = obiletService;
            _logger = logger;
            _memoryCache = memoryCache;
            _contextAccessor = contextAccessor;
            _detection = detection;
        }

        public IActionResult Index()
        {
            var data = new SessionResponseModel();

            var ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!_memoryCache.TryGetValue(ipAdress, out data))
            {
                SessionRequestModel request = new SessionRequestModel()
            {
                BrowserName = _detection.Browser.Type.GetDescription(),
                BrowserVersion = _detection.Browser.Version.ToString(),
                IpAdress = ipAdress,
                Port = _contextAccessor.HttpContext.Connection.RemotePort.ToString(),
                Type=7,

            };
            var result= _obiletService.GetSession(request);
            data = result.Data;
            if (result.Success)
            {
               
                    InMemoryCache.setKeyInMemory<SessionResponseModel>(ipAdress, result.Data,_memoryCache);
                
            }
            }
            else
            {
                data=InMemoryCache.GetValueInMemory<SessionResponseModel>(ipAdress, _memoryCache);

            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        ~HomeController()
        {
        //InMemoryCache.DeleteInMemory("a");
        //InMemoryCache.setKeyInMemory("aa","hh");
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
