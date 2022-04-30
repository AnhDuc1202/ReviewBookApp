using Microsoft.AspNetCore.Mvc;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.Services;

namespace ReviewBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            return Ok(_roleService.GetAllRoles());
        }

        [HttpGet("{id}")]
        public ActionResult<Role> Get(int id)
        {
            return Ok(_roleService.GetRoleById(id));
        }

        [HttpPost]
        public ActionResult<Role> Post([FromBody] RoleDTOs value)
        {
            return Ok(_roleService.CreateRole(new Role(value.RoleName)));
        }

        [HttpPut("{id}")]
        public ActionResult<Role> Put(int id, [FromBody] RoleDTOs value)
        {
            Role role = new Role(value.RoleName);
            role.ID = id;
            var kq = _roleService.UpdateRole(role);
            if (kq != null) return Ok(kq);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var kq = _roleService.DeleteRole(id);
            if (kq) return Ok();
            return BadRequest();
        }
    }
}