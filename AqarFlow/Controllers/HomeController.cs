using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AqarFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            // Total customers
            ViewBag.TotalCustomers = _db.Customers.Count();

            // Total properties
            ViewBag.TotalProperties = _db.Properties.Count();

            // Active deals
            ViewBag.ActiveDeals = _db.Deals
                .Count(d => d.Status != "Completed"
                         && d.Status != "Cancelled");

            // Today's date range
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            // Follow-ups scheduled for today
            ViewBag.FollowUpsToday = _db.FollowUps
                .Count(f =>
                    f.NextFollowUpDate >= today &&
                    f.NextFollowUpDate < tomorrow);

            // Latest 5 customers
            ViewBag.RecentCustomers = _db.Customers
                .OrderByDescending(c => c.CreatedAt)
                .Take(5)
                .ToList();

            // Customers by status
            ViewBag.NewCustomers = _db.Customers
                .Count(c => c.Status == "New");

            ViewBag.InterestedCustomers = _db.Customers
                .Count(c => c.Status == "Interested");

            ViewBag.InProgressCustomers = _db.Customers
                .Count(c => c.Status == "In Progress");

            ViewBag.ClosedCustomers = _db.Customers
                .Count(c => c.Status == "Closed");

            // Upcoming follow-ups
            ViewBag.UpcomingFollowUps = _db.FollowUps
                .Include(f => f.Customer)
                .Where(f =>
                    f.NextFollowUpDate.HasValue &&
                    f.NextFollowUpDate.Value >= DateTime.Now &&
                    f.Status != "Completed")
                .OrderBy(f => f.NextFollowUpDate)
                .Take(5)
                .ToList();

            // =========================
            // Leads Over Time Chart
            // =========================

            int currentYear = DateTime.Now.Year;

            var monthlyCustomers = _db.Customers
                .Where(c => c.CreatedAt.Year == currentYear)
                .GroupBy(c => c.CreatedAt.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToList();

            // Month names
            ViewBag.MonthLabels = new[]
            {
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec"
            };

            // Customer count for each month
            ViewBag.MonthValues = Enumerable
                .Range(1, 12)
                .Select(month =>
                    monthlyCustomers
                        .FirstOrDefault(x => x.Month == month)?.Count ?? 0)
                .ToArray();

            ViewBag.ChartYear = currentYear;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}

