using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _TagService;

        public TagController(ITagService tagService)
        {
            _TagService = tagService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> Get()
        {
            return Ok(_TagService.GetAllTags());
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> Get(int id)
        {
            return Ok(_TagService.GetTagById(id));
        }

        [HttpPost]
        public ActionResult<Tag> Post([FromBody] CreateTagDTOs value)
        {
            return Ok(_TagService.CreateTag(value.toTagEntity()));
        }

        [HttpPut("{id}")]
        public ActionResult<Tag> Put(int id, [FromBody] UpdateTagDTOs value)
        {
            return Ok(_TagService.UpdateTag(value.toTagEntity(id)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_TagService.DeleteTag(id));
        }
    }
}