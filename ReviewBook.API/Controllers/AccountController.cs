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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("hasPassword")]
        public ActionResult<IEnumerable<Account>> Get()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1) return Ok(_AccountService.GetAllAccounts());

            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("noPassword")]
        public ActionResult<IEnumerable<Account>> GetP()
        {
            return Ok(_AccountService.GetAllAccountsNoPassword());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("hasPassword/{id}")]
        public ActionResult<Account> Get(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == id || acc.ID_Role == 1)
                return Ok(_AccountService.GetAccountById(id));

            return BadRequest();
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
            if (acc == null) return BadRequest();
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
                if (newacc == null) return BadRequest();
                return Ok(newacc);
            }
            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("password/{id}")]
        public ActionResult<Account> Put(int id, [FromBody] UpdatePasswordAccountDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID == id || acc.ID_Role == 1)
            {
                var kq = _AccountService.UpdatePasswordAccount(value.toAccountEntity(id));
                if (kq == null) return BadRequest();
                return Ok(kq);
            }
            return BadRequest();

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
                if (kq == null) return BadRequest();
                return Ok(kq);
            }

            return BadRequest();
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
                if (!kq) return BadRequest();
                return Ok(kq);
            }
            return BadRequest();

        }
    }
}