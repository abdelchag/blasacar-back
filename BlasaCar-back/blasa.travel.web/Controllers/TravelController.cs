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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blasa.travel.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {


        private readonly IGenericCommandAsync<Travel> TravelGenericServices;
        private readonly IMapper _mapper;
        public TravelController(IGenericCommandAsync<Travel> TravelGenericServices, IMapper mapper)
        {
            this.TravelGenericServices = TravelGenericServices;
            _mapper = mapper;
        }

       
        // POST api/<TravelsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]  TravelModel _travelModel)
         
        {
            var TravelEntity = _mapper.Map<Travel>(_travelModel);
            var newTravelResult = await TravelGenericServices.AddAsync(TravelEntity);

            if (newTravelResult is null)
            {
                 
                    return BadRequest();
                    //   return StatusCode(StatusCodes.Status400BadRequest); //, new Error { code = "BlasaCar_EXISTING_ACCOUNT", message = "wrong Register :this user exists in the database ! " });
                }

            return Ok(newTravelResult);

        }

       
    }
}
