using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


namespace blasa.travel.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
         


            private readonly IGenericCommandAsync<Book> _BookGenericServices;
            private readonly IBookCommandAsync _UserCommandAsyncServices;
            private readonly IMapper _mapper;
            private readonly ILogger<BookController> _logger;
            public BookController(ILogger<BookController> logger, IGenericCommandAsync<Book> BookGenericServices, IBookCommandAsync UserCommandAsyncServices, IMapper mapper)
            {
                _BookGenericServices = BookGenericServices;
                _UserCommandAsyncServices = UserCommandAsyncServices;
                _mapper = mapper;
                _logger = logger;
            }


            // POST api/<TravelsController>
            [ValidateModel]
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] BookDTO _bookDto)

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

                var BookEntity = _mapper.Map<Book>(_bookDto);
                BookEntity.Userid = userId;
                var newBookResult = await _BookGenericServices.AddAsync(BookEntity);
                if (newBookResult is null)
                {
                    _logger.LogError("Un problème au niveau de la création du Book est survenu ");
                    throw new BadRequestException(ErrorConstants.BLASACARTravelfailedCreationBook);
                }

                return Ok(newBookResult);


            }


            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAsync(int id)
            {


                var BookResult = await _BookGenericServices.DeleteAsync(id);

                if (BookResult is null)
                {

                    throw new NotFoundException(ErrorConstants.BLASACARTravel_BookNotFoundException);
                }

                return Ok(_mapper.Map<BookDTO>(BookResult));
            }


            [HttpGet]
            public async Task<IActionResult> GetAllAsync([FromQuery] bool onlyUser)


            {
                var listBookResult = await Task.FromResult<IReadOnlyList<Book>>(Array.Empty<Book>());

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
                    listBookResult = await _UserCommandAsyncServices.GetBookByUserIdAsync(userId);
                }
                else
                {
                    // All
                    listBookResult = await _BookGenericServices.GetAllAsync();
                }

                if (listBookResult is null)
                {
                    listBookResult = await Task.FromResult<IReadOnlyList<Book>>(Array.Empty<Book>());
                }
                return Ok(_mapper.Map<IReadOnlyList<BookDTO>>(listBookResult));
            }

            [HttpGet]
            [Route("search")]
            public async Task<IActionResult> SearchBook([FromQuery] FiltreTravelDTO _FiltreBookDTO)
            {
                var BookEntity = _mapper.Map<Book>(_FiltreBookDTO);

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
                var listTravelResult = await _UserCommandAsyncServices.GetBookByFiltre(BookEntity);

                if (listTravelResult is null)
                {
                    listTravelResult = await Task.FromResult<IReadOnlyList<Book>>(Array.Empty<Book>());
                }
                return Ok(_mapper.Map<IReadOnlyList<BookDTO>>(listTravelResult));
            }


            [HttpGet("{id}")]
            public async Task<IActionResult> GetByIdAsync(int id)
            {
                var BookResult = await _BookGenericServices.GetByIdAsync(id);
                if (BookResult is null)
                {
                    throw new NotFoundException(ErrorConstants.BLASACARTravel_BookNotFoundException);
                }

                return Ok(_mapper.Map<BookDTO>(BookResult));
            }

           
            [HttpPut]
            public async Task<IActionResult> UpdateAsync([FromBody] BookDTO _bookDto)
            {
                var BookEntity = _mapper.Map<Book>(_bookDto);

                string userId = null;
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    userId = identity.FindFirst("userId").Value;

                }
                if (userId == null) throw new NotAuthorizException(ErrorConstants.BLASACARUnauthorized);

                BookEntity.Userid = userId;

                var newBookResult = await _BookGenericServices.UpdateAsync(BookEntity);

                if (newBookResult is null)
                {
                    _logger.LogError("Le Travel ID : " + (string.IsNullOrEmpty(BookEntity.Id.ToString()) ? "Null" : BookEntity.Id.ToString()) + ", n'existe pas dans la base de données");
                    throw new NotFoundException(ErrorConstants.BLASACARTravel_BookNotFoundException);
                }

                return Ok(_mapper.Map<BookDTO>(newBookResult));


            


        }
    }
}
