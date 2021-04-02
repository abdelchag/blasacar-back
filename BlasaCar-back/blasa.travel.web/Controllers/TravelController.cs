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
        private readonly IMapper _mapper;
        public TravelController(IGenericCommandAsync<Travel> TravelGenericServices, IMapper mapper)
        {
            _TravelGenericServices = TravelGenericServices;
            _mapper = mapper;
           // HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
        }


        // POST api/<TravelsController>
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]  TravelDTO _travelDto    )
         
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
               
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId=identity.FindFirst("userId").Value;

            }

            var TravelEntity = _mapper.Map<Travel>(_travelDto);           
            var newTravelResult = await _TravelGenericServices.AddAsync(TravelEntity);
            if (newTravelResult is null)
            {

                  throw new BadRequestException(ErrorConstants.BLASACARTravelfailedCreation);
            }

            return Ok( newTravelResult);
            

        }

        


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            

            var  TravelResult = await _TravelGenericServices.DeleteAsync(id);

            if (TravelResult is null)
            {

                throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);
            }

            return Ok(_mapper.Map<TravelDTO>(TravelResult));
        }

        [HttpGet ]
        public async Task<IActionResult> GetAllAsync()

        {
            var listTravelResult = await _TravelGenericServices.GetAllAsync();
            if (listTravelResult is null)
            {
                throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException);
            }
            return Ok(_mapper.Map<IReadOnlyList<TravelDTO>>(listTravelResult));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {          
           var  TravelResult = await _TravelGenericServices.GetByIdAsync(id);
            if ( TravelResult is null)
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
        public async Task<IActionResult> UpdateAsync ([FromBody] TravelDTO _travelDto)
        {            
            var TravelEntity = _mapper.Map<Travel>(_travelDto);
          
            var newTravelResult = await _TravelGenericServices.UpdateAsync(TravelEntity);

            if (newTravelResult is null)
            {
                  throw new NotFoundException(ErrorConstants.BLASACARTravelNotFoundException); ;
            }

            return Ok(_mapper.Map<TravelDTO>(newTravelResult));


        }


    }
}
