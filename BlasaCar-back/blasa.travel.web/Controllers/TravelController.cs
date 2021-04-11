using System.Collections.Generic;
using System.Security.Claims;
 
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using AutoMapper;
using blasa.travel.Core.Application.Commands;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.web.Exceptions;
using blasa.travel.web.Filtre;
using blasa.travel.web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tools.Constants;
using Microsoft.Extensions.Logging;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blasa.travel.web.Controllers
{

     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/travel")]

    [ApiController]
    //Adopter of InputPort
    public class TravelController : ControllerBase
    {


        private readonly IGenericCommandAsync<Travel> _TravelGenericServices;
        private readonly ITravelCommandAsync _UserCommandAsyncServices;
        private readonly IMapper _mapper;
        private readonly ILogger<TravelController> _logger;
        public TravelController(ILogger<TravelController> logger,IGenericCommandAsync<Travel> TravelGenericServices, ITravelCommandAsync UserCommandAsyncServices, IMapper mapper)
        {
            _TravelGenericServices = TravelGenericServices;
            _UserCommandAsyncServices = UserCommandAsyncServices;
            _mapper = mapper;
            _logger = logger;
        }


        // POST api/<TravelsController>
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TravelDTO _travelDto)

        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            string userId = null;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = identity.FindFirst("userId").Value;

            }
            if (userId == null)
            {
                _logger.LogError("Le user ID dans le claims  est null !");
                throw new NotAuthorizException(ErrorConstants.BLASACARUnauthorized);
                
            }

            var TravelEntity = _mapper.Map<Travel>(_travelDto);
            TravelEntity.Userid = userId;
            var newTravelResult = await _TravelGenericServices.AddAsync(TravelEntity);
            if (newTravelResult is null)
            {
                _logger.LogError("Un problème au niveau de la création du Travel est survenu ");
                throw new BadRequestException(ErrorConstants.BLASACARTravelfailedCreation);
            }

            return Ok(newTravelResult);


        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {


            var TravelResult = await _TravelGenericServices.DeleteAsync(id);

            if (TravelResult is null)
            {

                throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);
            }

            return Ok(_mapper.Map<TravelDTO>(TravelResult));
        }

 
        [HttpGet ]
        public async Task<IActionResult> GetAllAsync([FromQuery] bool onlyUser)
 

        {
            var listTravelResult = await Task.FromResult<IReadOnlyList<Travel>>(Array.Empty<Travel>());

            if (onlyUser)
            {
                // User
                string userId = null;
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    userId = identity.FindFirst("userId").Value;

                }
                if (userId == null)
                {
                    _logger.LogError("Le user ID dans le claims  est null !");
                    throw new NotAuthorizException(ErrorConstants.BLASACARUnauthorized);
                }
                listTravelResult = await _UserCommandAsyncServices.GetTravelByUserIdAsync(userId);
            }else
            {
                // All
                listTravelResult = await _TravelGenericServices.GetAllAsync();
            }

            if (listTravelResult is null)
            {
                listTravelResult = await Task.FromResult<IReadOnlyList<Travel>>(Array.Empty<Travel>());
            }
            return Ok(_mapper.Map<IReadOnlyList<TravelDTO>>(listTravelResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var TravelResult = await _TravelGenericServices.GetByIdAsync(id);
            if (TravelResult is null)
            {
                throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);
            }

            return Ok(_mapper.Map<TravelDTO>(TravelResult));
        }

        //[HttpGet("{pageNumber, pageSize}")]
        //public async Task<IActionResult> GetPagedReponseAsync([FromQuery]  int pageNumber, int pageSize)
        //{
        //    var ListTravelResult = await _TravelGenericServices.GetPagedReponseAsync(pageNumber, pageSize);

        //    if (ListTravelResult is null)
        //    {

        //        throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);
        //    }

        //    return Ok(_mapper.Map<TravelDTO>(ListTravelResult));
        //}
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TravelDTO _travelDto)
        {
            var TravelEntity = _mapper.Map<Travel>(_travelDto);

            string userId = null;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                userId = identity.FindFirst("userId").Value;

            }
            if (userId == null) throw new NotAuthorizException(ErrorConstants.BLASACARUnauthorized);

            TravelEntity.Userid = userId;

            var newTravelResult = await _TravelGenericServices.UpdateAsync(TravelEntity);

            if (newTravelResult is null)
            {
                _logger.LogError("Le Travel ID : " +  (string.IsNullOrEmpty(TravelEntity.Id.ToString()) ? "Null" : TravelEntity.Id.ToString()) + ", n'existe pas dans la base de données");
                throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);  
            }

            return Ok(_mapper.Map<TravelDTO>(newTravelResult));


        }


    }
}
