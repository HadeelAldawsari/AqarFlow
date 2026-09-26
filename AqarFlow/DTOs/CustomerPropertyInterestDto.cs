namespace AqarFlow.DTOs
{
    public class CustomerPropertyInterestDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string PropertyTitle { get; set; } = "";

        public string Status { get; set; } = "";

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}