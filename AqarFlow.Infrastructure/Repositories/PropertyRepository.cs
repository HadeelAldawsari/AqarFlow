
using AqarFlow.Application.DTOs;
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Property> _dbSet;

        public PropertyRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Property>();
        }

        public List<PropertyDto> GetAll()
        {
            return _dbSet
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PropertyDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    PropertyType = p.PropertyType,
                    City = p.City,
                    Area = p.Area,
                    Price = p.Price,
                    Status = p.Status,
                    ImagePath = p.ImagePath
                })
                .ToList();
        }

        public Property? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(Property property)
        {
            _dbSet.Add(property);
        }

        public void Update(Property property)
        {
            _dbSet.Update(property);
        }

        public void Delete(Property property)
        {
            _dbSet.Remove(property);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public int Count()
        {
            return _dbSet.Count();
        }
    }
}
