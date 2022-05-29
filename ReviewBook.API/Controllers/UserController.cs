using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ReviewBook.API.Models;
using ReviewBook.API.Data.Entities;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UserFollow")]
        public IActionResult Follow([FromBody] UserFollowDTOs value)
        {
            // var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            // var acc = _userService.jwtTokenToAccount(_bearer_token);
            // if (acc.ID == value.IdFollowing)
            // {
                bool status = _userService.Follow(value);
                return status ? Ok("Follow success!") : BadRequest("Unfollow success!");
            // }
            // return BadRequest("Không đủ quyền");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddMyBook")]
        public ActionResult<MyBooks> AddMyBook([FromBody] UserAddMyBookDTOs value){
            if(_userService.AddMyBook(value) == null)
                return BadRequest("This book is already added!");
            return Ok(_userService.AddMyBook(value));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("EditBookStatus")]
        public ActionResult<MyBooks> EditBookStatus([FromBody] UserEditBookStatusDTOs value){
            if(_userService.EditBookStatus(value) == null)
                return BadRequest("This book is not added to your book yet!");
            return Ok(_userService.EditBookStatus(value));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllMyBooks")]
        public ActionResult<IEnumerable<MyBooks>> GetMyBooks()
        {
            if(_userService.GetAllMyBooks() == null)
                return Ok("There are nothing in your book!");
            return Ok(_userService.GetAllMyBooks());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetMyBookById/{id}")]
        public IActionResult GetMyBookById(int id)
        {
            return Ok(_userService.GetMyBookById(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("DeleteMyBookById/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            if(_userService.DeleteBookById(id) == false)
                return BadRequest("There are no book with this id");
            return Ok("Delete successfully!");
        }

    }
}