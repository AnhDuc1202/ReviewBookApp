using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Models;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginAndRegisterController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IUserService userService;

        public UserLoginAndRegisterController(IConfiguration configuration, DataContext context, IUserService userService)
        {
            _configuration = configuration;
            _context = context;
            this.userService = userService;
        }

        [HttpPost("Register")]
        public ActionResult<Account> Register([FromBody] UserRegisterDTOs value)
        {
            return Ok(this.userService.UserRegisterAccount(value));
        }

        // [HttpPost("Login")]
        
        // public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        // {
        //     var response = this.userService.Authenticate(model);

        //     if (response == null)
        //         return BadRequest(new { message = "Username or password is incorrect" });

        //     return Ok(response);
        // }

        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody]AuthenticateRequest model)
        {
            Account user = _context.Accounts.FirstOrDefault(a => a.UserName == model.UserName);
                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.ID.ToString()),
                        new Claim("Username", user.UserName.ToString()),
                        new Claim("Password", user.Password.ToString()),
                        new Claim("Role", user.ID_Role.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
    }
}