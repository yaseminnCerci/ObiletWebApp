using System;
namespace ObiletWebApp.Models.Models
{
    public class BusJourneryRequestModel
    {
        public BusJourneryRequestModel()
        {
            DeviceSession = new SessionResponseModel();
        }
        public DateTime Date { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public SessionResponseModel DeviceSession { get; set; }
        public string Language { get; set; }
    }
}
