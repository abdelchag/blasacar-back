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

namespace blasa.access.management.web.Controllers
{
    [Route("api/Access-Management/[controller]")]
    [ApiController]
    public class UserEmailController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IResponse _response;
        private readonly IEmailSender _EmailSender;
        private readonly IToken _Token;

        
        public UserEmailController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, 
            IResponse response, IEmailSender EmailSender  , IToken Token )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this._response = response;
            this._EmailSender = EmailSender;
            this._Token = Token;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
               var _Token = await GetToken(user);
               return Ok(new
                {
                    token = _Token.Token,
                    expiration = _Token.Expiration,
                    user=User,
                });
            }
            return Unauthorized();
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

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName =     model.firstName,
                LastName  =     model.lastName,
                Telephone =     model.Telephone,
                Address   =     model.Address,
                BirthDate =     model.BirthDate,
                Sex       =     model.Sex,
                

            };
            var result = await userManager.CreateAsync(user, model.Password);
 

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });




            

             
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


            _response.Status = "Success";
            _response.Message = "User created successfully!";
            _response.ReturnObject = user;
            _response.token = _Token.Token;
            _response.expiration = _Token.Expiration;
            return Ok(_response);
        }

          }
}
