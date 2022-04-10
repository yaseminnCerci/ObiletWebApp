using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ObiletWebApp.Api.Models
{
    public class RequestBaseModel<T>
    {
       
        public RequestBaseModel()
        {
            DeviceSession = new DeviceSession();
        }
        [JsonProperty("device-session")]
        public DeviceSession DeviceSession { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
