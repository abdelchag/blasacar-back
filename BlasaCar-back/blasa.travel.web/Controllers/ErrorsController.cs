using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using blasa.travel.web.Exceptions;
using blasa.travel.web.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blasa.travel.web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public Error Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var exceptionType = exception.GetType();
            var code = 500; // Internal Server Error by default

            if (exceptionType == typeof(BadRequestException))
                if (exceptionType == typeof(NotFoundException)) code =400;
            //if (exceptionType == typeof(NotFoundException))
            //if (exceptionType == typeof(BadRequestException))
            //    if (exception is NotFoundException) code = 404; // Not Found
            //else if (exception is MyUnauthException) code = 401; // Unauthorized
            //else if (exception is MyException) code = 400; // Bad Request

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new Error(exception); // Your error model
        }
    }
}
