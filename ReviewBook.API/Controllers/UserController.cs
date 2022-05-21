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
        public ActionResult<Account> Register([FromBody] UserRegisterDTOs value)
        {
            return Ok(this.userService.UserRegisterAccount(value));
        }

        [HttpPut("EditAccount/{id}")]
        public ActionResult Edit([FromBody] UserAccountUpdateDtOs value, int id){
            var result = this.userService.EditAccount(value.toAccountEntity());
            if(result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("Login")]
        
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = this.userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("ReadReviews/{id}")]
        public IActionResult ReadReviews(int id){
            return Ok(this.userService.readReview(id));
        }

        [HttpPost("WriteReview")]
        public IActionResult WriteReview([FromBody] UserWriteReviewDTOs review){
            return Ok(this.userService.writeReview(review));
        }

        [HttpPost("Search")]
        public IActionResult Search([FromBody] String bookOrAuthor){
            return Ok(this.userService.searchForBookOrAuthor(bookOrAuthor));
        }

        [HttpPost("ProposeTag")]
        public IActionResult ProposeTag([FromBody] UserPropose_TagDTOs value){
            return Ok(this.userService.proposeTag(value));
        }

        [HttpPost("ProposeBook")]
        public IActionResult ProposeBook([FromBody] UserProposeBookDTOs value){
            return Ok(this.userService.proposeBook(value.toEntity()));
        }
    }
}