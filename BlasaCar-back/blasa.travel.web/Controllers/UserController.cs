using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blasa.travel.Core.Application.Commands;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using blasa.travel.web.Wrapper;
using blasa.travel.web.Exceptions;
using Tools.Constants;
using blasa.travel.web.Filtre;
using System.Security.Claims;

namespace blasa.travel.web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/user/travel")]

    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITravelCommandAsync _UserCommandAsyncServices;
        private readonly IMapper _mapper;
        public UserController(ITravelCommandAsync UserCommandAsyncServices, IMapper mapper)
        {
            _UserCommandAsyncServices = UserCommandAsyncServices;
            _mapper = mapper;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetTravelByUserIdAsync ()

        {
            string userId = null;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = identity.FindFirst("userId").Value;

            }
            if (userId == null) throw new NotAuthorizException(ErrorConstants.BLASACARUnauthorized);
            var listTravelResult = await _UserCommandAsyncServices.GetTravelByUserIdAsync(userId);
            if (listTravelResult is null)
            {
                throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);
            }
            return Ok(_mapper.Map<IReadOnlyList<TravelDTO>>(listTravelResult));
        }

    }
}
