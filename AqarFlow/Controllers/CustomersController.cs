using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers
{
    // Customers Controller:
    // Handles customer-related pages and operations.

    // Only logged-in users can access Customers
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        // Receives the customer repository
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        // =========================
        // INDEX
        // Displays all customers
        // =========================
        public IActionResult Index()
        {
            var customers = _customerRepository.GetAll();

            return View(customers);
        }


        // =========================
        // CREATE - GET
        // Displays the form to create a new customer
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // =========================
        // CREATE - POST
        // Saves the new customer to the database
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Status = "New";
                customer.CreatedAt = DateTime.Now;

                _customerRepository.Add(customer);
                _customerRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }


        // =========================
        // EDIT - GET
        // Displays the selected customer in the edit form
        // =========================
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // =========================
        // EDIT - POST
        // Saves the updated customer data
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var existingCustomer =
                _customerRepository.GetById(customer.Id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update customer information
            existingCustomer.Name = customer.Name;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.Purpose = customer.Purpose;
            existingCustomer.PropertyType = customer.PropertyType;
            existingCustomer.PreferredCity = customer.PreferredCity;
            existingCustomer.PreferredArea = customer.PreferredArea;
            existingCustomer.MinBudget = customer.MinBudget;
            existingCustomer.MaxBudget = customer.MaxBudget;
            existingCustomer.Bedrooms = customer.Bedrooms;
            existingCustomer.Status = customer.Status;
            existingCustomer.Source = customer.Source;
            existingCustomer.Notes = customer.Notes;
            existingCustomer.UpdatedAt = DateTime.Now;

            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DELETE - GET
        // Displays the customer before deletion
        // =========================
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // =========================
        // DELETE - POST
        // Deletes the customer from the database
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _customerRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(customer);
            _customerRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}