
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // =====================================
        // GET: api/Customers
        // =====================================

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetAll();

            return Ok(customers);
        }

        // =====================================
        // GET: api/Customers/5
        // =====================================

        [HttpGet("{id:int}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _customerService.GetById(id);

            if (customer == null)
                return NotFound("Customer not found.");

            return Ok(customer);
        }

        // =====================================
        // POST: api/Customers
        // =====================================

        [HttpPost]
        public IActionResult CreateCustomer(
            [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            customer.Id = 0;
            customer.CreatedAt = DateTime.Now;
            customer.UpdatedAt = null;

            _customerService.CreateCustomer(customer);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.Id },
                customer);
        }

        // =====================================
        // PUT: api/Customers/5
        // =====================================

        [HttpPut("{id:int}")]
        public IActionResult UpdateCustomer(
            int id,
            [FromBody] Customer customer)
        {
            if (id <= 0)
                return BadRequest("Invalid customer ID.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCustomer =
                _customerService.GetCustomerById(id);

            if (existingCustomer == null)
                return NotFound("Customer not found.");

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

            var updated =
                _customerService.UpdateCustomer(existingCustomer);

            if (!updated)
                return NotFound("Customer not found.");

            return NoContent();
        }

        // =====================================
        // DELETE: api/Customers/5
        // =====================================

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            var deleted = _customerService.DeleteCustomer(id);

            if (!deleted)
                return NotFound("Customer not found.");

            return NoContent();
        }
    }
}
