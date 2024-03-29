﻿using System.Collections.Generic;
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
