using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObiletWebApp.Api.Models;

namespace ObiletWebApp.Api.Abstract
{
    public interface  IObiletApi
    {
        ResponseBaseModel<GetSessionResponseModel> GetSession(GetSessionRequestModel request); 
        ResponseBaseModel<List<GetBusLocationResponseModel>> GetBusLocation(RequestBaseModel<string> request);

         ResponseBaseModel<List<GetBusJourneysResponseModel>> GetBusJourneys(RequestBaseModel<GetBusJourneysRequestModel> request);
    }
}
