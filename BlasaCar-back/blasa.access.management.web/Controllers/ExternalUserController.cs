using AutoMapper;
using blasa.access.management.Core.Domain.Entities;
using blasa.access.management.web.Dto;
using blasa.access.management.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace blasa.access.management.web.Controllers
{
    [Route("api/access-management/external-user")]
    [ApiController]
    public class ExternalUserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IResponse<User> _response;
        private readonly IEmailSender _EmailSender;
        private readonly IToken _Token;
        private readonly IMapper _mapper;


        public ExternalUserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,
            IResponse<User> response, IEmailSender EmailSender, IToken Token, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _response = response;
            _EmailSender = EmailSender;
            _Token = Token;
            _mapper = mapper;
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    var user = await userManager.FindByEmailAsync(model.Email);
        //    if (user != null   )
        //    {
        //        var _Token = await GetToken(user);
        //        return Ok(new
        //        {
        //            token = _Token.Token,
        //            expiration = _Token.Expiration,
        //            user = User,
        //        });
        //    }
        //    return Unauthorized();
        //}

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

        [HttpPost]
        [Route("register")]
       
        public async Task<IActionResult> Register([FromBody] RegisterModelExternal model)
        {

            var _Provider = new Provider();
            if (model.Provider.ToLower() == "facebook")
            {
                _Provider.Id = 1;
                _Provider.Label = "Facebook";
            }
            else if (model.Provider.ToLower() == "gmail")
            {
                _Provider.Id = 2;
                _Provider.Label = "Gmail";
            }

            var userExists = await userManager.FindByNameAsync(string.Concat(_Provider.Label, "", model.Email)) ;
            string ProviderExist = userExists?.Provider?.Label;
            if ((userExists != null && ProviderExist == model.Provider))
            {
                var _Token1 = await GetToken(userExists);
                //return Ok(_Token1);
                return Ok(new Response<UserDto> { Data = _mapper.Map<UserDto>(userExists), token = _Token1.Token });
                //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            
            User user = new User()
            {
                Email = model.Email,
                UserName = string.Concat(_Provider.Label, "", model.Email),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.firstName,
                LastName = model.lastName,
                Telephone = model.Telephone,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                //Provider= _Provider

            };
            var result = await userManager.CreateAsync(user);


            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response <User>{  Message = "User creation failed! Please check user details and try again." });

            // Send an email with this link
            //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _EmailSender.SendEmailAsync(model.Email, "Confirm your account",
            //   $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
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

            return Ok(new Response<UserDto> { Data = _mapper.Map<UserDto>(user), token = _Token.Token });
        }
        [HttpPost ]
         [Route("login")]
        public async Task<IActionResult> Register([FromBody] LoginModelExternal model)
        {

            var _Provider = new Provider();
            if (model.Provider.ToLower() == "facebook")
            {
                _Provider.Id = 1;
                _Provider.Label = "Facebook";
            }
            //else if (model.Provider.ToLower() == "gmail")
            //{
            //    _Provider.Id = 2;
            //    _Provider.Label = "Gmail";
            //}

            var userExists = await userManager.FindByNameAsync(string.Concat(_Provider.Label, "", model.Email));
            string ProviderExist = userExists?.Provider?.Label;
            if ((userExists != null && ProviderExist == model.Provider))
            {
                var _Token1 = await GetToken(userExists);
                //return Ok(_Token1);
                return Ok(new Response<UserDto> { Data = _mapper.Map<UserDto>(userExists), token = _Token1.Token });
                //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }


            User user = new User()
            {
                Email = model.Email,
                UserName = string.Concat(_Provider.Label, "", model.Email),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.firstName,
                LastName = model.lastName,
                Telephone = model.Telephone,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                //Provider= _Provider

            };
            var result = await userManager.CreateAsync(user);


            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User> { Message = "User creation failed! Please check user details and try again." });

            // Send an email with this link
            //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _EmailSender.SendEmailAsync(model.Email, "Confirm your account",
            //   $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
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

            return Ok(new Response<UserDto> { Data = _mapper.Map<UserDto>(user), token = _Token.Token });
        }
    }
}
