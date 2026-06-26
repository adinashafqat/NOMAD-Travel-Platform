namespace NOMAD.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
        
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public DateTime Date { get; set; }
    }
}
