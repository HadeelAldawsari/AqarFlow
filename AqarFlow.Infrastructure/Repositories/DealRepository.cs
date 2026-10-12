
using AqarFlow.Application.DTOs;
using AqarFlow.Application.Interfaces;
using AqarFlow.Domain.Models;
using AqarFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Infrastructure.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Deal> _dbSet;

        public DealRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<Deal>();
        }

        // GET ALL
        public List<DealDto> GetAll()
        {
            return _dbSet
                .OrderByDescending(d => d.CreatedAt)
                .Select(d => new DealDto
                {
                    Id = d.Id,

                    CustomerName = d.Customer != null
                        ? d.Customer.Name
                        : "Unknown",

                    PropertyTitle = d.Property != null
                        ? d.Property.Title
                        : "Unknown",

                    DealType = d.DealType,
                    DealValue = d.DealValue,
                    Commission = d.Commission,
                    DealDate = d.DealDate,
                    Status = d.Status
                })
                .ToList();
        }

        // GET BY ID
        public Deal? GetById(int id)
        {
            return _dbSet
                .Include(d => d.Customer)
                .Include(d => d.Property)
                .FirstOrDefault(d => d.Id == id);
        }

        // ADD
        public void Add(Deal deal)
        {
            _dbSet.Add(deal);
        }

        // UPDATE
        public void Update(Deal deal)
        {
            _dbSet.Update(deal);
        }

        // DELETE
        public void Delete(Deal deal)
        {
            _dbSet.Remove(deal);
        }

        // SAVE
        public void Save()
        {
            _db.SaveChanges();
        }

        // DASHBOARD - ACTIVE DEALS
        public int CountActive()
        {
            return _dbSet.Count(d =>
                d.Status != "Completed" &&
                d.Status != "Cancelled");
        }
    }
}
