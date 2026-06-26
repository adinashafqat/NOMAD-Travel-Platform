namespace NOMAD.Models
{
    public class TravelPreferences
    {
        public int Id { get; set; }
        public string TravelStyle { get; set; } = "Budget"; 
        public string PreferredActivities { get; set; } = string.Empty; 
        public string FavoriteDestinations { get; set; } = string.Empty; 
        public bool ReceiveWeatherAlerts { get; set; } = true;
        public bool ReceiveSafetyAlerts { get; set; } = true;
        public bool ReceiveNewsAlerts { get; set; } = true;
        public string DietaryRestrictions { get; set; } = string.Empty;
        public string AccessibilityNeeds { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
