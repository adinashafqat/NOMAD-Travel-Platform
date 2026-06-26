namespace NOMAD.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        
        public string Destination { get; set; } = "";
        public string Country { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Planned"; 
        public decimal TotalBudget { get; set; }
        
        public List<Expense> Expenses { get; set; } = new();
        public List<SavedRoute> SavedRoutes { get; set; } = new();
    }
}
