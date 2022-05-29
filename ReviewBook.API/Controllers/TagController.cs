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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _TagService;
        private readonly IUserService _userService;

        public TagController(ITagService tagService, IUserService userService)
        {
            _TagService = tagService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> Get()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_TagService.GetAllTags());
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> Get(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_TagService.GetTagById(id));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult<Tag> Post([FromBody] CreateTagDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_TagService.CreateTag(value.toTagEntity()));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpPut("{id}")]
        public ActionResult<Tag> Put(int id, [FromBody] UpdateTagDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_TagService.UpdateTag(value.toTagEntity(id)));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_TagService.DeleteTag(id));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
    }
}