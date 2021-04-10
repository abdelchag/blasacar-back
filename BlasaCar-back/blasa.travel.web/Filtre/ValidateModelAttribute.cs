using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using blasa.travel.web.Middleware;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace blasa.travel.web.Filtre
{
   

    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
      //  private readonly ILogger<ValidateModelAttribute> _logger;
      //public  ValidateModelAttribute(ILogger<ValidateModelAttribute> logger)
      //  {

      //      _logger = logger;
      //  }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                context.Result = new BadRequestObjectResult(context.ModelState);
                // var allErrors = context.ModelState.Values.SelectMany(
                //v => v.Errors.Select(b => new Error { message = b.ErrorMessage }))
                //             .ToList<Error>();
                // _logger.LogError("allErrors");
                //var result = new BadRequestObjectResult(allErrors);
                //result.ContentTypes.Add(MediaTypeNames.Application.Json);



            }
        }
    }
}
