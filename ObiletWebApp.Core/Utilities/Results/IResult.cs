using System;
using System.Collections.Generic;
using System.Text;

namespace ObiletWebApp.Core.Utilities.Results
{
    public interface IResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
