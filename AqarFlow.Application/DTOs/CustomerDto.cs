namespace AqarFlow.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Purpose { get; set; } = "";

        public string? PropertyType { get; set; }

        public string? PreferredArea { get; set; }

        public string Status { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}

