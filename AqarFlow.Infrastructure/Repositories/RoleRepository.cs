
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Role> _dbSet;

        public RoleRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Role>();
        }

        // GET ALL
        public List<Role> GetAll()
        {
            return _dbSet
                .OrderBy(r => r.Name)
                .ToList();
        }

        // GET BY ID
        public Role? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // ADD
        public void Add(Role role)
        {
            _dbSet.Add(role);
        }

        // UPDATE
        public void Update(Role role)
        {
            _dbSet.Update(role);
        }

        // DELETE
        public void Delete(Role role)
        {
            _dbSet.Remove(role);
        }

        // SAVE
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
