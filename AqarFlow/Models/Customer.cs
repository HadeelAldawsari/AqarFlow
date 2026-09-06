using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Models
{
    // Customer Table:
    // Stores customer information and their real estate requirements.
    // It helps the real estate marketer manage customer needs,
    // track their status, and match them with suitable properties.


    public class Customer

    {
        // Unique identifier for each customer (Primary Key)
        public int Id { get; set; }

        // Customer's full name
        [Required]
        public string Name { get; set; }

        // Customer's phone number
        [Required]
        public string Phone { get; set; }

        // Customer's email address (optional)
        public string? Email { get; set; }

        // Customer's purpose: Buy / Rent / Invest
        [Required]
        public string Purpose { get; set; }

        // Preferred property type: Villa / Apartment / Land / Floor
        public string? PropertyType { get; set; }

        // Preferred city
        public string? PreferredCity { get; set; }

        // Preferred area or neighborhood
        public string? PreferredArea { get; set; }

        // Minimum customer budget
        [Precision(18, 2)]
        public decimal? MinBudget { get; set; }

        // Maximum customer budget
        [Precision(18, 2)]
        public decimal? MaxBudget { get; set; }

        // Preferred number of bedrooms
        public int? Bedrooms { get; set; }

        // Customer status in the system
        // New customers are automatically assigned "New"
        public string Status { get; set; } = "New";

        // Customer source: Website / WhatsApp / Instagram / Referral / Other
        public string? Source { get; set; }

        // Additional notes about the customer or their requirements
        public string? Notes { get; set; }

        // Date and time when the customer was added to the system
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Date and time of the last update to the customer's information
        public DateTime? UpdatedAt { get; set; }



    }
}