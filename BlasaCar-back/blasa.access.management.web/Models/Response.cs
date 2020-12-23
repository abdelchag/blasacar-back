﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
namespace blasa.access.management.web.Models
{
    public class Response<T> : IResponse<T>
    {
        //public string Status { get; set; }
        //public string Message { get; set; }
        //public object Data { get; set; }
       


        public Response()
        {
        }
        public Response(T data, string token, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            this.token = token;
        }
        public Response(string Message, List<IdentityError> errors = null)
        {
            Succeeded = false;
            this.Message = Message;
            Errors = errors;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        //public List<string> Errors { get; set; }
        List<IdentityError> Errors { get; set; }
        public T Data { get; set; }
        public DateTime expiration { get; set; }
        public string token { get; set; }
        IEnumerable<IdentityError> IResponse<T>.Errors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
