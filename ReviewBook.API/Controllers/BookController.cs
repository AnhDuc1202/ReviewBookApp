using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;

        public BookController(IBookService bookService)
        {
            _BookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_BookService.GetAllBooks());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            return Ok(_BookService.GetBookById(id));
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] CreateBookDTOs value)
        {
            var newBook = _BookService.CreateBook(value.toBookEntity());
            for (int i = 0; i < value.List_ID_Tags.Count(); i++)
            {
                Book_Tag k = new Book_Tag();
                k.ID_Book = newBook.Id;
                k.ID_Tag = value.List_ID_Tags[i];
                _BookService.CreateBookTag(k);
            }
            return Ok(_BookService.GetBookById(newBook.Id));
        }

        [HttpPut("{id}")]
        public ActionResult<Book> Put(int id, [FromBody] UpdateBookDTOs value)
        {
            var book = _BookService.UpdateBook(value.toBookEntity(id));


            var k = _BookService.GetAllBookTagsByIdBook(book.Id);
            for (int i = 0; i < value.List_ID_Tags_Remove.Count(); i++)
            {
                foreach (Book_Tag k1 in k)
                {
                    if (k1.ID_Tag == value.List_ID_Tags_Remove[i])
                        _BookService.DeleteBookTag(k1.ID);
                }
            }
            for (int i = 0; i < value.List_ID_Tags_Add.Count(); i++)
            {
                Book_Tag m = new Book_Tag();
                m.ID_Book = book.Id;
                m.ID_Tag = value.List_ID_Tags_Add[i];
                _BookService.CreateBookTag(m);
            }
            return Ok(_BookService.GetBookById(book.Id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_BookService.DeleteBook(id));
        }
        [HttpGet("Propose")]
        public ActionResult<IEnumerable<Propose>> GetAllProposes()
        {
            return Ok(_BookService.GetAllProposes());
        }

        [HttpGet("Propose/{id}")]
        public ActionResult<Propose> GetPropose(int id)
        {
            return Ok(_BookService.GetProposeById(id));
        }

        [HttpPost("Propose")]
        public ActionResult<Propose> CreatePropose([FromBody] CreateProposeDTOs value)
        {
            var newPropose = _BookService.CreatePropose(value.toProposeEntity());
            for (int i = 0; i < value.List_ID_Tags.Count(); i++)
            {
                Propose_Tag k = new Propose_Tag();
                k.ID_Propose = newPropose.ID;
                k.ID_Tag = value.List_ID_Tags[i];
                _BookService.CreateProposeTag(k);
            }
            return Ok(_BookService.GetProposeById(newPropose.ID));
        }

        [HttpPut("Propose/{id}")]
        public ActionResult<Propose> UpdatePropose(int id, [FromBody] UpadateProposeDTOs value)
        {
            var p = _BookService.UpdatePropose(value.toProposeEntity(id));


            var k = _BookService.GetAllProposeTagsByIdProppose(p.ID);
            for (int i = 0; i < value.List_ID_Tags_Remove.Count(); i++)
            {
                foreach (Propose_Tag k1 in k)
                {
                    if (k1.ID_Tag == value.List_ID_Tags_Remove[i])
                        _BookService.DeleteProposeTag(k1.ID);
                }
            }
            for (int i = 0; i < value.List_ID_Tags_Add.Count(); i++)
            {
                Propose_Tag m = new Propose_Tag();
                m.ID_Propose = p.ID;
                m.ID_Tag = value.List_ID_Tags_Add[i];
                _BookService.CreateProposeTag(m);
            }
            return Ok(_BookService.GetProposeById(p.ID));
        }

        [HttpDelete("Propose/{id}")]
        public ActionResult DeletePropose(int id)
        {
            return Ok(_BookService.DeletePropose(id));
        }
    }
}