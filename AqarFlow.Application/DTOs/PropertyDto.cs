namespace AqarFlow.Application.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string PropertyType { get; set; } = "";

        public string City { get; set; } = "";

        public string? Area { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; } = "";

        public string? ImagePath { get; set; }
    }
}

