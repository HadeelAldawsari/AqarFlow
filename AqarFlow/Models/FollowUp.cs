using System.ComponentModel.DataAnnotations;

namespace AqarFlow.Models
{
    // FollowUp Table:
    // Stores all follow-up activities and communications with customers.
    // It helps the real estate marketer track customer interactions
    // and schedule future follow-ups.


    public class FollowUp


    {
        // Unique identifier for each follow-up (Primary Key)
        public int Id { get; set; }

        // Identifies the customer related to this follow-up (Foreign Key)
        [Required]
        public int CustomerId { get; set; }

        // Date and time of the communication with the customer
        public DateTime ContactDate { get; set; } = DateTime.Now;

        // Communication method: Call / WhatsApp / Meeting / Email
        public string? ContactMethod { get; set; }

        // Result of the communication
        // Example: Interested / No Answer / Requested More Information
        public string? Result { get; set; }

        // Date and time for the next follow-up with the customer
        public DateTime? NextFollowUpDate { get; set; }

        // Follow-up status: Pending / Completed / Cancelled
        public string Status { get; set; } = "Pending";

        // Additional notes about the follow-up
        public string? Notes { get; set; }

        // Date and time when the follow-up record was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        // Connects this follow-up to the related customer
        public Customer? Customer { get; set; }




    }
}