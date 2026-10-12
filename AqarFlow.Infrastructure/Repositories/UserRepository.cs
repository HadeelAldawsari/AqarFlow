
using AqarFlow.Application.DTOs;
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<User>();
        }

        // GET ALL
        public List<UserDto> GetAll()
        {
            return _dbSet
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,

                    RoleName = u.RoleUsers
                        .Select(ru =>
                            ru.Role != null
                                ? ru.Role.Name
                                : null)
                        .FirstOrDefault(),

                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt
                })
                .ToList();
        }

        // GET BY ID
        public User? GetById(int id)
        {
            return _dbSet
                .Include(u => u.RoleUsers)
                    .ThenInclude(ru => ru.Role)
                .FirstOrDefault(u => u.Id == id);
        }

        // GET BY EMAIL
        public User? GetByEmail(string email)
        {
            return _dbSet
                .Include(u => u.RoleUsers)
                    .ThenInclude(ru => ru.Role)
                .FirstOrDefault(u => u.Email == email);
        }

        // ADD
        public void Add(User user)
        {
            _dbSet.Add(user);
        }

        // UPDATE
        public void Update(User user)
        {
            _dbSet.Update(user);
        }

        // DELETE
        public void Delete(User user)
        {
            _dbSet.Remove(user);
        }

        // SAVE
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
