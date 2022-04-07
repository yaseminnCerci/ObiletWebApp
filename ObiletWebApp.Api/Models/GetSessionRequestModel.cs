
using Newtonsoft.Json;

namespace ObiletWebApp.Api.Models
{
  public  class GetSessionRequestModel
    {
       public GetSessionRequestModel()
        {
            Connection = new Connections();
            Browser = new Browsers();
        }
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("connection")]
        public Connections Connection { get; set; }

        [JsonProperty("browser")]
        public Browsers Browser { get; set; }

    }

  public class Connections
  {
      [JsonProperty("ip-address")]
      public string IpAddress { get; set; }
      [JsonProperty("port")]
      public string Port { get; set; }
    }
  public class Browsers
    {
      [JsonProperty("version")]
      public string Version { get; set; }
     
      [JsonProperty("name")]
      public string Name { get; set; }
    }
}
