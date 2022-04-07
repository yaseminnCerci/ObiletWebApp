using Newtonsoft.Json;

namespace ObiletWebApp.Api.Models
{
   public class GetSessionResponseModel
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }

        [JsonProperty("affiliate")]
        public string Affiliate { get; set; }

        [JsonProperty("device-type")]
        public int DeviceType { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }
    }
}
