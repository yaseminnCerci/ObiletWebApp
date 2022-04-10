using Newtonsoft.Json;

namespace ObiletWebApp.Api.Models
{
   public  class ResponseBaseModel<T>
    {
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }
    
        [JsonProperty("message")]
        public string Message { get; set; }
       
        [JsonProperty("user-message")]
        public string UserMessage { get; set; }

        [JsonProperty("api-request-id")]
        public string ApiRequestId { get; set; }
      
        [JsonProperty("controller")]
        public string Controller { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
