
using AqarFlow.Application.DTOs;
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class CustomerPropertyInterestRepository
        : ICustomerPropertyInterestRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<CustomerPropertyInterest> _dbSet;

        public CustomerPropertyInterestRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<CustomerPropertyInterest>();
        }

        // GET ALL
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

        // GET BY ID
        public CustomerPropertyInterest? GetById(int id)
        {
            return _dbSet
                .Include(i => i.Customer)
                .Include(i => i.Property)
                .FirstOrDefault(i => i.Id == id);
        }

        // ADD
        public void Add(CustomerPropertyInterest interest)
        {
            _dbSet.Add(interest);
        }

        // UPDATE
        public void Update(CustomerPropertyInterest interest)
        {
            _dbSet.Update(interest);
        }

        // DELETE
        public void Delete(CustomerPropertyInterest interest)
        {
            _dbSet.Remove(interest);
        }

        // SAVE
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
