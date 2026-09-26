using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AqarFlow.Controllers
{
    // Only logged-in users can access property matching
    [Authorize]
    public class CustomerPropertyInterestsController : Controller
    {
        private readonly ICustomerPropertyInterestRepository _interestRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPropertyRepository _propertyRepository;

        public CustomerPropertyInterestsController(
            ICustomerPropertyInterestRepository interestRepository,
            ICustomerRepository customerRepository,
            IPropertyRepository propertyRepository)
        {
            _interestRepository = interestRepository;
            _customerRepository = customerRepository;
            _propertyRepository = propertyRepository;
        }


        // =========================
        // INDEX
        // Get only the data needed for the matching list
        // =========================
        public IActionResult Index()
        {
            var interests = _interestRepository.GetAll();

            return View(interests);
        }


        // =========================
        // CREATE - GET
        // Display create matching form
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
        // Save new customer-property match
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerPropertyInterest interest)
        {
            if (ModelState.IsValid)
            {
                interest.CreatedAt = DateTime.Now;

                _interestRepository.Add(interest);
                _interestRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(interest.CustomerId);
            LoadProperties(interest.PropertyId);

            return View(interest);
        }


        // =========================
        // EDIT - GET
        // Display edit matching form
        // =========================
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest =
                _interestRepository.GetById(id.Value);

            if (interest == null)
            {
                return NotFound();
            }

            LoadCustomers(interest.CustomerId);
            LoadProperties(interest.PropertyId);

            return View(interest);
        }


        // =========================
        // EDIT - POST
        // Update customer-property match
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            CustomerPropertyInterest interest)
        {
            if (id != interest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingInterest =
                    _interestRepository.GetById(id);

                if (existingInterest == null)
                {
                    return NotFound();
                }

                existingInterest.CustomerId =
                    interest.CustomerId;

                existingInterest.PropertyId =
                    interest.PropertyId;

                existingInterest.Status =
                    interest.Status;

                existingInterest.Notes =
                    interest.Notes;

                _interestRepository.Update(existingInterest);
                _interestRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(interest.CustomerId);
            LoadProperties(interest.PropertyId);

            return View(interest);
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

            var interest =
                _interestRepository.GetById(id.Value);

            if (interest == null)
            {
                return NotFound();
            }

            return View(interest);
        }


        // =========================
        // DELETE - POST
        // Delete customer-property match
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var interest =
                _interestRepository.GetById(id);

            if (interest != null)
            {
                _interestRepository.Delete(interest);
                _interestRepository.Save();
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