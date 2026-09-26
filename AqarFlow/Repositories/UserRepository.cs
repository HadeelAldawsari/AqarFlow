using AqarFlow.Data;
using AqarFlow.DTOs;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<User>();
        }

        // Get only the data needed for the users list
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

        // Get user with roles
        public User? GetById(int id)
        {
            return _dbSet
                .Include(u => u.RoleUsers)
                    .ThenInclude(ru => ru.Role)
                .FirstOrDefault(u => u.Id == id);
        }

        // Get user by email
        public User? GetByEmail(string email)
        {
            return _dbSet
                .Include(u => u.RoleUsers)
                    .ThenInclude(ru => ru.Role)
                .FirstOrDefault(u => u.Email == email);
        }

        // Add new user
        public void Add(User user)
        {
            _dbSet.Add(user);
        }

        // Update existing user
        public void Update(User user)
        {
            _dbSet.Update(user);
        }

        // Delete user
        public void Delete(User user)
        {
            _dbSet.Remove(user);
        }

        // Save changes
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}