
using System.ComponentModel;


namespace ObiletWebApp.Api.Models
{
    public enum  ResponseStatus
    {
        [Description("Success")]
        Success,
        [Description("InvalidDepartureDate")]
        InvalidDepartureDate, 
        [Description("InvalidRoute")]
        InvalidRoute, 
        [Description("InvalidLocation")]
        InvalidLocation, 
        [Description("Timeout")]
        Timeout, 
  
    }
}
