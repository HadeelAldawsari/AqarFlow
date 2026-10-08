
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPropertyInterestsController : ControllerBase
    {
        private readonly ICustomerPropertyInterestService _interestService;

        public CustomerPropertyInterestsController(
            ICustomerPropertyInterestService interestService)
        {
            _interestService = interestService;
        }

        // =====================================
        // GET: api/CustomerPropertyInterests
        // =====================================

        [HttpGet]
        public IActionResult GetInterests()
        {
            var interests = _interestService.GetAll();

            return Ok(interests);
        }

        // =====================================
        // GET: api/CustomerPropertyInterests/5
        // =====================================

        [HttpGet("{id:int}")]
        public IActionResult GetInterest(int id)
        {
            var interest = _interestService.GetById(id);

            if (interest == null)
            {
                return NotFound("Customer property interest not found.");
            }

            return Ok(interest);
        }

        // =====================================
        // POST: api/CustomerPropertyInterests
        // =====================================

        [HttpPost]
        public IActionResult CreateInterest(
            [FromBody] CustomerPropertyInterest interest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            interest.Id = 0;
            interest.CreatedAt = DateTime.Now;

            // Relationships are set using foreign keys.
            interest.Customer = null;
            interest.Property = null;

            _interestService.Add(interest);

            return CreatedAtAction(
                nameof(GetInterest),
                new { id = interest.Id },
                interest);
        }

        // =====================================
        // PUT: api/CustomerPropertyInterests/5
        // =====================================

        [HttpPut("{id:int}")]
        public IActionResult UpdateInterest(
            int id,
            [FromBody] CustomerPropertyInterest interest)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid interest ID.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingInterest = _interestService.GetById(id);

            if (existingInterest == null)
            {
                return NotFound("Customer property interest not found.");
            }

            existingInterest.CustomerId = interest.CustomerId;
            existingInterest.PropertyId = interest.PropertyId;
            existingInterest.Status = interest.Status;
            existingInterest.Notes = interest.Notes;

            _interestService.Update(existingInterest);

            return NoContent();
        }

        // =====================================
        // DELETE: api/CustomerPropertyInterests/5
        // =====================================

        [HttpDelete("{id:int}")]
        public IActionResult DeleteInterest(int id)
        {
            var interest = _interestService.GetById(id);

            if (interest == null)
            {
                return NotFound("Customer property interest not found.");
            }

            _interestService.Delete(interest);

            return NoContent();
        }
    }
}
