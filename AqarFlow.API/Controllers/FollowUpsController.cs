
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpsController : ControllerBase
    {
        private readonly IFollowUpService _followUpService;

        public FollowUpsController(IFollowUpService followUpService)
        {
            _followUpService = followUpService;
        }

        // =====================================
        // GET: api/FollowUps
        // =====================================

        [HttpGet]
        public IActionResult GetFollowUps()
        {
            var followUps = _followUpService.GetAll();

            return Ok(followUps);
        }

        // =====================================
        // GET: api/FollowUps/5
        // =====================================

        [HttpGet("{id:int}")]
        public IActionResult GetFollowUp(int id)
        {
            var followUp = _followUpService.GetById(id);

            if (followUp == null)
            {
                return NotFound("Follow-up not found.");
            }

            return Ok(followUp);
        }

        // =====================================
        // POST: api/FollowUps
        // =====================================

        [HttpPost]
        public IActionResult CreateFollowUp(
            [FromBody] FollowUp followUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            followUp.Id = 0;
            followUp.CreatedAt = DateTime.Now;

            // The customer is linked using CustomerId
            followUp.Customer = null;

            _followUpService.Add(followUp);

            return CreatedAtAction(
                nameof(GetFollowUp),
                new { id = followUp.Id },
                followUp);
        }

        // =====================================
        // PUT: api/FollowUps/5
        // =====================================

        [HttpPut("{id:int}")]
        public IActionResult UpdateFollowUp(
            int id,
            [FromBody] FollowUp followUp)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid follow-up ID.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFollowUp = _followUpService.GetById(id);

            if (existingFollowUp == null)
            {
                return NotFound("Follow-up not found.");
            }

            existingFollowUp.CustomerId = followUp.CustomerId;
            existingFollowUp.ContactDate = followUp.ContactDate;
            existingFollowUp.ContactMethod = followUp.ContactMethod;
            existingFollowUp.Result = followUp.Result;
            existingFollowUp.NextFollowUpDate =
                followUp.NextFollowUpDate;
            existingFollowUp.Status = followUp.Status;
            existingFollowUp.Notes = followUp.Notes;

            _followUpService.Update(existingFollowUp);

            return NoContent();
        }

        // =====================================
        // DELETE: api/FollowUps/5
        // =====================================

        [HttpDelete("{id:int}")]
        public IActionResult DeleteFollowUp(int id)
        {
            var followUp = _followUpService.GetById(id);

            if (followUp == null)
            {
                return NotFound("Follow-up not found.");
            }

            _followUpService.Delete(followUp);

            return NoContent();
        }
    }
}
