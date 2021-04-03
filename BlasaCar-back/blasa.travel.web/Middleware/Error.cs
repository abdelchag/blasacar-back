using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.travel.web.Middleware
{
    public class Error
    {

        public Error()
        { }
        public string message { get; set; }
        public Error(Exception ex)
        {
            //Type = ex.GetType().Name;
            message = ex.Message;
            //StackTrace = ex.ToString();
        }
    }
}
