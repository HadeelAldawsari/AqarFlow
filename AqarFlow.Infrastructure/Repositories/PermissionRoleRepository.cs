
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class PermissionRoleRepository : IPermissionRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<PermissionRole> _dbSet;

        public PermissionRoleRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<PermissionRole>();
        }

        // GET PERMISSION IDS BY ROLE
        public List<int> GetPermissionIdsByRoleId(int roleId)
        {
            return _dbSet
                .Where(pr => pr.RoleId == roleId)
                .Select(pr => pr.PermissionId)
                .ToList();
        }

        // ADD
        public void Add(PermissionRole permissionRole)
        {
            _dbSet.Add(permissionRole);
        }

        // DELETE BY ROLE ID
        public void DeleteByRoleId(int roleId)
        {
            var permissionRoles = _dbSet
                .Where(pr => pr.RoleId == roleId)
                .ToList();

            _dbSet.RemoveRange(permissionRoles);
        }

        // SAVE
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
