using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _AuthorService;
        private readonly IUserService _userService;

        public AuthorController(IAuthorService authorService, IUserService userService)
        {
            _AuthorService = authorService;
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            return Ok(_AuthorService.GetAllAuthors());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id)
        {
            return Ok(_AuthorService.GetAuthorById(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ActionResult<Author> Post([FromBody] CreateAuthorDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var check = _AuthorService.CheckStageName(value.Stage_Name);
                if (check != null)
                    return Problem("Đã tồn tại tác giả mang nghệ danh này",
                    statusCode: (int)HttpStatusCode.BadRequest);
                return Ok(_AuthorService.CreateAuthor(value.toAuthorEntity()));
            }
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public ActionResult<Author> Put(int id, [FromBody] UpdateInforAuthorDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var check = _AuthorService.CheckStageName(value.Stage_Name);
                if (check != id && check != null)
                    return Problem("Đã tồn tại tác giả khác mang nghệ danh này",
                    statusCode: (int)HttpStatusCode.BadRequest);
                return Ok(_AuthorService.UpdateAuthor(value.toAuthorEntity(id)));
            }
            return Problem("Không đủ quyền. Phải là admin",
            statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_AuthorService.DeleteAuthor(id));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
    }
}