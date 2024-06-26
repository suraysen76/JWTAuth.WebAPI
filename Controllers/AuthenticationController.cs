﻿using JWTAuth.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace JWTAuth.WebApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DatabaseContext _context;

        public AuthenticationController(IConfiguration config, DatabaseContext context)
        {
            _configuration = config;
            _context = context;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Post(UserModel _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                        new Claim(ClaimTypes.GivenName, user.DisplayName),
                        new Claim(ClaimTypes.NameIdentifier, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                    var claimsModel = new ClaimsModel();
                    claimsModel.Token = tokenStr;
                    claimsModel.Claims = new List<ClaimModel>();

                    foreach (var cl in token.Claims)
                    {
                        var claimModel = new ClaimModel() { Type = cl.Type, Value = cl.Value };                        
                        claimsModel.Claims.Add(claimModel);
                    }
                    return Ok(claimsModel);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserModel> GetUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
