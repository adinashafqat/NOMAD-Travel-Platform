namespace NOMAD.Models
{
    public class TravelRecord
    {
        public int Id { get; set; }
        public string Destination { get; set; } = string.Empty;
        public string? CountryCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Purpose { get; set; } = "Tourism"; 
        public string? Notes { get; set; }
        public decimal? TotalExpenses { get; set; }
        public string Currency { get; set; } = "USD";
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
