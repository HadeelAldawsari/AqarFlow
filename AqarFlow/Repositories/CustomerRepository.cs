using AqarFlow.Data;
using AqarFlow.DTOs;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Customer>();
        }


        // =========================
        // GET ALL
        // =========================
        public List<CustomerDto> GetAll()
        {
            return _dbSet
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Purpose = c.Purpose,
                    PropertyType = c.PropertyType,
                    PreferredArea = c.PreferredArea,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt
                })
                .ToList();
        }


        // =========================
        // GET BY ID
        // =========================
        public Customer? GetById(int id)
        {
            return _dbSet.Find(id);
        }


        // =========================
        // ADD
        // =========================
        public void Add(Customer customer)
        {
            _dbSet.Add(customer);
        }


        // =========================
        // UPDATE
        // =========================
        public void Update(Customer customer)
        {
            _dbSet.Update(customer);
        }


        // =========================
        // DELETE
        // =========================
        public void Delete(Customer customer)
        {
            _dbSet.Remove(customer);
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


        // =========================
        // DASHBOARD - STATUS COUNT
        // =========================
        public int CountByStatus(string status)
        {
            return _dbSet.Count(c =>
                c.Status == status);
        }


        // =========================
        // DASHBOARD - RECENT
        // =========================
        public List<Customer> GetRecent(int count)
        {
            return _dbSet
                .OrderByDescending(c => c.CreatedAt)
                .Take(count)
                .ToList();
        }


        // =========================
        // DASHBOARD - MONTHLY COUNTS
        // =========================
        public int[] GetMonthlyCounts(int year)
        {
            var monthlyCustomers = _dbSet
                .Where(c => c.CreatedAt.Year == year)
                .GroupBy(c => c.CreatedAt.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToList();

            return Enumerable
                .Range(1, 12)
                .Select(month =>
                    monthlyCustomers
                        .FirstOrDefault(
                            x => x.Month == month
                        )?.Count ?? 0)
                .ToArray();
        }
    }
}