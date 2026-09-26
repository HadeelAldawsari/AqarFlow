using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Role> _dbSet;

        public RoleRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Role>();
        }

        public List<Role> GetAll()
        {
            return _dbSet
                .OrderBy(r => r.Name)
                .ToList();
        }

        public Role? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(Role role)
        {
            _dbSet.Add(role);
        }

        public void Update(Role role)
        {
            _dbSet.Update(role);
        }

        public void Delete(Role role)
        {
            _dbSet.Remove(role);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}