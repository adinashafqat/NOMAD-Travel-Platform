using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NOMAD.DTOs
{
    public class VisitedCountryLog
    {
        public string CountryName { get; set; } = string.Empty;
        public int Rating { get; set; } = 5; 
        public string Opinion { get; set; } = string.Empty;
    }

    public class ProfileUpdateDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(200, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;

        public string? Nationality { get; set; }

        [Required]
        public string PreferredLanguage { get; set; } = "English";

        [Required]
        public string PreferredCurrency { get; set; } = "USD";

        public string? Bio { get; set; }

        [Phone(ErrorMessage = "Enter a valid phone number")]
        public string? PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; } = "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?auto=format&fit=crop&w=250&q=80";

        public List<VisitedCountryLog> VisitedCountries { get; set; } = new List<VisitedCountryLog>();
    }
}