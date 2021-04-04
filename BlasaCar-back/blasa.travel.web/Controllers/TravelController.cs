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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blasa.travel.web.Controllers
{
    //[Authorize]
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
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        // POST api/<TravelsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]  TravelModel _travelDto    )
         
        {
            //throw new Exception("test");
            var TravelEntity = _mapper.Map<Travel>(_travelDto);
            //var newTravelResult = await _TravelGenericServices.AddAsync(TravelEntity);
            var newTravelResult = await _TravelGenericServices.AddAsync(TravelEntity);

            if (newTravelResult is null)
            {

                  throw new BadRequestException(ErrorConstants.BLASACARTravelfailedCreation);
            }

            return Ok( newTravelResult);
            

        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // POST api/<TravelsController>
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()

        {
            //throw new Exception("test");

            //var newTravelResult = await _TravelGenericServices.AddAsync(TravelEntity);
            var travels = await _TravelGenericServices.GetAllAsync();

            if (travels is null)
            {
                throw new BadRequestException("BLASACAR_Travel_failed_Creation");
            }
            return Ok(travels);
        }

        [HttpPut]
        public async Task<IActionResult> EditTravel([FromBody] TravelModel _travelDto)
        {
            var TravelEntity = _mapper.Map<Travel>(_travelDto);
            var travel = await _TravelGenericServices.UpdateAsync(TravelEntity);

            if (travel is null)
            {
                throw new BadRequestException("BLASACAR_Travel_failed_Edition");
            }
            return Ok(travel);
        }


    }
}
