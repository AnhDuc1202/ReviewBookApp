using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IRoleService
    {
        public List<Role> GetAllRoles();
        public Role? GetRoleById(int IdRole);
        public Role CreateRole(Role role);
        public Role? UpdateRole(Role role);
        public bool DeleteRole(int IdRole);
    }
}