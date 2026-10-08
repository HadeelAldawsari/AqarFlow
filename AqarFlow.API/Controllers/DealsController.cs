
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealsController : ControllerBase
    {
        private readonly IDealService _dealService;

        public DealsController(IDealService dealService)
        {
            _dealService = dealService;
        }

        // =====================================
        // GET: api/Deals
        // =====================================

        [HttpGet]
        public IActionResult GetDeals()
        {
            var deals = _dealService.GetAll();

            return Ok(deals);
        }

        // =====================================
        // GET: api/Deals/5
        // =====================================

        [HttpGet("{id:int}")]
        public IActionResult GetDeal(int id)
        {
            var deal = _dealService.GetById(id);

            if (deal == null)
            {
                return NotFound("Deal not found.");
            }

            return Ok(deal);
        }

        // =====================================
        // POST: api/Deals
        // =====================================

        [HttpPost]
        public IActionResult CreateDeal([FromBody] Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            deal.Id = 0;
            deal.CreatedAt = DateTime.Now;

            // Relationships are set using foreign keys.
            deal.Customer = null;
            deal.Property = null;

            _dealService.Add(deal);

            return CreatedAtAction(
                nameof(GetDeal),
                new { id = deal.Id },
                deal);
        }

        // =====================================
        // PUT: api/Deals/5
        // =====================================

        [HttpPut("{id:int}")]
        public IActionResult UpdateDeal(
            int id,
            [FromBody] Deal deal)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid deal ID.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDeal = _dealService.GetById(id);

            if (existingDeal == null)
            {
                return NotFound("Deal not found.");
            }

            existingDeal.CustomerId = deal.CustomerId;
            existingDeal.PropertyId = deal.PropertyId;
            existingDeal.DealType = deal.DealType;
            existingDeal.DealValue = deal.DealValue;
            existingDeal.Commission = deal.Commission;
            existingDeal.Status = deal.Status;
            existingDeal.DealDate = deal.DealDate;
            existingDeal.Notes = deal.Notes;

            _dealService.Update(existingDeal);

            return NoContent();
        }

        // =====================================
        // DELETE: api/Deals/5
        // =====================================

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDeal(int id)
        {
            var deal = _dealService.GetById(id);

            if (deal == null)
            {
                return NotFound("Deal not found.");
            }

            _dealService.Delete(deal);

            return NoContent();
        }
    }
}
