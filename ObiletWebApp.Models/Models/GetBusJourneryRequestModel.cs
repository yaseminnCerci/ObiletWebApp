using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ObiletWebApp.Models.Models
{
  public  class GetBusJourneryRequestModel
    {
        public DateTime Date { get; set; }
        public string OriginId { get; set; }
        public string DestinationId { get; set; }
        public SessionResponseModel DeviceSession { get; set; }
        public string Language { get; set; }
    }
}
