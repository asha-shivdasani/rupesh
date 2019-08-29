using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityAPI.Infrastructure;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IdentityDbContext identityDbContext;
        private IConfiguration configuration;

        public IdentityController(IdentityDbContext identityDbContext, IConfiguration configuration )
        {
            this.identityDbContext = identityDbContext;
            this.configuration = configuration;
        }
        //POST /api/identity/auth/register
        [HttpPost("auth/register",Name ="Register")]        
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<dynamic>> RegisterAsync(UserInfo userInfo)
        {
            TryValidateModel(userInfo);
            if (ModelState.IsValid)
            {
                var result = await identityDbContext.Users.AddAsync(userInfo);
                await identityDbContext.SaveChangesAsync();
                return Created("", new {
                    Id = result.Entity.Id,
                    FirstName = result.Entity.FirstName,
                    LastName = result.Entity.LastName,
                    Email = result.Entity.Email
                });
                //return Created($"/api/events/{result.Entity.Id}", result.Entity);
                //return CreatedAtAction(nameof(GetEventById), new { id = result.Entity.Id }, result.Entity);
                //return CreatedAtRoute("GetEventById", new { id = result.Entity.Id }, result.Entity);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        //POST /api/identity/auth/register
        [HttpPost("auth/token", Name = "GetToken")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<dynamic> GetToken(LoginModel loginModel)
        {
            TryValidateModel(loginModel);
            if (ModelState.IsValid)
            {
                var user = identityDbContext.Users.SingleOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
                if (user == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return GenerateToken(user);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        private object GenerateToken(UserInfo user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims:claims,
                expires:DateTime.Now.AddMinutes(30),
                signingCredentials:credentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new {
                user.FirstName,
                user.LastName,
                token = tokenString
            };









                
            
        }
    }
}