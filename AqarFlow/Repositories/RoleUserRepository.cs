using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class RoleUserRepository : IRoleUserRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<RoleUser> _dbSet;

        public RoleUserRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<RoleUser>();
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