using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RoleController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_roleService.GetAllRoles());
            return BadRequest("Không đủ quyền");
        }

        [HttpGet("{id}")]
        public ActionResult<Role> Get(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_roleService.GetRoleById(id));
            return BadRequest("Không đủ quyền");
        }

        [HttpPost]
        public ActionResult<Role> Post([FromBody] RoleDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
                return Ok(_roleService.CreateRole(new Role(value.RoleName)));
            return BadRequest("Không đủ quyền");
        }

        [HttpPut("{id}")]
        public ActionResult<Role> Put(int id, [FromBody] RoleDTOs value)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                Role role = new Role(value.RoleName);
                role.ID = id;
                var kq = _roleService.UpdateRole(role);
                if (kq != null) return Ok(kq);
                return BadRequest("that bai");
            }
            return BadRequest("Không đủ quyền");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var acc = _userService.jwtTokenToAccount(_bearer_token);
            if (acc.ID_Role == 1)
            {
                var kq = _roleService.DeleteRole(id);
                if (kq) return Ok();
                return BadRequest();
            }
            return BadRequest("Không đủ quyền");
        }
    }
}