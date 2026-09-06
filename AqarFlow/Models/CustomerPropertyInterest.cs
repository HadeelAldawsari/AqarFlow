using System.ComponentModel.DataAnnotations;

namespace AqarFlow.Models
{
    // Customer Property Interest Table:
    // Connects customers with properties they are interested in
    // or properties recommended to them by the real estate marketer.


    public class CustomerPropertyInterest

    {
        // Unique identifier for each customer-property interest record
        // Primary Key
        public int Id { get; set; }

        // Identifies the related customer
        // Foreign Key
        [Required]
        public int CustomerId { get; set; }

        // Identifies the related property
        // Foreign Key
        [Required]
        public int PropertyId { get; set; }

        // Customer's interest status for this property
        // Interested / Viewed / Rejected / Negotiating / Selected
        public string Status { get; set; } = "Interested";

        // Additional notes about the customer's interest
        public string? Notes { get; set; }

        // Date and time when the property was linked to the customer
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        // Connects this record to the related customer
        public Customer? Customer { get; set; }

        // Navigation Property
        // Connects this record to the related property
        public Property? Property { get; set; }


    }
}