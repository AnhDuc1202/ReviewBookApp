using Microsoft.AspNetCore.Mvc;
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

        public AuthorController(IAuthorService authorService)
        {
            _AuthorService = authorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            return Ok(_AuthorService.GetAllAuthors());
        }

        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id)
        {
            return Ok(_AuthorService.GetAuthorById(id));
        }

        [HttpPost]
        public ActionResult<Author> Post([FromBody] CreateAuthorDTOs value)
        {
            return Ok(_AuthorService.CreateAuthor(value.toAuthorEntity()));
        }

        [HttpPut("{id}")]
        public ActionResult<Author> Put(int id, [FromBody] UpdateInforAuthorDTOs value)
        {
            return Ok(_AuthorService.UpdateAuthor(value.toAuthorEntity(id)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_AuthorService.DeleteAuthor(id));
        }
    }
}