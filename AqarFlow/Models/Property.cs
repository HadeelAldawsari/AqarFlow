using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;    

namespace AqarFlow.Models
{
    // Property Table:
    // Stores information about properties marketed by the real estate marketer.
    // It helps manage property listings and match them with suitable customers.


    public class Property
    {

        // Unique identifier for each property (Primary Key)
        public int Id { get; set; }

        // Property title
        // Example: Villa for Sale in Al Yasmin
        [Required]
        public string Title { get; set; }

        // Property type: Villa / Apartment / Land / Floor
        [Required]
        public string PropertyType { get; set; }

        // City where the property is located
        [Required]
        public string City { get; set; }

        // Area or neighborhood where the property is located
        public string? Area { get; set; }

        // Property price
        [Required]
        [Precision(18, 2)] 
        public decimal Price { get; set; }

        // Number of bedrooms
        public int? Bedrooms { get; set; }

        // Number of bathrooms
        public int? Bathrooms { get; set; }

        // Property size in square meters
        [Precision(18, 2)]
        public decimal? PropertySize { get; set; }

        // Property status: Available / Reserved / Sold / Rented / Unavailable
        // New properties are automatically assigned "Available"
        public string Status { get; set; } = "Available";

        // Additional information about the property
        public string? Description { get; set; }

        // Date and time when the property was added to the system
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Date and time of the last update to the property information
        public DateTime? UpdatedAt { get; set; }



    }
}