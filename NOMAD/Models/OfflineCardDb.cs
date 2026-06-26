namespace NOMAD.Models
{
    public class OfflineCardDb
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        
        public string Type { get; set; } = ""; 
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string MetaData { get; set; } = "";
    }
}
