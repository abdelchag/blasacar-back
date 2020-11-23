using System;
using System.IdentityModel.Tokens.Jwt;
namespace blasa.access.management.web.Models
{
    public interface IToken
    {
        DateTime Expiration { get; set; }
        string Token { get; set; }
    }
}
