using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AqarFlow.Controllers
{
    // Only logged-in users can access this controller
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IDealRepository _dealRepository;
        private readonly IFollowUpRepository _followUpRepository;


        public HomeController(
            ILogger<HomeController> logger,
            ICustomerRepository customerRepository,
            IPropertyRepository propertyRepository,
            IDealRepository dealRepository,
            IFollowUpRepository followUpRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _propertyRepository = propertyRepository;
            _dealRepository = dealRepository;
            _followUpRepository = followUpRepository;
        }


        // =========================
        // DASHBOARD
        // =========================
        public IActionResult Index()
        {
            // Total customers
            ViewBag.TotalCustomers =
                _customerRepository.Count();


            // Total properties
            ViewBag.TotalProperties =
                _propertyRepository.Count();


            // Active deals
            ViewBag.ActiveDeals =
                _dealRepository.CountActive();


            // Follow-ups scheduled for today
            ViewBag.FollowUpsToday =
                _followUpRepository.CountForDate(
                    DateTime.Today
                );


            // Latest 5 customers
            ViewBag.RecentCustomers =
                _customerRepository.GetRecent(5);


            // Customers by status
            ViewBag.NewCustomers =
                _customerRepository
                    .CountByStatus("New");

            ViewBag.InterestedCustomers =
                _customerRepository
                    .CountByStatus("Interested");

            ViewBag.InProgressCustomers =
                _customerRepository
                    .CountByStatus("In Progress");

            ViewBag.ClosedCustomers =
                _customerRepository
                    .CountByStatus("Closed");


            // Upcoming follow-ups
            ViewBag.UpcomingFollowUps =
                _followUpRepository.GetUpcoming(5);


            // =========================
            // LEADS OVER TIME CHART
            // =========================

            int currentYear = DateTime.Now.Year;


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
            ViewBag.MonthValues =
                _customerRepository
                    .GetMonthlyCounts(currentYear);


            ViewBag.ChartYear = currentYear;


            return View();
        }


        // =========================
        // PRIVACY
        // =========================
        public IActionResult Privacy()
        {
            return View();
        }


        // =========================
        // ERROR
        // =========================
        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId =
                    Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}