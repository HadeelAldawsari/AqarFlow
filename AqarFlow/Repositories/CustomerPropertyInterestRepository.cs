using AqarFlow.Data;
using AqarFlow.DTOs;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class CustomerPropertyInterestRepository
        : ICustomerPropertyInterestRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<CustomerPropertyInterest> _dbSet;

        public CustomerPropertyInterestRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<CustomerPropertyInterest>();
        }

        // Get only the data needed for the interests list
        public List<CustomerPropertyInterestDto> GetAll()
        {
            return _dbSet
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => new CustomerPropertyInterestDto
                {
                    Id = i.Id,

                    CustomerName = i.Customer != null
                        ? i.Customer.Name
                        : "Unknown",

                    PropertyTitle = i.Property != null
                        ? i.Property.Title
                        : "Unknown",

                    Status = i.Status,
                    Notes = i.Notes,
                    CreatedAt = i.CreatedAt
                })
                .ToList();
        }

        // Get interest with related customer and property
        public CustomerPropertyInterest? GetById(int id)
        {
            return _dbSet
                .Include(i => i.Customer)
                .Include(i => i.Property)
                .FirstOrDefault(i => i.Id == id);
        }

        // Add new interest
        public void Add(CustomerPropertyInterest interest)
        {
            _dbSet.Add(interest);
        }

        // Update existing interest
        public void Update(CustomerPropertyInterest interest)
        {
            _dbSet.Update(interest);
        }

        // Delete interest
        public void Delete(CustomerPropertyInterest interest)
        {
            _dbSet.Remove(interest);
        }

        // Save changes
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}