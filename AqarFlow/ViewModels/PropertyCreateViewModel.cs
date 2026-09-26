using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AqarFlow.ViewModels
{
    public class PropertyCreateViewModel
    {
        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string PropertyType { get; set; } = "";

        [Required]
        public string City { get; set; } = "";

        public string? Area { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? Bedrooms { get; set; }

        public int? Bathrooms { get; set; }

        public decimal? PropertySize { get; set; }

        public string Status { get; set; } = "Available";

        public string? Description { get; set; }

        // Receives the image selected from the user's device
        public IFormFile? ImageFile { get; set; }
    }
}

