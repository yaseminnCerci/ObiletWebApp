using System;

namespace ObiletWebApp.Models.Models
{
   public class BusLocationRequestModel
    {
        public SessionResponseModel DeviceSession { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }
    }
}
