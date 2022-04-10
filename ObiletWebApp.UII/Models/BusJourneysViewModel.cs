using System;
using System.Collections.Generic;
using ObiletWebApp.Models.Models;

namespace ObiletWebApp.UII.Models
{
    public class BusJourneysViewModel
    {
        public BusJourneysViewModel()
        {
            BusJourneysList = new List<BusJourneysResponseModel>();
        }
        public string OriginLocation { get; set; }
        public DateTime Date { get; set; }
        public string DestinationLocation { get; set; }
        public List<BusJourneysResponseModel> BusJourneysList { get; set; }
    }
}
