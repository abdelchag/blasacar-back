using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.access.management.web.Models
{
    public class Response : IResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object ReturnObject { get; set; }
        public DateTime expiration { get; set; }
         public string token { get; set; }
   
}
}
