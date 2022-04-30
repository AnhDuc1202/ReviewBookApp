
using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public Role CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public bool DeleteRole(int IdRole)
        {
            var currentRole = GetRoleById(IdRole);
            if (currentRole == null) return false;
            _context.Roles.Remove(currentRole);
            _context.SaveChanges();
            return true;
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.Include(r => r.Accounts).ToList();
        }

        public Role? GetRoleById(int IdRole)
        {
            return _context.Roles.Include(r => r.Accounts).FirstOrDefault(r => r.ID == IdRole);
        }

        public Role? UpdateRole(Role role)
        {
            var currentRole = GetRoleById(role.ID);
            if (currentRole == null) return null;
            currentRole.NameRole = role.NameRole;
            _context.Roles.Update(currentRole);
            _context.SaveChanges();
            return role;
        }
    }
}