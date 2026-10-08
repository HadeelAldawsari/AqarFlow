
using AqarFlow.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebCustomer = AqarFlow.Models.Customer;
using DomainCustomer = AqarFlow.Domain.Models.Customer;
using WebCustomerDto = AqarFlow.DTOs.CustomerDto;

namespace AqarFlow.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // =====================================
        // INDEX
        // =====================================

        public IActionResult Index()
        {
            var customers = _customerService.GetAll()
                .Select(c => new WebCustomerDto
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

            return View(customers);
        }

        // =====================================
        // CREATE - GET
        // =====================================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // =====================================
        // CREATE - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WebCustomer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var domainCustomer = ToDomainCustomer(customer);

            _customerService.CreateCustomer(domainCustomer);

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // EDIT - GET
        // =====================================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(ToWebCustomer(customer));
        }

        // =====================================
        // EDIT - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WebCustomer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var updated = _customerService.UpdateCustomer(
                ToDomainCustomer(customer)
            );

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // DELETE - GET
        // =====================================

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(ToWebCustomer(customer));
        }

        // =====================================
        // DELETE - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted = _customerService.DeleteCustomer(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // DOMAIN TO MVC MODEL
        // =====================================

        private static WebCustomer ToWebCustomer(DomainCustomer customer)
        {
            return new WebCustomer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Purpose = customer.Purpose,
                PropertyType = customer.PropertyType,
                PreferredCity = customer.PreferredCity,
                PreferredArea = customer.PreferredArea,
                MinBudget = customer.MinBudget,
                MaxBudget = customer.MaxBudget,
                Bedrooms = customer.Bedrooms,
                Status = customer.Status,
                Source = customer.Source,
                Notes = customer.Notes,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        // =====================================
        // MVC MODEL TO DOMAIN
        // =====================================

        private static DomainCustomer ToDomainCustomer(WebCustomer customer)
        {
            return new DomainCustomer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Purpose = customer.Purpose,
                PropertyType = customer.PropertyType,
                PreferredCity = customer.PreferredCity,
                PreferredArea = customer.PreferredArea,
                MinBudget = customer.MinBudget,
                MaxBudget = customer.MaxBudget,
                Bedrooms = customer.Bedrooms,
                Status = customer.Status,
                Source = customer.Source,
                Notes = customer.Notes,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }
    }
}
