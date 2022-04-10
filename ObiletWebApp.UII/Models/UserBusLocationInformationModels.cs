using System;
namespace ObiletWebApp.UII.Models
{
    public class UserBusLocationInformationModels
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
    }
}
