
using AqarFlow.Application.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using DealModel = AqarFlow.Models.Deal;
using DomainDeal = AqarFlow.Domain.Models.Deal;
using DealDto = AqarFlow.DTOs.DealDto;

namespace AqarFlow.Controllers
{
    [Authorize]
    public class DealsController : Controller
    {
        private readonly IDealService _dealService;
        private readonly ICustomerService _customerService;
        private readonly IPropertyService _propertyService;

        public DealsController(
            IDealService dealService,
            ICustomerService customerService,
            IPropertyService propertyService)
        {
            _dealService = dealService;
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

            var deals = _dealService
                .GetAll()
                .Select(d => new DealDto
                {
                    Id = d.Id,

                    CustomerName = customers.TryGetValue(
                        d.CustomerId, out var customerName)
                        ? customerName
                        : "-",

                    PropertyTitle = properties.TryGetValue(
                        d.PropertyId, out var propertyTitle)
                        ? propertyTitle
                        : "-",

                    DealType = d.DealType,
                    DealValue = d.DealValue,
                    Commission = d.Commission,
                    DealDate = d.DealDate,
                    Status = d.Status
                })
                .ToList();

            return View(deals);
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
        public IActionResult Create(DealModel deal)
        {
            if (ModelState.IsValid)
            {
                var domainDeal = ToDomainModel(deal);

                domainDeal.CreatedAt = DateTime.Now;

                _dealService.Add(domainDeal);

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(deal.CustomerId);
            LoadProperties(deal.PropertyId);

            return View(deal);
        }

        // =====================================
        // EDIT - GET
        // =====================================

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var deal = _dealService.GetById(id.Value);

            if (deal == null)
                return NotFound();

            LoadCustomers(deal.CustomerId);
            LoadProperties(deal.PropertyId);

            return View(ToViewModel(deal));
        }

        // =====================================
        // EDIT - POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DealModel deal)
        {
            if (id != deal.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingDeal = _dealService.GetById(id);

                if (existingDeal == null)
                    return NotFound();

                existingDeal.CustomerId = deal.CustomerId;
                existingDeal.PropertyId = deal.PropertyId;
                existingDeal.DealType = deal.DealType;
                existingDeal.DealValue = deal.DealValue;
                existingDeal.Commission = deal.Commission;
                existingDeal.Status = deal.Status;
                existingDeal.DealDate = deal.DealDate;
                existingDeal.Notes = deal.Notes;

                _dealService.Update(existingDeal);

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(deal.CustomerId);
            LoadProperties(deal.PropertyId);

            return View(deal);
        }

        // =====================================
        // DELETE - GET
        // =====================================

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var deal = _dealService.GetById(id.Value);

            if (deal == null)
                return NotFound();

            return View(ToViewModel(deal));
        }

        // =====================================
        // DELETE - POST
        // =====================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deal = _dealService.GetById(id);

            if (deal != null)
            {
                _dealService.Delete(deal);
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

        private static DomainDeal ToDomainModel(DealModel deal)
        {
            return new DomainDeal
            {
                Id = deal.Id,
                CustomerId = deal.CustomerId,
                PropertyId = deal.PropertyId,
                DealType = deal.DealType,
                DealValue = deal.DealValue,
                Commission = deal.Commission,
                Status = deal.Status,
                DealDate = deal.DealDate,
                Notes = deal.Notes,
                CreatedAt = deal.CreatedAt
            };
        }

        // =====================================
        // DOMAIN MODEL TO MVC MODEL
        // =====================================

        private static DealModel ToViewModel(DomainDeal deal)
        {
            return new DealModel
            {
                Id = deal.Id,
                CustomerId = deal.CustomerId,
                PropertyId = deal.PropertyId,
                DealType = deal.DealType,
                DealValue = deal.DealValue,
                Commission = deal.Commission,
                Status = deal.Status,
                DealDate = deal.DealDate,
                Notes = deal.Notes,
                CreatedAt = deal.CreatedAt
            };
        }
    }
}
