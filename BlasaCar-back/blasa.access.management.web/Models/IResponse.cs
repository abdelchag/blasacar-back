using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace blasa.access.management.web.Models
{
    public interface IResponse<T>
    {
        //string Message { get; set; }
        //object Data { get; set; }
        //string Status { get; set; }
        //DateTime expiration { get; set; }
        //string token { get; set; }
        //bool Succeeded { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        //public List <string> Errors { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public T Data { get; set; }
        public DateTime expiration { get; set; }
        public string token { get; set; }
    }
}