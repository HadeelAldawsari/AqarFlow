using AqarFlow.Data;
using AqarFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.Controllers


{
    // Customers Controller:
    // Handles customer-related pages and database operations.


    public class CustomersController : Controller
    {
        private readonly AppDbContext _db;

        // Receives the database context
        public CustomersController(AppDbContext db)
        {
            _db = db;
        }


        // =========================
        // INDEX
        // Displays all customers
        // =========================
        public IActionResult Index()
        {
            var customers = _db.Customers.ToList();

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
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Status = "New";
                customer.CreatedAt = DateTime.Now;

                _db.Customers.Add(customer);
                _db.SaveChanges();

                return RedirectToAction("Index");
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
            var customer = _db.Customers.Find(id);

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
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.UpdatedAt = DateTime.Now;

                _db.Customers.Update(customer);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }


        // =========================
        // DELETE - GET
        // Displays the customer before deletion
        // =========================
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);

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
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customer);
            _db.SaveChanges();

            return RedirectToAction("Index");



        }
    }
}