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
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly ITagService _tagService;

        public BookController(IBookService bookService, IUserService userService
        , IReviewService reviewService, ITagService tagService)
        {
            _BookService = bookService;
            _userService = userService;
            _reviewService = reviewService;
            _tagService = tagService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks(int page)
        {
            if (page < 1) return Problem("tham số page là số nguyên dương",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(_BookService.GetAllBooksByPage(page));
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            return Ok(_BookService.GetBookById(id));
        }

        [HttpGet("RateAvg/{id}")]
        public ActionResult<IEnumerable<BookAvgDTOs>> GetAllRateBookbyID(int id)
        {
            BookAvgDTOs b = new BookAvgDTOs(id, _BookService.GetRateAvgBookByIdBook(id));
            return Ok(b);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Review")]
        public ActionResult<Review> PostReview([FromBody] CreateReviewDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            var b = _BookService.GetBookById(value.ID_Book);
            if (b == null)
                return Problem("Sách không tồn tại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            var r = _reviewService.CreateReview(value.toEntitiesReviewBook(acc.ID));
            if (r == null)
                return Problem("Bình luận thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(r);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Review/{id}")]
        public ActionResult<Review> PutReview(int id, [FromBody] updateReviewDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            var r = _reviewService.UpdateReview(value.toEntitiesReviewBook(id, acc.ID));
            if (r == null)
                return Problem("cập nhật bình luận thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(r);

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Review/{id}")]
        public ActionResult DeleteReview(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            var kq = _reviewService.DeleteReview(id, acc.ID);
            if (kq)
                return Ok();
            return Problem("Không đủ quyền. Phải tài khoản chính chủ. Hoặc đánh giá không tồn tại",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Review/Reply")]
        public ActionResult<ReviewChildren> PostReviewReply([FromBody] CreateReviewChildrenDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            var b = _reviewService.GetReviewById(value.Id_parent);
            if (b == null)
                return Problem("Đánh giá bạn trả lời không còn tồn tại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            var r = _reviewService.CreateReviewChildren(value.toEntitiesReviewChildren(acc.ID));
            if (r == null)
                return Problem("Trả lời bình luận thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(r);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Review/Reply/{id}")]
        public ActionResult<ReviewChildren> PutReviewReply(int id, [FromBody] UpdateReviewChildrenDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            var r = _reviewService.UpdateReviewChildren(value.toEntitiesReviewChildren(id, acc.ID));
            if (r == null)
                return Problem("Cập nhật thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(r);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Review/Reply/{id}")]
        public ActionResult DeleteReviewReply(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            var kq = _reviewService.DeleteReviewChildren(id, acc.ID);
            if (kq)
                return Ok();
            return Problem("Không đủ quyền. Phải là tài khoản chính chủ. Hoặc đánh giá không tồn tại",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public ActionResult<Book> PostBook([FromBody] CreateBookDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var check = _BookService.CheckName(value.Name);
                if (check != null)
                    return Problem("Cuốn sách khác đã dùng tên này",
                    statusCode: (int)HttpStatusCode.BadRequest);
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
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public ActionResult<Book> Put(int id, [FromBody] UpdateBookDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var check = _BookService.CheckName(value.Name);
                if (check != id && check != null)
                    return Problem("Cuốn sách khác đã dùng tên này",
                    statusCode: (int)HttpStatusCode.BadRequest);
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
                return Ok(_BookService.DeleteBook(id));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
        [HttpGet("Propose")]
        public ActionResult<IEnumerable<ProposeBasicDTOs>> GetAllProposes()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_BookService.GetAllProposes());
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Propose/{id}")]
        public ActionResult<Propose> GetPropose(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_BookService.GetProposeById(id));
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Propose/ProposeByIdUser/{id}")]
        public ActionResult<Propose> ProposeByIdUser(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 2 && acc.ID == id)
                return Ok(_BookService.GetProposeByIdUser(id));
            return Problem("Không đủ quyền. Phải là user và tài khoản chính chủ",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Propose")]
        public ActionResult<Propose> CreatePropose([FromBody] CreateProposeDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            Propose p = value.toProposeEntity(acc.ID);
            if (acc.ID_Role != 2)
                return Problem("Không đủ quyền. Phải là user",
                statusCode: (int)HttpStatusCode.BadRequest);
            if (String.IsNullOrEmpty(p.BookName))
                return Problem("BookName không được để trống",
                    statusCode: (int)HttpStatusCode.BadRequest);
            if (String.IsNullOrEmpty(p.NewAut) && p.ID_Aut != null)
                return Problem("Bạn chưa điền thông tin tác giả",
                    statusCode: (int)HttpStatusCode.BadRequest);
            if (String.IsNullOrEmpty(p.NewPub) && p.ID_Pub != null)
                return Problem("Bạn chưa điền thông tin nhà xuất bản",
                    statusCode: (int)HttpStatusCode.BadRequest);
            var newPropose = _BookService.CreatePropose(p);
            for (int i = 0; i < value.List_ID_Tags.Count(); i++)
            {
                Propose_Tag k = new Propose_Tag();
                k.ID_Propose = newPropose.ID;
                k.ID_Tag = value.List_ID_Tags[i];
                _BookService.CreateProposeTag(k);
            }
            return Ok(_BookService.GetProposeById(newPropose.ID));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddBookFromPropose/{id}")]
        public ActionResult<Book> AddBookFromPropose(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role != 1)
                return Problem("Không đủ quyền. Phải là admin",
                    statusCode: (int)HttpStatusCode.BadRequest);
            var kq = _BookService.AddBookFromPropose(id);
            if (kq == null) return Problem("ID Propose không tồn tại",
                statusCode: (int)HttpStatusCode.BadRequest);
            if (kq == -1) return Problem("Propose không có thông tin tên sách",
                statusCode: (int)HttpStatusCode.BadRequest);
            if (kq == -2) return Problem("Tên sách đã tồn tại",
                statusCode: (int)HttpStatusCode.BadRequest);
            if (kq == -3) return Problem("Propose không có thông tin tác giả",
                statusCode: (int)HttpStatusCode.BadRequest);
            if (kq == -4) return Problem("Propose không có thông tin nxb",
                statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(_BookService.GetBookById(Int32.Parse(kq.ToString())));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Propose/{id}")]
        public ActionResult DeletePropose(int id)
        {
            return Ok(_BookService.DeletePropose(id));
        }
    }
}