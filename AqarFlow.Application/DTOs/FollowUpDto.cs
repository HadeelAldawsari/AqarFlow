namespace AqarFlow.Application.DTOs
{
    public class FollowUpDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public DateTime ContactDate { get; set; }

        public string? ContactMethod { get; set; }

        public string? Result { get; set; }

        public DateTime? NextFollowUpDate { get; set; }

        public string Status { get; set; } = "";
    }
}

