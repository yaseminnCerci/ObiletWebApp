using System.Collections.Generic;
using System.Linq;
using ObiletWebApp.Api.Abstract;
using ObiletWebApp.Api.Models;
using ObiletWebApp.Core.Utilities.Results;
using ObiletWebApp.Models.Models;
using ObiletWebApp.Services.Abstract;

namespace ObiletWebApp.Services.Concrete
{
    public class ObiletService : IObiletService
    {
        private readonly IObiletApi _obiletApi;

        public ObiletService(IObiletApi obiletApi)
        {
            _obiletApi = obiletApi;
        }

        public IDataResult<SessionResponseModel> GetSession(SessionRequestModel request)
        {

            GetSessionRequestModel requestmodel = new GetSessionRequestModel()
            {
                Type = request.Type,
                Connection = { IpAddress = request.IpAdress, Port = request.Port },
                Browser = { Name = request.BrowserName, Version = request.BrowserVersion }

            };
            var result = _obiletApi.GetSession(requestmodel);
            if (result.Status == ResponseStatus.Success)
            {
                return new DataResult<SessionResponseModel>(new SessionResponseModel(){SessionId = result.Data.SessionId,DeviceId = result.Data.DeviceId}, true);
            }
            else
            {
                return new DataResult<SessionResponseModel>(new SessionResponseModel(), false, result.Message);
            }

        }

        public IDataResult<List<BusLocationListItem>> GetBusLocation(BusLocationRequestModel request)
        {

            RequestBaseModel<string> requestmodel = new RequestBaseModel<string>()
            {
           DeviceSession = {DeviceId = request.DeviceSession.DeviceId,SessionId = request.DeviceSession.SessionId},
           Data = request.Data,
           Date = request.Date,
           Language = request.Language
            };
            var result = _obiletApi.GetBusLocation(requestmodel);
            if (result.Status == ResponseStatus.Success)
            {
                var data = new List<BusLocationListItem>();
                if (result.Data.Count > 7)
                {
                    data = result.Data.GetRange(0, 7).Select(x => new BusLocationListItem {Id = x.Id, Name = x.Name})
                        .ToList();
                }
                else
                {
                    data = result.Data.Select(x => new BusLocationListItem { Id = x.Id, Name = x.Name })
                        .ToList();
                }

                return new DataResult<List<BusLocationListItem>>(data, true);
            }
            else
            {
                return new DataResult<List<BusLocationListItem>>(new List<BusLocationListItem>(), false, result.Message);
            }

        }

        public IDataResult<List<BusLocationListItem>> GetBusJourneys(GetBusJourneryRequestModel request)
        {

            RequestBaseModel<string> requestmodel = new RequestBaseModel<string>()
            {
                DeviceSession = { DeviceId = request.DeviceSession.DeviceId, SessionId = request.DeviceSession.SessionId },
             
                Date = request.Date,
                Language = request.Language
            };
            return null;
        }
    }
}
