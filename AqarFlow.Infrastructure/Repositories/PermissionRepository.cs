
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Permission> _dbSet;

        public PermissionRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Permission>();
        }

        // GET ALL
        public List<Permission> GetAll()
        {
            return _dbSet.ToList();
        }

        // GET BY ID
        public Permission? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // ADD
        public void Add(Permission permission)
        {
            _dbSet.Add(permission);
        }

        // UPDATE
        public void Update(Permission permission)
        {
            _dbSet.Update(permission);
        }

        // DELETE
        public void Delete(Permission permission)
        {
            _dbSet.Remove(permission);
        }

        // SAVE
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
