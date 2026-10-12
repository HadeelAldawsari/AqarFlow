
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class RoleUserRepository : IRoleUserRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<RoleUser> _dbSet;

        public RoleUserRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<RoleUser>();
        }

        public List<RoleUser> GetByUserId(int userId)
        {
            return _dbSet
                .Where(ru => ru.UserId == userId)
                .ToList();
        }

        public void Add(RoleUser roleUser)
        {
            _dbSet.Add(roleUser);
        }

        public void DeleteByUserId(int userId)
        {
            var roleUsers = _dbSet
                .Where(ru => ru.UserId == userId)
                .ToList();

            _dbSet.RemoveRange(roleUsers);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

