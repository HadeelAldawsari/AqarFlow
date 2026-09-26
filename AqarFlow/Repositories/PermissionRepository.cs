using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Permission> _dbSet;

        public PermissionRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Permission>();
        }

        // Get all permissions
        public List<Permission> GetAll()
        {
            return _dbSet.ToList();
        }

        // Get permission by ID
        public Permission? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // Add new permission
        public void Add(Permission permission)
        {
            _dbSet.Add(permission);
        }

        // Update permission
        public void Update(Permission permission)
        {
            _dbSet.Update(permission);
        }

        // Delete permission
        public void Delete(Permission permission)
        {
            _dbSet.Remove(permission);
        }

        // Save changes
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}