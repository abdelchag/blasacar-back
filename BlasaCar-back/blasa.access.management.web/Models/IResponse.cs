using System;
using System.IdentityModel.Tokens.Jwt;

namespace blasa.access.management.web.Models
{
    public interface IResponse
    {
        string Message { get; set; }
        object ReturnObject { get; set; }
        string Status { get; set; }
        DateTime expiration { get; set; }
        string token { get; set; }
    }
}