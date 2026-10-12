
using AqarFlow.Models;
using AqarFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AqarFlow.Controllers
{
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

        // DASHBOARD
        public IActionResult Index()
        {
            ViewBag.TotalCustomers =
                _customerRepository.Count();

            ViewBag.TotalProperties =
                _propertyRepository.Count();

            ViewBag.ActiveDeals =
                _dealRepository.CountActive();

            ViewBag.FollowUpsToday =
                _followUpRepository.CountForDate(DateTime.Today);

            ViewBag.RecentCustomers =
                _customerRepository.GetRecent(5);

            ViewBag.NewCustomers =
                _customerRepository.CountByStatus("New");

            ViewBag.InterestedCustomers =
                _customerRepository.CountByStatus("Interested");

            ViewBag.InProgressCustomers =
                _customerRepository.CountByStatus("In Progress");

            ViewBag.ClosedCustomers =
                _customerRepository.CountByStatus("Closed");

            ViewBag.UpcomingFollowUps =
                _followUpRepository.GetUpcoming(5);

            int currentYear = DateTime.Now.Year;

            ViewBag.MonthLabels = new[]
            {
                "Jan", "Feb", "Mar", "Apr",
                "May", "Jun", "Jul", "Aug",
                "Sep", "Oct", "Nov", "Dec"
            };

            ViewBag.MonthValues =
                _customerRepository.GetMonthlyCounts(currentYear);

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
                RequestId =
                    Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
