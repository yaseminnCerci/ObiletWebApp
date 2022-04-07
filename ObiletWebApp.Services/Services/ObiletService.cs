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
    }
}
