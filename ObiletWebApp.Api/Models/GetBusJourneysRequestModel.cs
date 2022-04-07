using Newtonsoft.Json;

namespace ObiletWebApp.Api.Models
{
  public  class GetBusJourneysRequestModel
    {
        [JsonProperty("origin-id")]
        public int OriginId { get; set; }

        [JsonProperty("destination-id")]

        public int DestinationId { get; set; }

        [JsonProperty("departure-date")]

        public string DepartureDate { get; set; }
    }
}
