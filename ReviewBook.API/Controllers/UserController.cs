using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.DTOs;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.Services;
using ReviewBook.API.Models;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Register")]
        public ActionResult<Account> Register([FromForm] UserRegisterDTOs value)
        {
            return Ok(this.userService.CreateAccount(value.toAccountEntity()));
        }

        [HttpPut("Edit/{id}")]
        public ActionResult Edit([FromForm] UserAccountUpdateDtOs value, int id){
            var result = this.userService.EditAccount(value.toAccountEntity());
            if(result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromForm] AuthenticateRequest model)
        {
            var response = this.userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}