using System;
using System.Collections.Generic;

namespace NOMAD.Services
{
    public class MapLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Title { get; set; } = "";
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class MapRoute
    {
        public List<double[]> Coordinates { get; set; } = new();
        public string Color { get; set; } = "#2d4a43";
    }

    public class DangerZone
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Intensity { get; set; } 
    }

    public class Checkpoint
    {
        public string Name { get; set; } = "";
        public string Status { get; set; } = "Active";
        public string SafetyRating { get; set; } = "High";
    }

    public class AIJourneyPlan
    {
        public string Destination { get; set; } = "";
        public string Strategy { get; set; } = "";
        public decimal EstimatedCost { get; set; }
        public string EstimatedTime { get; set; } = "0h";
        public string RecommendedTransport { get; set; } = "";
        public List<string> Attractions { get; set; } = new();
        public List<double[]> RouteCoordinates { get; set; } = new();
        public List<Checkpoint> Checkpoints { get; set; } = new();
        public List<DangerZone> LocalDangers { get; set; } = new();
        public List<MapLocation> NearbyPOIs { get; set; } = new();
    }

    public class MapService
    {
        public MapLocation GetCurrentLocation(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return new MapLocation 
            { 
                Lat = meta.Lat, 
                Lng = meta.Lng, 
                Title = $"{meta.City} Hub Station", 
                Type = "User" 
            };
        }

        public List<MapLocation> GetNearbyPlaces(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            var places = new List<MapLocation>();
            
            places.Add(new MapLocation { Lat = meta.Lat + 0.003, Lng = meta.Lng + 0.002, Title = $"{meta.City} Emergency Hospital", Type = "Hospital", Description = "Active 24/7 Medical Hub" });
            places.Add(new MapLocation { Lat = meta.Lat - 0.003, Lng = meta.Lng - 0.003, Title = $"{meta.City} Local Police HQ", Type = "Police", Description = "Liaison Officers Available" });
            places.Add(new MapLocation { Lat = meta.Lat + 0.012, Lng = meta.Lng + 0.012, Title = "Consular Mission Enclave", Type = "Embassy", Description = "Secure Extraction Point" });
            places.Add(new MapLocation { Lat = meta.Lat + 0.007, Lng = meta.Lng + 0.007, Title = "Explorer Safe Lodge", Type = "Hotel", Description = "Nature-friendly Safe Haven" });

            return places;
        }

        public MapRoute GetActiveRoute(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return new MapRoute
            {
                Color = "#e2a76f",
                Coordinates = new List<double[]>
                {
                    new[] { meta.Lat, meta.Lng },
                    new[] { meta.Lat + 0.002, meta.Lng + 0.002 },
                    new[] { meta.Lat + 0.007, meta.Lng + 0.007 },
                    new[] { meta.Lat + 0.012, meta.Lng + 0.012 }
                }
            };
        }

        public List<DangerZone> GetDangerHeatmap(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return new List<DangerZone>
            {
                new() { Lat = meta.Lat - 0.008, Lng = meta.Lng - 0.008, Intensity = 0.85 },
                new() { Lat = meta.Lat - 0.013, Lng = meta.Lng - 0.013, Intensity = 0.70 },
                new() { Lat = meta.Lat - 0.006, Lng = meta.Lng - 0.006, Intensity = 0.90 }
            };
        }

        public AIJourneyPlan GenerateAIJourney(string originCity, string targetDestination, string strategy, bool avoidDanger)
        {
            var metaOrigin = DashboardService.Resolve(originCity, "");
            var metaTarget = DashboardService.Resolve(targetDestination, "");

            var plan = new AIJourneyPlan
            {
                Destination = targetDestination,
                Strategy = strategy
            };

            double distanceFactor = Math.Sqrt(Math.Pow(metaOrigin.Lat - metaTarget.Lat, 2) + Math.Pow(metaOrigin.Lng - metaTarget.Lng, 2));
            
            if (distanceFactor < 0.5)
            {
                plan.EstimatedCost = strategy.Equals("Budget", StringComparison.OrdinalIgnoreCase) ? 15.00m : 45.00m;
                plan.EstimatedTime = "0.5 hrs";
            }
            else if (distanceFactor < 5.0)
            {
                plan.EstimatedCost = (decimal)Math.Round(distanceFactor * 85.0, 2);
                plan.EstimatedTime = $"{Math.Round(distanceFactor * 1.2, 1)} hrs";
            }
            else
            {
                plan.EstimatedCost = (decimal)Math.Round(distanceFactor * 42.0, 2);
                plan.EstimatedTime = $"{Math.Round(distanceFactor * 0.8, 1)} hrs";
            }

            var targetLower = targetDestination.ToLower();
            bool isPakistan = targetLower.Contains("islamabad") || targetLower.Contains("rawalpindi") || targetLower.Contains("saddar") || targetLower.Contains("lahore") || metaTarget.Country.ToLower().Contains("pakistan");

            if (strategy.Equals("Safest", StringComparison.OrdinalIgnoreCase))
            {
                plan.RecommendedTransport = isPakistan ? "Armored Coaster Shuttle via GT Road Corridor" : "Armored Wilderness EV Shuttle";
                plan.Attractions = isPakistan
                    ? new List<string> { "Margalla Hills National Park", "Faisal Mosque Gardens", "Daman-e-Koh Viewpoint", "Trail 5 — Margalla Trails" }
                    : new List<string> { "Misty Gorge Sanctuary", "Highland Fir Trail", "Ancient Cedar Canopy" };
            }
            else if (strategy.Equals("Budget", StringComparison.OrdinalIgnoreCase))
            {
                plan.RecommendedTransport = isPakistan ? "Metro Bus Rapid Transit (Rawalpindi–Islamabad)" : "Scenic Wilderness Rail System";
                plan.Attractions = isPakistan
                    ? new List<string> { "Lok Virsa Heritage Museum", "Saidpur Village", "Pakistan Monument", "Lake View Park" }
                    : new List<string> { "Whispering Glade", "Brambling Stream Picnic Ground", "Eco-Camp Lookout" };
            }
            else
            {
                plan.RecommendedTransport = isPakistan ? "InDrive / Careem Premium via Islamabad Expressway" : "Premium Ranger Rover Offroader";
                plan.Attractions = isPakistan
                    ? new List<string> { "Centaurus Mall Observation Deck", "Shakarparian Hills", "Rawal Lake", "F-9 Park Trail" }
                    : new List<string> { "Jagged Pass Ridge", "Cascading Torrent Falls", "Windy Steppe Overlook" };
            }

            double stepLat = (metaTarget.Lat - metaOrigin.Lat) / 4.0;
            double stepLng = (metaTarget.Lng - metaOrigin.Lng) / 4.0;

            bool isSameArea = Math.Abs(stepLat) < 0.002 && Math.Abs(stepLng) < 0.002;
            if (isSameArea)
            {
                stepLat = 0.008;
                stepLng = 0.006;
            }

            plan.RouteCoordinates.Add(new[] { metaOrigin.Lat, metaOrigin.Lng });
            
            for (int i = 1; i <= 3; i++)
            {
                double checkpointLat = metaOrigin.Lat + (stepLat * i);
                double checkpointLng = metaOrigin.Lng + (stepLng * i);

                if (avoidDanger)
                {
                    checkpointLat += 0.004;
                    checkpointLng -= 0.003;
                }

                plan.RouteCoordinates.Add(new[] { checkpointLat, checkpointLng });
                plan.Checkpoints.Add(new Checkpoint 
                { 
                    Name = $"Extraction Checkpoint {((char)(64 + i))}", 
                    Status = "Cleared", 
                    SafetyRating = avoidDanger ? "High (Bypass Active)" : "Caution (Dangers Nearby)" 
                });
            }

            plan.RouteCoordinates.Add(new[] { metaTarget.Lat, metaTarget.Lng });
            plan.Checkpoints.Add(new Checkpoint { Name = $"{metaTarget.City} Extraction LZ", Status = "Secure Ready", SafetyRating = "Optimal" });

            plan.NearbyPOIs.Add(new MapLocation { Lat = metaTarget.Lat + 0.012, Lng = metaTarget.Lng + 0.008, Title = $"{metaTarget.City} Emergency Hospital", Type = "Hospital", Description = "24/7 Trauma Response Unit" });
            plan.NearbyPOIs.Add(new MapLocation { Lat = metaTarget.Lat - 0.010, Lng = metaTarget.Lng - 0.012, Title = $"{metaTarget.City} Police Command", Type = "Police", Description = "Regional Law Enforcement HQ" });
            plan.NearbyPOIs.Add(new MapLocation { Lat = metaTarget.Lat + 0.018, Lng = metaTarget.Lng - 0.010, Title = $"{metaTarget.City} Diplomatic Enclave", Type = "Embassy", Description = "Foreign Consular Services" });
            plan.NearbyPOIs.Add(new MapLocation { Lat = metaTarget.Lat - 0.008, Lng = metaTarget.Lng + 0.015, Title = $"{metaTarget.City} Safe Lodge", Type = "Hotel", Description = "Verified Secure Accommodation" });
            plan.NearbyPOIs.Add(new MapLocation { Lat = metaTarget.Lat + 0.005, Lng = metaTarget.Lng - 0.018, Title = $"{metaTarget.City} Medical Clinic", Type = "Hospital", Description = "Walk-in Emergency Care" });

            plan.LocalDangers.Add(new DangerZone { Lat = metaTarget.Lat - 0.015, Lng = metaTarget.Lng - 0.012, Intensity = 0.85 });
            plan.LocalDangers.Add(new DangerZone { Lat = metaTarget.Lat + 0.013, Lng = metaTarget.Lng - 0.016, Intensity = 0.70 });
            plan.LocalDangers.Add(new DangerZone { Lat = metaTarget.Lat - 0.008, Lng = metaTarget.Lng + 0.014, Intensity = 0.55 });

            return plan;
        }
    }
}
