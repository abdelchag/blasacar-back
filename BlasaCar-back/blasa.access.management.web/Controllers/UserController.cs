using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using blasa.access.management.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using blasa.access.management.Core.Domain.Entities;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using AutoMapper;
using blasa.access.management.web.Dto;

namespace blasa.access.management.web.Controllers
{
    [Route("api/access-management/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IResponse<User> _response;
        private readonly IEmailSender _EmailSender;
        private readonly IToken _Token;
        private readonly IMapper _mapper;


        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, 
            IResponse<User> response, IEmailSender EmailSender  , IToken Token, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this._response = response;
            this._EmailSender = EmailSender;
            this._Token = Token;
            _mapper = mapper;
        }


        /// <summary>
        /// Login a BlasaCar account.
        /// </summary>   
        /// <returns>login a BlasaCar account</returns>
        /// <response code="200">Success : return the BlasaCar account</response>
        [ProducesResponseType(typeof(Response<UserDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]

        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
               var _Token = await GetToken(user);
               
                return Ok(new Response<UserDto> { Succeeded = true, Data = _mapper.Map<UserDto>(user), token = _Token.Token, expiration = _Token.Expiration, Message = null });

                //return Ok(new
                //{
                //    token = _Token.Token,
                //    expiration = _Token.Expiration,
                //    user=User,
                //});
            }
            //return Unauthorized();
            return StatusCode(StatusCodes.Status401Unauthorized, new Error { code = StatusCodes.Status401Unauthorized, message = "BlasaCar_login_failed" });

        }

        private async Task<IToken> GetToken(User user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            _Token.Token = new JwtSecurityTokenHandler().WriteToken(token);
            _Token.Expiration = token.ValidTo;


            return _Token;

        }



        /// <summary>
        /// Create a new BlasaCar account from Facebook.
        /// </summary>   
        /// <returns>A newly created BlasaCar account</returns>
        /// <response code="200">Success : returns the newly created BlasaCar account</response>
 
        [ProducesResponseType(typeof(Response<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null &&( userExists.Provider is null))
                return StatusCode(StatusCodes.Status400BadRequest, new Error { code = StatusCodes.Status400BadRequest, message = "BLASA_EXISTING_ACCOUNT" });

            User user = new User()
            {
                Email = model.Email,
                UserName = string.Concat("", model.Email),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName =     model.firstName,
                LastName  =     model.lastName,
                Telephone =     model.Telephone,
                Address   =     model.Address,
                BirthDate =     model.BirthDate,
                Gender       =  model.Gender,
                

            };
            var result = await userManager.CreateAsync(user, model.Password);

            
            if (!result.Succeeded)
                 
                return StatusCode(StatusCodes.Status400BadRequest, new Error { code = StatusCodes.Status400BadRequest,  message = "BLASA_User_creation_failed" });

            //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });




            

             
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _EmailSender.SendEmailAsync(model.Email, "Confirm your account",
               $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
            //await signInManager.SignInAsync(user, isPersistent: false);
            //logger.LogInformation(3, "User created a new account with password.");
            //return RedirectToLocal(returnUrl);

            var _Token = await GetToken(user);
            
            

            //_response.Status = "Success";
            //_response.Message = "User created successfully!";
            //_response.Data = user;
            //_response.token = _Token.Token;
            //_response.expiration = _Token.Expiration;
            //return Ok(_response);
          
            return Ok(new Response<UserDto> { Succeeded = true, Data = _mapper.Map<UserDto>(user), token = _Token.Token, expiration = _Token.Expiration, Message = null });

        }

    }
}
