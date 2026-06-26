namespace NOMAD.Services
{
    public class DailyForecast
    {
        public string Day { get; set; } = "";
        public int HighTemp { get; set; }
        public int LowTemp { get; set; }
        public string Condition { get; set; } = "";
        public string Icon { get; set; } = "";
        public int PrecipitationChance { get; set; }
    }

    public class WeatherAlert
    {
        public string Type { get; set; } = ""; 
        public string Severity { get; set; } = ""; 
        public string Message { get; set; } = "";
        public string Timeframe { get; set; } = "";
    }

    public class WeatherData
    {
        public string City { get; set; } = "";
        public int CurrentTemp { get; set; }
        public int FeelsLike { get; set; }
        public string MainCondition { get; set; } = ""; 
        public string Description { get; set; } = "";
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public int UVIndex { get; set; }
        public string Sunrise { get; set; } = "";
        public string Sunset { get; set; } = "";
        public string TravelCondition { get; set; } = ""; 
        
        public List<DailyForecast> WeeklyForecast { get; set; } = new();
        public List<WeatherAlert> ActiveAlerts { get; set; } = new();
        public List<string> AIRecommendations { get; set; } = new();
    }

    public class WeatherService
    {
        public async Task<WeatherData> GetLiveWeatherAsync(string city = "Istanbul", string country = "")
        {
            await Task.Delay(300);

            var meta = DashboardService.Resolve(city, country);
            var isStorm = city.Equals("Tokyo", StringComparison.OrdinalIgnoreCase) || 
                          city.Equals("Hiroshima", StringComparison.OrdinalIgnoreCase);

            if (isStorm)
            {
                var storm = GetStormMockData(meta.City);
                storm.AIRecommendations = meta.Recommendations;
                storm.ActiveAlerts = new List<WeatherAlert>
                {
                    new() { Type = "Storm Warning", Severity = "Critical", Message = $"Active typhoon vector approaching {meta.City}.", Timeframe = "Next 12 Hours" },
                    new() { Type = "Flash Flood Advisory", Severity = "High", Message = "Low lying terrain susceptible to extreme flooding.", Timeframe = "Immediate" }
                };
                return storm;
            }

            var clear = GetClearMockData(meta.City);
            clear.AIRecommendations = meta.Recommendations;
            return clear;
        }

        private WeatherData GetClearMockData(string city)
        {
            return new WeatherData
            {
                City = city,
                CurrentTemp = 24,
                FeelsLike = 26,
                MainCondition = "Clouds",
                Description = "Partly cloudy with pleasant breezes.",
                Humidity = 55,
                WindSpeed = 12,
                UVIndex = 6,
                Sunrise = "06:15 AM",
                Sunset = "08:30 PM",
                TravelCondition = "Optimal",
                WeeklyForecast = new List<DailyForecast>
                {
                    new() { Day = "Mon", HighTemp = 25, LowTemp = 18, Condition = "Sunny", Icon = "bi-sun", PrecipitationChance = 0 },
                    new() { Day = "Tue", HighTemp = 24, LowTemp = 17, Condition = "Cloudy", Icon = "bi-cloud", PrecipitationChance = 10 },
                    new() { Day = "Wed", HighTemp = 22, LowTemp = 16, Condition = "Rain", Icon = "bi-cloud-rain", PrecipitationChance = 60 },
                    new() { Day = "Thu", HighTemp = 23, LowTemp = 15, Condition = "Partly Cloudy", Icon = "bi-cloud-sun", PrecipitationChance = 20 },
                    new() { Day = "Fri", HighTemp = 26, LowTemp = 18, Condition = "Sunny", Icon = "bi-sun", PrecipitationChance = 0 },
                    new() { Day = "Sat", HighTemp = 27, LowTemp = 19, Condition = "Sunny", Icon = "bi-sun", PrecipitationChance = 0 },
                    new() { Day = "Sun", HighTemp = 25, LowTemp = 18, Condition = "Cloudy", Icon = "bi-cloud", PrecipitationChance = 15 }
                },
                ActiveAlerts = new List<WeatherAlert>(),
                AIRecommendations = new List<string>
                {
                    "Light breathable clothing recommended for the day.",
                    "A light jacket is advisable for the evening.",
                    "Conditions are optimal for walking tours and outdoor activities.",
                    "UV Index is moderate; apply sunscreen if outdoors for extended periods."
                }
            };
        }

        private WeatherData GetStormMockData(string city)
        {
            return new WeatherData
            {
                City = city,
                CurrentTemp = 18,
                FeelsLike = 15,
                MainCondition = "Storm",
                Description = "Heavy thunderstorms and strong winds.",
                Humidity = 90,
                WindSpeed = 45,
                UVIndex = 1,
                Sunrise = "05:45 AM",
                Sunset = "07:15 PM",
                TravelCondition = "Danger",
                WeeklyForecast = new List<DailyForecast>
                {
                    new() { Day = "Mon", HighTemp = 19, LowTemp = 14, Condition = "Storm", Icon = "bi-cloud-lightning-rain", PrecipitationChance = 95 },
                    new() { Day = "Tue", HighTemp = 20, LowTemp = 15, Condition = "Rain", Icon = "bi-cloud-rain", PrecipitationChance = 80 },
                    new() { Day = "Wed", HighTemp = 23, LowTemp = 16, Condition = "Cloudy", Icon = "bi-cloud", PrecipitationChance = 30 },
                    new() { Day = "Thu", HighTemp = 25, LowTemp = 18, Condition = "Sunny", Icon = "bi-sun", PrecipitationChance = 0 },
                    new() { Day = "Fri", HighTemp = 24, LowTemp = 18, Condition = "Partly Cloudy", Icon = "bi-cloud-sun", PrecipitationChance = 10 },
                    new() { Day = "Sat", HighTemp = 26, LowTemp = 19, Condition = "Sunny", Icon = "bi-sun", PrecipitationChance = 0 },
                    new() { Day = "Sun", HighTemp = 26, LowTemp = 20, Condition = "Sunny", Icon = "bi-sun", PrecipitationChance = 5 }
                },
                ActiveAlerts = new List<WeatherAlert>
                {
                    new() { Type = "Storm Warning", Severity = "Severe", Message = "Category 3 Typhoon approaching. High winds expected.", Timeframe = "Next 48 Hours" },
                    new() { Type = "Flash Flood Watch", Severity = "High", Message = "Low lying areas susceptible to flooding.", Timeframe = "Until 11 PM" }
                },
                AIRecommendations = new List<string>
                {
                    "REMAIN INDOORS: Cancel all non-essential travel.",
                    "Keep emergency kits and flashlights readily available.",
                    "Secure loose outdoor items immediately.",
                    "Monitor local broadcast channels for evacuation orders."
                }
            };
        }
    }
}
