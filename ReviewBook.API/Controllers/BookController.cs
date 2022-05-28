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
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;
        private readonly IRateBookService _rateBookService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public BookController(IBookService bookService,
        IRateBookService rateBookService, IUserService userService
        , IReviewService reviewService)
        {
            _BookService = bookService;
            _rateBookService = rateBookService;
            _userService = userService;
            _reviewService = reviewService;
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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("CreateOrUpdateRate")]
        public ActionResult<RateBook> PostRate([FromBody] RateBookDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == value.ID_Acc)
            {
                var b = _BookService.GetBookById(value.ID_Book);
                if (b == null) return BadRequest("sách không còn tồn tại");
                var r = _rateBookService.CreateOrUpdateRateBook(value.toEntitiesRateBook());
                if (r == null) return BadRequest("đánh giá số sao thất bại");
                return Ok(r);
            }
            return BadRequest("Không đủ quyền");
        }

        [HttpGet("RateAvg/{id}")]
        public ActionResult<IEnumerable<BookAvgDTOs>> GetAllRateBookbyID(int id)
        {
            BookAvgDTOs b = new BookAvgDTOs(id, _rateBookService.GetAllRateBookByIdBook(id));
            return Ok(b);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Review")]
        public ActionResult<Review> PostReview([FromBody] ReviewDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == value.ID_Acc)
            {
                var b = _BookService.GetBookById(value.ID_Book);
                if (b == null) return BadRequest("sách không còn tồn tại");
                var r = _reviewService.CreateReview(value.toEntitiesReviewBook());
                if (r == null) return BadRequest("bình luận thất bại");
                return Ok(r);
            }
            return BadRequest("Không đủ quyền");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Review/{id}")]
        public ActionResult<Review> PutReview(int id, [FromBody] ReviewDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == value.ID_Acc)
            {
                var r = _reviewService.UpdateReview(value.toEntitiesReviewBook(id));
                if (r == null) return BadRequest("cập nhật bình luận thất bại");
                return Ok(r);
            }
            return BadRequest("Không đủ quyền");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Review/{id}")]
        public ActionResult DeleteReview(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == _reviewService.GetReviewById(id).ID_Acc)
            {
                return Ok(_reviewService.DeleteReview(id));
            }
            return BadRequest("Không đủ quyền hoặc id review không tồn tại");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Review/Reply")]
        public ActionResult<ReviewChildren> PostReviewReply([FromBody] ReviewChildrenDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == value.ID_Acc)
            {
                var b = _reviewService.GetReviewById(value.Id_parent);
                if (b == null) return BadRequest("đánh giá bạn trả lời không tồn tại");
                var r = _reviewService.CreateReviewChildren(value.toEntitiesReviewChildren());
                if (r == null) return BadRequest("trả lời bình luận thất bại");
                return Ok(r);
            }
            return BadRequest("Không đủ quyền");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Review/Reply/{id}")]
        public ActionResult<ReviewChildren> PutReviewReply(int id, [FromBody] ReviewChildrenDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == value.ID_Acc)
            {
                var r = _reviewService.UpdateReviewChildren(value.toEntitiesReviewChildren(id));
                if (r == null) return BadRequest("cập nhật bình luận thất bại");
                return Ok(r);
            }
            return BadRequest("Không đủ quyền");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Review/Reply/{id}")]
        public ActionResult DeleteReviewReply(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == _reviewService.GetReviewChildrenById(id).ID_Acc)
            {
                return Ok(_reviewService.DeleteReviewChildren(id));
            }
            return BadRequest("Không đủ quyền hoặc id review không tồn tại");
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ActionResult<Book> PostBook([FromBody] CreateBookDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var newBook = _BookService.CreateBook(value.toBookEntity());
                for (int i = 0; i < value.List_ID_Tags.Count(); i++)
                {
                    Book_Tag k = new Book_Tag();
                    k.ID_Book = newBook.Id;
                    k.ID_Tag = value.List_ID_Tags[i];
                    var a = _BookService.CreateBookTag(k);
                }
                return Ok(_BookService.GetBookById(newBook.Id));
            }
            return BadRequest("Không đủ quyền");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public ActionResult<Book> Put(int id, [FromBody] UpdateBookDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
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
            return BadRequest("Không đủ quyền");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_BookService.DeleteBook(id));
            return BadRequest("Không đủ quyền");
        }
        [HttpGet("Propose")]
        public ActionResult<IEnumerable<Propose>> GetAllProposes()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_BookService.GetAllProposes());
            return BadRequest("Không đủ quyền");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Propose/{id}")]
        public ActionResult<Propose> GetPropose(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_BookService.GetProposeById(id));
            return BadRequest("Không đủ quyền");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Propose/ProposeByIdUser/{id}")]
        public ActionResult<Propose> ProposeByIdUser(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 2 && acc.ID == id)
                return Ok(_BookService.GetProposeByIdUser(id));
            return BadRequest("Chỉ User mới xem đề xuất sách của mình");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Propose")]
        public ActionResult<Propose> CreatePropose([FromBody] CreateProposeDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 2)
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
            return BadRequest("Chỉ User mới đề xuất sách");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Propose/{id}")]
        public ActionResult<Propose> UpdatePropose(int id, [FromBody] UpadateProposeDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
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
            return BadRequest("Không đủ quyền");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Propose/{id}")]
        public ActionResult DeletePropose(int id)
        {
            return Ok(_BookService.DeletePropose(id));
        }
    }
}