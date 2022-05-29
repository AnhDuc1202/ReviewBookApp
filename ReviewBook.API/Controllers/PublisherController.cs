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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _PublisherService;
        private readonly IUserService _userService;

        public PublisherController(IPublisherService publisherService, IUserService userService)
        {
            _PublisherService = publisherService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_PublisherService.GetAllPublishers());
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpGet("{id}")]
        public ActionResult<Publisher> Get(int id)
        {
            return Ok(_PublisherService.GetPublisherById(id));
        }

        [HttpPost]
        public ActionResult<Publisher> Post([FromBody] CreatePublisherDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_PublisherService.CreatePublisher(value.toAuthorEntity()));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpPut("{id}")]
        public ActionResult<Publisher> Put(int id, [FromBody] UpdateInforPublisherDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_PublisherService.UpdatePublisher(value.toPublisherEntity(id)));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_PublisherService.DeletePublisher(id));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
    }
}