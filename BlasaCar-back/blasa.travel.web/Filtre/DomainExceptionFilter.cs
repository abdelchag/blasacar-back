using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
namespace blasa.travel.web.Filtre
{
    
        
        public sealed class DomainExceptionFilter : IExceptionFilter
        {
            public void OnException(ExceptionContext context)
            {
                Exception domainException = context.Exception as Exception;
                if (domainException != null)
                {
                    string json = JsonConvert.SerializeObject(domainException.Message);

                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                ApplicationException applicationException = context.Exception as ApplicationException;
                if (applicationException != null)
                {
                    string json = JsonConvert.SerializeObject(applicationException.Message);

                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

            Exception infrastructureException = context.Exception as Exception;
                if (infrastructureException != null)
                {
                    string json = JsonConvert.SerializeObject(infrastructureException.Message);

                    context.Result = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
        }
     

}
