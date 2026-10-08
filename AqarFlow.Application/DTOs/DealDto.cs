namespace AqarFlow.Application.DTOs
{
    public class DealDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string PropertyTitle { get; set; } = "";

        public string DealType { get; set; } = "";

        public decimal? DealValue { get; set; }

        public decimal? Commission { get; set; }

        public DateTime? DealDate { get; set; }

        public string Status { get; set; } = "";
    }
}

