using AqarFlow.Data;
using AqarFlow.DTOs;
using AqarFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Repositories
{
    public class FollowUpRepository : IFollowUpRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<FollowUp> _dbSet;

        public FollowUpRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<FollowUp>();
        }


        // =========================
        // GET ALL
        // =========================
        public List<FollowUpDto> GetAll()
        {
            return _dbSet
                .OrderByDescending(f => f.ContactDate)
                .Select(f => new FollowUpDto
                {
                    Id = f.Id,

                    CustomerName = f.Customer != null
                        ? f.Customer.Name
                        : "Unknown",

                    ContactDate = f.ContactDate,
                    ContactMethod = f.ContactMethod,
                    Result = f.Result,
                    NextFollowUpDate = f.NextFollowUpDate,
                    Status = f.Status
                })
                .ToList();
        }


        // =========================
        // GET BY ID
        // =========================
        public FollowUp? GetById(int id)
        {
            return _dbSet
                .Include(f => f.Customer)
                .FirstOrDefault(f => f.Id == id);
        }


        // =========================
        // ADD
        // =========================
        public void Add(FollowUp followUp)
        {
            _dbSet.Add(followUp);
        }


        // =========================
        // UPDATE
        // =========================
        public void Update(FollowUp followUp)
        {
            _dbSet.Update(followUp);
        }


        // =========================
        // DELETE
        // =========================
        public void Delete(FollowUp followUp)
        {
            _dbSet.Remove(followUp);
        }


        // =========================
        // SAVE
        // =========================
        public void Save()
        {
            _db.SaveChanges();
        }


        // =========================
        // DASHBOARD - TODAY
        // =========================
        public int CountForDate(DateTime date)
        {
            var startDate = date.Date;
            var nextDay = startDate.AddDays(1);

            return _dbSet.Count(f =>
                f.NextFollowUpDate >= startDate &&
                f.NextFollowUpDate < nextDay);
        }


        // =========================
        // DASHBOARD - UPCOMING
        // =========================
        public List<FollowUp> GetUpcoming(int count)
        {
            return _dbSet
                .Include(f => f.Customer)
                .Where(f =>
                    f.NextFollowUpDate.HasValue &&
                    f.NextFollowUpDate.Value >= DateTime.Now &&
                    f.Status != "Completed")
                .OrderBy(f => f.NextFollowUpDate)
                .Take(count)
                .ToList();
        }
    }
}