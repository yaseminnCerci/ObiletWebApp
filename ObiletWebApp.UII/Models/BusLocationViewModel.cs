using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObiletWebApp.Models.Models;

namespace ObiletWebApp.UII.Models
{
    public class BusLocationViewModel
    {
        public DateTime Date { get; set; }
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }
        public List<BusLocationListItem> BusLocationList { get; set; }
    }
}
