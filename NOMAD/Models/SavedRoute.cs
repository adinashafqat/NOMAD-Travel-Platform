namespace NOMAD.Models
{
    public class SavedRoute
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
        
        public string Origin { get; set; } = "";
        public string Destination { get; set; } = "";
        public string TransportMode { get; set; } = "";
        public string ETA { get; set; } = "";
    }
}
