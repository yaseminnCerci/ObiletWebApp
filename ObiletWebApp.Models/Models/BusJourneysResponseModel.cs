using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletWebApp.Models.Models
{
  public  class BusJourneysResponseModel
    {
        public string PartnerName { get; set; }
        public long PartnerId { get; set; }
        public long Id { get; set; }
        public string BusType { get; set; }
        public List<Stations> Stops { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public double Price { get; set; }
        public string PriceType { get; set; }
        public List<string> Features { get; set; }
        public string Description { get; set; }
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public long OriginLocationId { get; set; }
        public long DestinationLocationId { get; set; }
    }

  public class Stations
  {
      public string Name { get; set; }
      public string Station { get; set; }
      public DateTime? Time { get; set; }
  

    }
}
