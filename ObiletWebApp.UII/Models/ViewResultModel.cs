using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObiletWebApp.UII.Models
{
    public class ViewResultModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
