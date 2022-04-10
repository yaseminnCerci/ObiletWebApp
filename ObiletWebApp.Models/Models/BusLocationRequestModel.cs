using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletWebApp.Models.Models
{
   public class BusLocationRequestModel
    {
        public SessionResponseModel DeviceSession { get; set; }
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }

    }
}
