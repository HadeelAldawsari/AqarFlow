using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AqarFlow.Controllers
{
    // Only logged-in users can access Deals
    [Authorize]
    public class DealsController : Controller
    {
        private readonly IDealRepository _dealRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPropertyRepository _propertyRepository;

        public DealsController(
            IDealRepository dealRepository,
            ICustomerRepository customerRepository,
            IPropertyRepository propertyRepository)
        {
            _dealRepository = dealRepository;
            _customerRepository = customerRepository;
            _propertyRepository = propertyRepository;
        }


        // =========================
        // INDEX
        // Get only the data needed for the deals list
        // =========================
        public IActionResult Index()
        {
            var deals = _dealRepository.GetAll();

            return View(deals);
        }


        // =========================
        // CREATE - GET
        // Display create deal form
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            LoadCustomers();
            LoadProperties();

            return View();
        }


        // =========================
        // CREATE - POST
        // Save new deal
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.CreatedAt = DateTime.Now;

                _dealRepository.Add(deal);
                _dealRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(deal.CustomerId);
            LoadProperties(deal.PropertyId);

            return View(deal);
        }


        // =========================
        // EDIT - GET
        // Display edit deal form
        // =========================
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = _dealRepository.GetById(id.Value);

            if (deal == null)
            {
                return NotFound();
            }

            LoadCustomers(deal.CustomerId);
            LoadProperties(deal.PropertyId);

            return View(deal);
        }


        // =========================
        // EDIT - POST
        // Update existing deal
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Deal deal)
        {
            if (id != deal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingDeal =
                    _dealRepository.GetById(id);

                if (existingDeal == null)
                {
                    return NotFound();
                }

                existingDeal.CustomerId = deal.CustomerId;
                existingDeal.PropertyId = deal.PropertyId;
                existingDeal.DealType = deal.DealType;
                existingDeal.DealValue = deal.DealValue;
                existingDeal.Commission = deal.Commission;
                existingDeal.Status = deal.Status;
                existingDeal.DealDate = deal.DealDate;
                existingDeal.Notes = deal.Notes;

                _dealRepository.Update(existingDeal);
                _dealRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(deal.CustomerId);
            LoadProperties(deal.PropertyId);

            return View(deal);
        }


        // =========================
        // DELETE - GET
        // Display delete confirmation
        // =========================
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = _dealRepository.GetById(id.Value);

            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }


        // =========================
        // DELETE - POST
        // Delete deal
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deal = _dealRepository.GetById(id);

            if (deal != null)
            {
                _dealRepository.Delete(deal);
                _dealRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // LOAD CUSTOMERS
        // Prepare customer dropdown
        // =========================
        private void LoadCustomers(
            int? selectedCustomerId = null)
        {
            var customers = _customerRepository
                .GetAll()
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Customers = new SelectList(
                customers,
                "Id",
                "Name",
                selectedCustomerId
            );
        }


        // =========================
        // LOAD PROPERTIES
        // Prepare property dropdown
        // =========================
        private void LoadProperties(
            int? selectedPropertyId = null)
        {
            var properties = _propertyRepository
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
    }
}