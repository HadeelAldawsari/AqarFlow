using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class PermissionRoleRepository : IPermissionRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<PermissionRole> _dbSet;

        public PermissionRoleRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<PermissionRole>();
        }

        // Get permission IDs assigned to a role
        public List<int> GetPermissionIdsByRoleId(int roleId)
        {
            return _dbSet
                .Where(pr => pr.RoleId == roleId)
                .Select(pr => pr.PermissionId)
                .ToList();
        }

        // Add permission to role
        public void Add(PermissionRole permissionRole)
        {
            _dbSet.Add(permissionRole);
        }

        // Delete all permissions assigned to a role
        public void DeleteByRoleId(int roleId)
        {
            var permissionRoles = _dbSet
                .Where(pr => pr.RoleId == roleId)
                .ToList();

            _dbSet.RemoveRange(permissionRoles);
        }

        // Save changes
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}