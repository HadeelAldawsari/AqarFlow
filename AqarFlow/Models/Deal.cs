using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AqarFlow.Models
{
    // Deal Table:
    // Stores real estate deals between customers and properties.
    // It helps track deal progress, value, commission,
    // and whether the deal was completed or cancelled.

    public class Deal


    {
        // Unique identifier for each deal (Primary Key)
        public int Id { get; set; }

        // Identifies the customer related to the deal (Foreign Key)
        [Required]
        public int CustomerId { get; set; }

        // Identifies the property related to the deal (Foreign Key)
        [Required]
        public int PropertyId { get; set; }

        // Deal type: Sale / Rent
        [Required]
        public string DealType { get; set; }

        // Total value of the deal
        [Precision(18, 2)]
        public decimal? DealValue { get; set; }

        // Commission earned from the deal
        [Precision(18, 2)]
        public decimal? Commission { get; set; }

        // Deal status: Open / Negotiating / Completed / Cancelled
        public string Status { get; set; } = "Open";

        // Date when the deal was completed or recorded
        public DateTime? DealDate { get; set; }

        // Additional notes about the deal
        public string? Notes { get; set; }

        // Date and time when the deal record was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        // Connects this deal to the related customer
        public Customer? Customer { get; set; }

        // Navigation Property
        // Connects this deal to the related property
        public Property? Property { get; set; }


    }
}