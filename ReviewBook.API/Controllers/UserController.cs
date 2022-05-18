using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.DTOs;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.Services;
using ReviewBook.API.Models;
using Microsoft.AspNetCore.Authorization;

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
            return Ok(this.userService.UserRegisterAccount(value));
        }

        [HttpPut("Edit/{id}")]
        public ActionResult Edit([FromForm] UserAccountUpdateDtOs value, int id){
            var result = this.userService.EditAccount(value.toAccountEntity());
            if(result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("Login")]
        
        public IActionResult Authenticate([FromForm] AuthenticateRequest model)
        {
            var response = this.userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("ReadReviews")]
        public IActionResult ReadReviews([FromForm] int idBook){
            return Ok(this.userService.readReview(idBook));
        }

        [HttpPost("WriteReview")]
        public IActionResult WriteReview([FromForm] UserWriteReviewDTOs review){
            return Ok(this.userService.writeReview(review));
        }

        [HttpPost("Search")]
        public IActionResult Search([FromForm] String bookOrAuthor){
            return Ok(this.userService.searchForBookOrAuthor(bookOrAuthor));
        }
    }
}