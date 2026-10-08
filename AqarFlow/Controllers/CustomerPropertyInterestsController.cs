
using AqarFlow.Application.Services.Base;
using AqarFlow.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using InterestModel = AqarFlow.Models.CustomerPropertyInterest;
using DomainInterest = AqarFlow.Domain.Models.CustomerPropertyInterest;

namespace AqarFlow.Controllers
{
    [Authorize]
    public class CustomerPropertyInterestsController : Controller
    {
        private readonly ICustomerPropertyInterestService _interestService;
        private readonly ICustomerService _customerService;
        private readonly IPropertyService _propertyService;

        public CustomerPropertyInterestsController(
            ICustomerPropertyInterestService interestService,
            ICustomerService customerService,
            IPropertyService propertyService)
        {
            _interestService = interestService;
            _customerService = customerService;
            _propertyService = propertyService;
        }

        // =====================================
        // INDEX
        // =====================================

        public IActionResult Index()
        {
            var customers = _customerService
                .GetAllCustomers()
                .ToDictionary(c => c.Id, c => c.Name);

            var properties = _propertyService
                .GetAll()
                .ToDictionary(p => p.Id, p => p.Title);

            var interests = _interestService
                .GetAll()
                .Select(i => new CustomerPropertyInterestDto
                {
                    Id = i.Id,

                    CustomerName = customers.TryGetValue(
                        i.CustomerId, out var customerName)
                        ? customerName
                        : "-",

                    PropertyTitle = properties.TryGetValue(
                        i.PropertyId, out var propertyTitle)
                        ? propertyTitle
                        : "-",

                    Status = i.Status,
                    Notes = i.Notes,
                    CreatedAt = i.CreatedAt
                })
                .ToList();

            return View(interests);
        }

        // =====================================
        // CREATE - GET
        // =====================================

        [HttpGet]
        public IActionResult Create()
        {
            LoadCustomers();
            LoadProperties();

            return View();
        }

        // =====================================
        // CREATE - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterestModel interest)
        {
            if (ModelState.IsValid)
            {
                var domainInterest = ToDomainModel(interest);

                domainInterest.CreatedAt = DateTime.Now;

                _interestService.Add(domainInterest);

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(interest.CustomerId);
            LoadProperties(interest.PropertyId);

            return View(interest);
        }

        // =====================================
        // EDIT - GET
        // =====================================

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var interest = _interestService.GetById(id.Value);

            if (interest == null)
                return NotFound();

            LoadCustomers(interest.CustomerId);
            LoadProperties(interest.PropertyId);

            return View(ToViewModel(interest));
        }

        // =====================================
        // EDIT - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InterestModel interest)
        {
            if (id != interest.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingInterest = _interestService.GetById(id);

                if (existingInterest == null)
                    return NotFound();

                existingInterest.CustomerId = interest.CustomerId;
                existingInterest.PropertyId = interest.PropertyId;
                existingInterest.Status = interest.Status;
                existingInterest.Notes = interest.Notes;

                _interestService.Update(existingInterest);

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(interest.CustomerId);
            LoadProperties(interest.PropertyId);

            return View(interest);
        }

        // =====================================
        // DELETE - GET
        // =====================================

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var interest = _interestService.GetById(id.Value);

            if (interest == null)
                return NotFound();

            return View(ToViewModel(interest));
        }

        // =====================================
        // DELETE - POST
        // =====================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var interest = _interestService.GetById(id);

            if (interest != null)
            {
                _interestService.Delete(interest);
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // LOAD CUSTOMERS
        // =====================================

        private void LoadCustomers(int? selectedCustomerId = null)
        {
            var customers = _customerService
                .GetAllCustomers()
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Customers = new SelectList(
                customers,
                "Id",
                "Name",
                selectedCustomerId
            );
        }

        // =====================================
        // LOAD PROPERTIES
        // =====================================

        private void LoadProperties(int? selectedPropertyId = null)
        {
            var properties = _propertyService
                .GetAll()
                .OrderBy(p => p.Title)
                .ToList();

            ViewBag.Properties = new SelectList(
                properties,
                "Id",
                "Title",
                selectedPropertyId
            );
        }

        // =====================================
        // MVC MODEL TO DOMAIN MODEL
        // =====================================

        private static DomainInterest ToDomainModel(InterestModel interest)
        {
            return new DomainInterest
            {
                Id = interest.Id,
                CustomerId = interest.CustomerId,
                PropertyId = interest.PropertyId,
                Status = interest.Status,
                Notes = interest.Notes,
                CreatedAt = interest.CreatedAt
            };
        }

        // =====================================
        // DOMAIN MODEL TO MVC MODEL
        // =====================================

        private static InterestModel ToViewModel(DomainInterest interest)
        {
            return new InterestModel
            {
                Id = interest.Id,
                CustomerId = interest.CustomerId,
                PropertyId = interest.PropertyId,
                Status = interest.Status,
                Notes = interest.Notes,
                CreatedAt = interest.CreatedAt
            };
        }
    }
}
