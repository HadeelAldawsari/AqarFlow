using AqarFlow.Data;
using AqarFlow.DTOs;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Property> _dbSet;

        public PropertyRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Property>();
        }


        // =========================
        // GET ALL
        // =========================
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


        // =========================
        // GET BY ID
        // =========================
        public Property? GetById(int id)
        {
            return _dbSet.Find(id);
        }


        // =========================
        // ADD
        // =========================
        public void Add(Property property)
        {
            _dbSet.Add(property);
        }


        // =========================
        // UPDATE
        // =========================
        public void Update(Property property)
        {
            _dbSet.Update(property);
        }


        // =========================
        // DELETE
        // =========================
        public void Delete(Property property)
        {
            _dbSet.Remove(property);
        }


        // =========================
        // SAVE
        // =========================
        public void Save()
        {
            _db.SaveChanges();
        }


        // =========================
        // DASHBOARD - TOTAL
        // =========================
        public int Count()
        {
            return _dbSet.Count();
        }
    }
}