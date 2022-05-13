using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _PublisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _PublisherService = publisherService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return Ok(_PublisherService.GetAllPublishers());
        }

        [HttpGet("{id}")]
        public ActionResult<Publisher> Get(int id)
        {
            return Ok(_PublisherService.GetPublisherById(id));
        }

        [HttpPost]
        public ActionResult<Publisher> Post([FromBody] CreatePublisherDTOs value)
        {
            return Ok(_PublisherService.CreatePublisher(value.toAuthorEntity()));
        }

        [HttpPut("{id}")]
        public ActionResult<Publisher> Put(int id, [FromBody] UpdateInforPublisherDTOs value)
        {
            return Ok(_PublisherService.UpdatePublisher(value.toPublisherEntity(id)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_PublisherService.DeletePublisher(id));
        }
    }
}