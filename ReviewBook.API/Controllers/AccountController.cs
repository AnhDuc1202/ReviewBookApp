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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _AccountService = accountService;
            _userService = userService;
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // [HttpGet("hasPassword")]
        // public ActionResult<IEnumerable<Account>> Get()
        // {
        //     var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
        //     var acc = _userService.jwtTokenToAccount(_bearer_token);
        //     if (acc.ID_Role == 1) return Ok(_AccountService.GetAllAccounts());

        //     return Problem("Không đủ quyền. Phải là admin",
        //         statusCode: (int)HttpStatusCode.BadRequest);
        // }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("noPassword")]
        public ActionResult<IEnumerable<Account>> GetP()
        {
            return Ok(_AccountService.GetAllAccountsNoPassword());
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // [HttpGet("hasPassword/{id}")]
        // public ActionResult<Account> Get(int id)
        // {
        //     var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
        //     var acc = _userService.jwtTokenToAccount(_bearer_token);
        //     if (acc.ID == id || acc.ID_Role == 1)
        //         return Ok(_AccountService.GetAccountById(id));

        //     return Problem("Không đủ quyền. Phải là admin hoặc tài khoản chính chủ",
        //         statusCode: (int)HttpStatusCode.BadRequest);
        // }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("MyInforHasMyTag")]
        public ActionResult<Account> Get()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            return Ok(_AccountService.GetAccountByIdHasMyTag(acc.ID));

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("noPassword/{id}")]
        public ActionResult<Account> GetP(int id)
        {
            return Ok(_AccountService.GetAccountByIdNoPassword(id));
        }


        [HttpPost]
        public ActionResult<Account> Post([FromBody] CreateAccountDTOs value)
        {
            var acc = _AccountService.CreateAccount(value.toAccountEntity());
            if (acc == null)
                return Problem("Tạo tài khoản thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(acc);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("/api/AccountAdmin")]
        public ActionResult<Account> PostA([FromBody] CreateAccountAdminDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {

                var newacc = _AccountService.CreateAccount(value.toAccountEntity());
                if (newacc == null)
                    return Problem("Tạo tài khoản thất bại",
                        statusCode: (int)HttpStatusCode.BadRequest);
                return Ok(newacc);
            }
            return Problem("Không đủ quyền. Phải là admin",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("password/{id}")]
        public ActionResult<Account> Put(int id, [FromBody] UpdatePasswordAccountDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == id)
            {
                string pass = _AccountService.GetAccountById(id).Password;
                if (pass != value.CurrentPassword)
                    return Problem("Mật khẩu cũ không đúng",
                    statusCode: (int)HttpStatusCode.BadRequest);
                var kq = _AccountService.UpdatePasswordAccount(value.toAccountEntity(id));
                if (kq == null)
                    return Problem("Cập nhật thất bại",
                        statusCode: (int)HttpStatusCode.BadRequest);
                return Ok();
            }
            return Problem("Phải là tài khoản chính chủ",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("passwordAdmin/{id}")]
        public ActionResult<Account> PutPP(int id, [FromBody] UpdatePasswordAccountRoleAdminDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var kq = _AccountService.UpdatePasswordAccount(value.toAccountEntity(id));
                if (kq == null)
                    return Problem("Cập nhật thất bại",
                        statusCode: (int)HttpStatusCode.BadRequest);
                return Ok();
            }
            return Problem("Không đủ quyền. Phải là admin.",
                statusCode: (int)HttpStatusCode.BadRequest);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("information/{id}")]
        public ActionResult<Account> Put(int id, [FromBody] UpdateInforAccountDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == id || acc.ID_Role == 1)
            {
                var kq = _AccountService.UpdateInforAccount(value.toAccountEntity(id));
                if (kq == null) return Problem("cập nhật thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
                return Ok(kq);
            }

            return Problem("Không đủ quyền. Phải là admin hoặc tài khoản chính chủ",
                statusCode: (int)HttpStatusCode.BadRequest);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == id || acc.ID_Role == 1)
            {
                var kq = _AccountService.DeleteAccount(id);
                if (!kq)
                    return Problem("Xóa thất bại",
                        statusCode: (int)HttpStatusCode.BadRequest);
                return Ok();
            }
            return Problem("Không đủ quyền. Phải là admin hoặc tài khoản chính chủ",
            statusCode: (int)HttpStatusCode.BadRequest);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Follow")]
        public ActionResult<Follow> PostA([FromBody] FollowDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            var newfollow = _AccountService.CreateFollow(value.toEntitiesFollow(acc.ID));
            if (newfollow == null)
                return Problem("Follow thất bại do tài khoản không tồn tại hoặc bạn follow chính mình",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok(newfollow);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Follow")]
        public ActionResult DeleteF([FromBody] FollowDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            var kq = _AccountService.DeleteFollow(value.toEntitiesFollow(acc.ID));
            if (!kq)
                return Problem("Hủy Follow thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok();

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("MyTag/{id_tag}")]
        public ActionResult PostT(int id_tag)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            MyTags m = new MyTags();
            m.ID_Acc = acc.ID;
            m.ID_Tag = id_tag;
            var newMyTag = _AccountService.CreateMyTag(m);
            if (newMyTag == null)
                return Problem("thêm my tag thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok();

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("MyTag/{id_tag}")]
        public ActionResult DeleteT(int id_tag)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);

            MyTags m = new MyTags();
            m.ID_Acc = acc.ID;
            m.ID_Tag = id_tag;
            var kq = _AccountService.DeleteMyTag(m);
            if (!kq)
                return Problem("Hủy my tag thất bại",
                    statusCode: (int)HttpStatusCode.BadRequest);
            return Ok();

        }
    }
}