using System.Collections.Generic;
using ObiletWebApp.Core.Utilities.Results;
using ObiletWebApp.Models.Models;

namespace ObiletWebApp.Services.Abstract
{
    public interface IObiletService
    {
        IDataResult<SessionResponseModel> GetSession(SessionRequestModel request);
        IDataResult<List<BusLocationListItem>> GetBusLocation(BusLocationRequestModel request);
        IDataResult<List<BusJourneysResponseModel>> GetBusJourneys(BusJourneryRequestModel request);
    }
}
