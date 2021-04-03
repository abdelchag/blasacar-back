using System;

namespace blasa.travel.web.Exceptions
{
    public class NotAuthorizException : Exception
    {
        public NotAuthorizException(string message) : base(message)
        { }
    }
}
