using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ReviewBook.API.Models;
using Microsoft.Net.Http.Headers;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // [HttpGet("ReviewsbyIdBook/{id}")]
        // public IActionResult ReadReviews(int id)
        // {
        //     return Ok(this._userService.readReviewbyIdBook(id));
        // }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // [HttpPost("Review")]
        // public IActionResult WriteReview([FromBody] UserWriteReviewDTOs review)
        // {
        //     var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

        //     var acc = _userService.jwtTokenToAccount(_bearer_token);

        //     if (acc.ID == review.ID_Acc)
        //         return Ok(this._userService.writeReview(review));
        //     return BadRequest();
        // }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Search")]
        public IActionResult Search([FromBody] String bookOrAuthor)
        {
            return Ok(this._userService.searchForBookOrAuthor(bookOrAuthor));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthenticateRequest model)
        {
            return Ok(_userService.Authenticate(model));
        }

    }
}