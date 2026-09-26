using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string City { get; set; }

        public string? Area { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int? Bedrooms { get; set; }

        public int? Bathrooms { get; set; }

        [Precision(18, 2)]
        public decimal? PropertySize { get; set; }

        public string Status { get; set; } = "Available";

        public string? Description { get; set; }

        // Stores the uploaded property image path
        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}

