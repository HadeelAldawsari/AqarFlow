
using AqarFlow.Application.Services.Base;
using AqarFlow.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AqarFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // =====================================
        // GET: api/Properties
        // =====================================

        [HttpGet]
        public IActionResult GetProperties()
        {
            var properties = _propertyService.GetAll();

            return Ok(properties);
        }

        // =====================================
        // GET: api/Properties/5
        // =====================================

        [HttpGet("{id:int}")]
        public IActionResult GetProperty(int id)
        {
            var property = _propertyService.GetById(id);

            if (property == null)
            {
                return NotFound("Property not found.");
            }

            return Ok(property);
        }

        // =====================================
        // POST: api/Properties
        // =====================================

        [HttpPost]
        public IActionResult CreateProperty(
            [FromBody] Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            property.Id = 0;
            property.CreatedAt = DateTime.Now;
            property.UpdatedAt = null;

            _propertyService.Add(property);

            return CreatedAtAction(
                nameof(GetProperty),
                new { id = property.Id },
                property);
        }

        // =====================================
        // PUT: api/Properties/5
        // =====================================

        [HttpPut("{id:int}")]
        public IActionResult UpdateProperty(
            int id,
            [FromBody] Property property)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid property ID.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProperty = _propertyService.GetById(id);

            if (existingProperty == null)
            {
                return NotFound("Property not found.");
            }

            existingProperty.Title = property.Title;
            existingProperty.PropertyType = property.PropertyType;
            existingProperty.City = property.City;
            existingProperty.Area = property.Area;
            existingProperty.Price = property.Price;
            existingProperty.Bedrooms = property.Bedrooms;
            existingProperty.Bathrooms = property.Bathrooms;
            existingProperty.PropertySize = property.PropertySize;
            existingProperty.Status = property.Status;
            existingProperty.Description = property.Description;
            existingProperty.ImagePath = property.ImagePath;
            existingProperty.UpdatedAt = DateTime.Now;

            _propertyService.Update(existingProperty);

            return NoContent();
        }

        // =====================================
        // DELETE: api/Properties/5
        // =====================================

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProperty(int id)
        {
            var property = _propertyService.GetById(id);

            if (property == null)
            {
                return NotFound("Property not found.");
            }

            _propertyService.Delete(property);

            return NoContent();
        }
    }
}
