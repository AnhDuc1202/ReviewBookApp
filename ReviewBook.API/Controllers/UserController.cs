using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ReviewBook.API.Models;
using ReviewBook.API.Data.Entities;
using Microsoft.Net.Http.Headers;
using System.Net;

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
        [HttpPost("MyBook")]
        public ActionResult<MyBooks> AddMyBook([FromBody] UserAddMyBookDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            var kq = _userService.AddMyBook(value.toEntitiesMyBooks(acc.ID));
            if (kq == null)
                return Problem("Thêm sách vào mybook thất bại",
                statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(kq);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("MyBook")]
        public ActionResult<MyBooks> EditBookStatus([FromBody] UserEditBookStatusDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            var kq = _userService.EditBookStatus(value.toEntitiesMyBooks(acc.ID));
            if (kq == null)
                return Problem("Cập nhật mybook thất bại",
                statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(kq);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("MyBook")]
        public ActionResult<IEnumerable<MyBooks>> GetMyBooks()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            return Ok(_userService.GetAllMyBooksByIdAcc(acc.ID));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("MyBookByIdBook/{id}")]
        public IActionResult GetMyBookById(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            MyBooks myBooks = new MyBooks();
            myBooks.ID_Acc = acc.ID;
            myBooks.ID_Book = id;
            return Ok(_userService.GetMyBookByIdBook(myBooks));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("MyBookByIdBook/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            MyBooks myBooks = new MyBooks();
            myBooks.ID_Acc = acc.ID;
            myBooks.ID_Book = id;
            if (_userService.DeleteBookById(myBooks) == false)
                return Problem("Xóa sách khỏi mybook thất bại",
                statusCode: (int)HttpStatusCode.BadRequest);
            return Ok();
        }

    }
}