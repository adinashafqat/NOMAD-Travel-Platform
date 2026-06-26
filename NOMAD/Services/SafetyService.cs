using System.Collections.Generic;

namespace NOMAD.Services
{
    public class SafetyService
    {
        public List<SafetyLocation> GetEmergencyLocations(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return meta.SafetyLocations;
        }

        public List<OfflineCard> GetDefaultOfflineCards(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return meta.OfflineCards;
        }
    }
}
