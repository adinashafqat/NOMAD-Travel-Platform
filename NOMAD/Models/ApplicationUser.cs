using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NOMAD.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? Nationality { get; set; }
        public string PreferredLanguage { get; set; } = "English";
        public string PreferredCurrency { get; set; } = "USD";

        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }

        public string? SerializedVisitedCountries { get; set; } = "[]";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ActiveDestination { get; set; }
        public string? ActiveCountry { get; set; }

        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
        public virtual TravelPreferences? TravelPreferences { get; set; }
        public virtual ICollection<TravelRecord> TravelHistory { get; set; } = new List<TravelRecord>();
    }
}