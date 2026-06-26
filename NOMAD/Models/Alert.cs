namespace NOMAD.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public string Type { get; set; } = ""; 
        public string Severity { get; set; } = "Info"; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
