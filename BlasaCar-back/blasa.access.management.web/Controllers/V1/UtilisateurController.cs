using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blasa.access.management.Core.Application.Features.Users.Commands.CreateUser;
using blasa.access.management.Core.Application.Features.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blasa.access.management.web.Controllers.V1
{
    
     
     
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
