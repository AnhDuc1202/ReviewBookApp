
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IAccountService accountService)
        {
            _AccountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(_AccountService.GetAllAccounts());
        }

        [HttpGet("{id}")]
        public ActionResult<Account> Get(int id)
        {
            return Ok(_AccountService.GetAccountById(id));
        }

        [HttpPost]
        public ActionResult<Account> Post([FromBody] CreateAccountDTOs value)
        {
            return Ok(_AccountService.CreateAccount(value.toAccountEntity()));
        }

        [HttpPut("/password/{id}")]
        public ActionResult<Account> Put(int id, [FromBody] UpdatePasswordAccountDTOs value)
        {
            var kq = _AccountService.UpdatePasswordAccount(value.toAccountEntity(id));
            if (kq == null) return BadRequest();
            return Ok(kq);
        }
        [HttpPut("/information/{id}")]
        public ActionResult<Account> Put(int id, [FromBody] UpdateInforAccountDTOs value)
        {
            var kq = _AccountService.UpdateInforAccount(value.toAccountEntity(id));
            if (kq == null) return BadRequest();
            return Ok(kq);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var kq = _AccountService.DeleteAccount(id);
            if (!kq) return BadRequest();
            return Ok(kq);
        }
    }
}