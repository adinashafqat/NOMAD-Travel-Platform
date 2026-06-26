using System;
using System.Collections.Generic;
using System.Linq;

namespace NOMAD.Services
{
    public class DestinationMetadata
    {
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public string Currency { get; set; } = "USD";
        public string Language { get; set; } = "English";
        public string LanguageCode { get; set; } = "en-US";
        public double Lat { get; set; } = 41.0082;
        public double Lng { get; set; } = 28.9784;
        public string Greeting { get; set; } = "";
        public string TippingCulture { get; set; } = "";
        public string DressCode { get; set; } = "";
        public List<string> DosAndDonts { get; set; } = new();
        public List<string> ScamAlerts { get; set; } = new();
        public string EmergencyInfo { get; set; } = "";
        public List<EmergencyPhrase> EmergencyPhrases { get; set; } = new();
        public List<EmergencyShortcut> EmergencyShortcuts { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public List<DangerAlert> DangerAlerts { get; set; } = new();
        public List<NearbyPlace> NearbyPlaces { get; set; } = new();
        public List<SafetyLocation> SafetyLocations { get; set; } = new();
        public List<OfflineCard> OfflineCards { get; set; } = new();
    }

    public class EmergencyPhrase
    {
        public string English { get; set; } = "";
        public string Translated { get; set; } = "";
        public string Pronunciation { get; set; } = "";
    }

    public class SafetyLocation
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = ""; 
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Distance { get; set; } = "";
    }

    public class OfflineCard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; } = "";
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string MetaData { get; set; } = "";
        public string Icon { get; set; } = "";
    }

    public class WeatherInfo
    {
        public string City { get; set; } = "";
        public int Temperature { get; set; }
        public string Condition { get; set; } = "";
        public string Icon { get; set; } = "";
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
    }

    public class ExchangeRate
    {
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public decimal Rate { get; set; }
        public decimal Change { get; set; } 
    }

    public class DangerAlert
    {
        public string Title { get; set; } = "";
        public string Region { get; set; } = "";
        public string Level { get; set; } = "";
        public string Icon { get; set; } = "";
    }

    public class NearbyPlace
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = ""; 
        public string Distance { get; set; } = "";
        public string Icon { get; set; } = "";
    }

    public class TripStatus
    {
        public string Destination { get; set; } = "";
        public string Country { get; set; } = "";
        public int DaysRemaining { get; set; }
        public int ProgressPercent { get; set; }
        public string Status { get; set; } = "";
    }

    public class BudgetSummary
    {
        public decimal TotalBudget { get; set; }
        public decimal Spent { get; set; }
        public decimal Remaining => TotalBudget - Spent;
        public int PercentUsed => TotalBudget > 0 ? (int)(Spent / TotalBudget * 100) : 0;
        public string Currency { get; set; } = "USD";
    }

    public class QuickStat
    {
        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Color { get; set; } = "";
    }

    public class EmergencyShortcut
    {
        public string Title { get; set; } = "";
        public string Number { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Color { get; set; } = "";
    }

    public class TranslationTool
    {
        public string Original { get; set; } = "";
        public string Translated { get; set; } = "";
        public string Language { get; set; } = "";
    }

    public class ActiveRoute
    {
        public string Origin { get; set; } = "";
        public string Destination { get; set; } = "";
        public string TransportMode { get; set; } = "";
        public string ETA { get; set; } = "";
    }

    public class DashboardService
    {
        public static DestinationMetadata Resolve(string city, string country)
        {
            if (string.IsNullOrWhiteSpace(city)) city = "Istanbul";
            if (string.IsNullOrWhiteSpace(country)) country = "Turkey";

            var meta = new DestinationMetadata
            {
                City = city,
                Country = country
            };

            var cLower = country.ToLower();
            if (cLower.Contains("japan") || city.ToLower().Contains("tokyo") || city.ToLower().Contains("kyoto"))
            {
                meta.Country = "Japan";
                meta.Currency = "JPY";
                meta.Language = "Japanese";
                meta.LanguageCode = "ja-JP";
                meta.Lat = city.ToLower().Contains("kyoto") ? 35.0116 : 35.6762;
                meta.Lng = city.ToLower().Contains("kyoto") ? 135.7681 : 139.6503;
                meta.Greeting = "Konnichiwa (Hello) - Bowing is the standard greeting. A slight bow is polite for peers; a deeper bow shows respect to elders.";
                meta.TippingCulture = "Tipping is not practiced in Japan. It can be seen as awkward or rude. Excellent service is assumed.";
                meta.DressCode = "Modest and neat. Remove shoes when entering temples, traditional guest houses, or private homes.";
                meta.DosAndDonts = new List<string>
                {
                    "DO stand on the left side of escalators in Tokyo (right side in Osaka).",
                    "DO keep your trash with you until you find a bin (they are very rare).",
                    "DON'T talk on your phone or make loud noises on public trains.",
                    "DON'T walk while eating or drinking from convenience stores."
                };
                meta.ScamAlerts = new List<string>
                {
                    "Kabukicho Host Scams: Friendly touts offering cheap all-you-can-drink deals who then charge thousands of dollars.",
                    "Fake Charity Petitions: Scammers asking for signatures for fake international funds while charging cash fees."
                };
                meta.EmergencyInfo = "Police: 110, Ambulance/Fire: 119. General Emergency support: 112.";
                meta.EmergencyPhrases = new List<EmergencyPhrase>
                {
                    new() { English = "I need help", Translated = "助けてください (Tasukete kudasai)", Pronunciation = "Tah-soo-keh-teh koo-dah-sigh" },
                    new() { English = "Call the police", Translated = "警察を呼んでください (Keisatsu o yonde kudasai)", Pronunciation = "Kay-saht-soo oh yohn-deh koo-dah-sigh" },
                    new() { English = "I am lost", Translated = "道に迷いました (Michi ni mayoimashita)", Pronunciation = "Mee-chee nee mah-yoy-mah-shee-tah" },
                    new() { English = "I need a doctor", Translated = "医者が必要です (Isha ga hitsuyo desu)", Pronunciation = "Ee-shah gah heet-soo-yoh-deh-soo" },
                    new() { English = "Where is the hospital?", Translated = "病院はどこですか (Byoin wa doko desuka)", Pronunciation = "Byoh-een wah doh-koh-deh-soo-kah" }
                };
                meta.EmergencyShortcuts = new List<EmergencyShortcut>
                {
                    new() { Title = "Local Police", Number = "110", Icon = "bi-shield-shaded", Color = "#e2a76f" },
                    new() { Title = "Ambulance", Number = "119", Icon = "bi-heart-pulse", Color = "#d85a5a" },
                    new() { Title = "Embassy Support", Number = "+81 3 3224 5000", Icon = "bi-flag-fill", Color = "#5c7275" }
                };
                meta.Recommendations = new List<string>
                {
                    "Explore Kyoto's quiet bamboo paths at 6 AM for beautiful morning mist.",
                    "Use a digital JR Pass or Suica card on Apple Wallet for instant transit access.",
                    "Visit neighborhood temples for tranquil nature strolls away from crowds.",
                    "Download an offline map layer to navigate traditional narrow alleys."
                };
                meta.DangerAlerts = new List<DangerAlert>
                {
                    new() { Title = "Heatwave Warning", Region = $"{city} Metro", Level = "Medium", Icon = "bi-thermometer-sun" },
                    new() { Title = "Earthquake Watch", Region = "Pacific Coast", Level = "Low", Icon = "bi-exclamation-triangle-fill" }
                };
                meta.NearbyPlaces = new List<NearbyPlace>
                {
                    new() { Name = $"{city} University Hospital", Type = "Hospital", Distance = "0.8 km", Icon = "bi-hospital" },
                    new() { Name = "Embassy of the United States", Type = "Embassy", Distance = "2.4 km", Icon = "bi-building" },
                    new() { Name = $"{city} Local Koban (Police Box)", Type = "Police", Distance = "0.3 km", Icon = "bi-shield-shaded" },
                    new() { Name = "Nature View Inn & Gardens", Type = "Hotel", Distance = "1.5 km", Icon = "bi-house-door" }
                };
                meta.SafetyLocations = new List<SafetyLocation>
                {
                    new() { Name = $"{city} Emergency Hospital", Type = "Hospital", Address = "Central District, Chome 2", Phone = "119", Distance = "0.8 km" },
                    new() { Name = $"{city} Central Koban", Type = "Police", Address = "Main Crossing Ave", Phone = "110", Distance = "0.3 km" },
                    new() { Name = "Consular Services", Type = "Embassy", Address = "Chancery Block", Phone = "+81 3 3224 5000", Distance = "2.4 km" }
                };
                meta.OfflineCards = new List<OfflineCard>
                {
                    new() { Type = "Hotel", Title = "Nature View Inn", Content = $"Booking Ref: NOM-9823. Address: {city}, traditional guest house.", MetaData = "Check-in: 15:00", Icon = "bi-building-fill" },
                    new() { Type = "Embassy", Title = "Embassy Support Info", Content = "Emergency Support: +81 3 3224 5000. Open 24/7 for travelers.", MetaData = "Keep physical passport copy ready.", Icon = "bi-flag-fill" },
                    new() { Type = "Route", Title = "Transit extraction", Content = "Take local Shinkansen direct to airport terminal. Journey time ~30 mins.", MetaData = "Last train 23:30", Icon = "bi-train-front-fill" },
                    new() { Type = "Note", Title = "Allergies & Medical", Content = "Penicillin Allergy. Blood Type: O-Negative.", MetaData = "Essential Info", Icon = "bi-heart-pulse-fill" }
                };
            }
            else if (cLower.Contains("france") || cLower.Contains("paris") || city.ToLower().Contains("paris") || city.ToLower().Contains("nice"))
            {
                meta.Country = "France";
                meta.Currency = "EUR";
                meta.Language = "French";
                meta.LanguageCode = "fr-FR";
                meta.Lat = city.ToLower().Contains("nice") ? 43.7102 : 48.8566;
                meta.Lng = city.ToLower().Contains("nice") ? 7.2620 : 2.3522;
                meta.Greeting = "Bonjour (Hello) - A light handshake or kiss on both cheeks (la bise) is standard for greetings.";
                meta.TippingCulture = "Service is included (service compris). A small tip of 5-10% is appreciated for exceptional service.";
                meta.DressCode = "Chic yet practical. Modest attire is required for churches and historical cathedrals.";
                meta.DosAndDonts = new List<string>
                {
                    "DO say 'Bonjour' immediately upon entering any café or boutique.",
                    "DO validate your subway or train ticket before boarding.",
                    "DON'T rush your dining experience; meals are meant to be enjoyed slowly.",
                    "DON'T leave your personal bags or phones unattended on café tables."
                };
                meta.ScamAlerts = new List<string>
                {
                    "Gold Ring Scam: Scammers finding a fake ring on the pavement and pressuring you to purchase it.",
                    "Fake Petitions: Groups of teens distracting you with clipboard petitions while pickpocketing."
                };
                meta.EmergencyInfo = "General Emergency: 112, Police: 17, Ambulance: 15.";
                meta.EmergencyPhrases = new List<EmergencyPhrase>
                {
                    new() { English = "I need help", Translated = "Aidez-moi (Aidez-moi)", Pronunciation = "Eh-deh mwah" },
                    new() { English = "Call the police", Translated = "Appelez la police (Appelez la police)", Pronunciation = "Ah-peh-lay lah poh-lees" },
                    new() { English = "I am lost", Translated = "Je suis perdu (Je suis perdu)", Pronunciation = "Zhuh swee pair-doo" },
                    new() { English = "I need a doctor", Translated = "J'ai besoin d'un médecin", Pronunciation = "Zhay beh-zwan dun mayd-san" },
                    new() { English = "Où est l'hôpital?", Translated = "Où est l'hôpital?", Pronunciation = "Oo eh loh-pee-tahl" }
                };
                meta.EmergencyShortcuts = new List<EmergencyShortcut>
                {
                    new() { Title = "Local Police", Number = "17", Icon = "bi-shield-shaded", Color = "#e2a76f" },
                    new() { Title = "Ambulance", Number = "15", Icon = "bi-heart-pulse", Color = "#d85a5a" },
                    new() { Title = "Embassy", Number = "+33 1 43 12 22 22", Icon = "bi-flag-fill", Color = "#5c7275" }
                };
                meta.Recommendations = new List<string>
                {
                    "Take an early morning stroll along the Seine River for cinematic views.",
                    "Rent a hybrid bicycle to glide through the parks and nature paths.",
                    "Visit neighborhood boulangeries away from the tourist hot spots.",
                    "Download local transport apps for real-time schedule tracking."
                };
                meta.DangerAlerts = new List<DangerAlert>
                {
                    new() { Title = "Pickpocket Alert", Region = "Metropolitan Centers", Level = "High", Icon = "bi-exclamation-triangle-fill" },
                    new() { Title = "Transit Strike Advisory", Region = "National Rail", Level = "Medium", Icon = "bi-info-circle-fill" }
                };
                meta.NearbyPlaces = new List<NearbyPlace>
                {
                    new() { Name = $"{city} General Hospital", Type = "Hospital", Distance = "1.1 km", Icon = "bi-hospital" },
                    new() { Name = "Consulate General Representative", Type = "Embassy", Distance = "3.2 km", Icon = "bi-building" },
                    new() { Name = "Commissariat de Police", Type = "Police", Distance = "0.6 km", Icon = "bi-shield-shaded" },
                    new() { Name = "Misty River Boutique Hotel", Type = "Hotel", Distance = "1.8 km", Icon = "bi-house-door" }
                };
                meta.SafetyLocations = new List<SafetyLocation>
                {
                    new() { Name = $"{city} General Hospital", Type = "Hospital", Address = "Boulevard de l'Hôpital", Phone = "15", Distance = "1.1 km" },
                    new() { Name = "Commissariat de Police", Type = "Police", Address = "Rue de la Paix", Phone = "17", Distance = "0.6 km" },
                    new() { Name = "Consulate Representative", Type = "Embassy", Address = "Avenue des Nations", Phone = "+33 1 43 12 22 22", Distance = "3.2 km" }
                };
                meta.OfflineCards = new List<OfflineCard>
                {
                    new() { Type = "Hotel", Title = "Boutique River Hotel", Content = $"Booking Ref: PAR-4923. Address: {city}, historic center.", MetaData = "Check-in: 14:00", Icon = "bi-building-fill" },
                    new() { Type = "Embassy", Title = "Consulate Support Info", Content = "Emergency: +33 1 43 12 22 22. Open 24/7 for citizen support.", MetaData = "Keep soft copy on cloud.", Icon = "bi-flag-fill" },
                    new() { Type = "Route", Title = "Transit escape route", Content = "Take RER train line direct to major terminal. Journey time ~40 mins.", MetaData = "Last train 00:30", Icon = "bi-train-front-fill" },
                    new() { Type = "Note", Title = "Vital Info", Content = "Penicillin Allergy. Blood Type: O-Negative.", MetaData = "Medical Details", Icon = "bi-heart-pulse-fill" }
                };
            }
            else
            {
                meta.Currency = cLower.Contains("pakistan") || city.ToLower().Contains("lahore") || city.ToLower().Contains("islamabad") || city.ToLower().Contains("rawalpindi") || city.ToLower().Contains("saddar") ? "PKR" :
                                cLower.Contains("uk") || cLower.Contains("london") || cLower.Contains("england") ? "GBP" :
                                cLower.Contains("turkey") || cLower.Contains("istanbul") ? "TRY" : "USD";
                
                meta.Language = cLower.Contains("pakistan") || city.ToLower().Contains("lahore") || city.ToLower().Contains("islamabad") || city.ToLower().Contains("rawalpindi") || city.ToLower().Contains("saddar") ? "Urdu" :
                                cLower.Contains("turkey") || cLower.Contains("istanbul") ? "Turkish" : "English";

                meta.LanguageCode = cLower.Contains("pakistan") || city.ToLower().Contains("lahore") || city.ToLower().Contains("islamabad") || city.ToLower().Contains("rawalpindi") || city.ToLower().Contains("saddar") ? "ur-PK" :
                                    cLower.Contains("turkey") || cLower.Contains("istanbul") ? "tr-TR" : "en-US";

                var cityLower = city.ToLower();
                if (cityLower.Contains("islamabad"))
                {
                    meta.Lat = 33.6844;
                    meta.Lng = 73.0479;
                }
                else if (cityLower.Contains("rawalpindi") || cityLower.Contains("saddar"))
                {
                    meta.Lat = 33.5973;
                    meta.Lng = 73.0479;
                }
                else if (cityLower.Contains("lahore") || cLower.Contains("pakistan"))
                {
                    meta.Lat = 31.5204;
                    meta.Lng = 74.3587;
                }
                else if (cLower.Contains("uk") || cLower.Contains("london"))
                {
                    meta.Lat = 51.5074;
                    meta.Lng = -0.1278;
                }
                else if (cLower.Contains("turkey") || cLower.Contains("istanbul"))
                {
                    meta.Lat = 41.0082;
                    meta.Lng = 28.9784;
                }
                else
                {
                    meta.Lat = 37.7749;
                    meta.Lng = -122.4194;
                }

                meta.Greeting = $"Welcome to {city}! Greet locals with warmth and a smile. Standard polite greetings apply.";
                meta.TippingCulture = $"Usually 5-10% at restaurants. Rounding up fares is common practice.";
                meta.DressCode = "Modest casual dress. Always respect local customs when visiting religious or historical monuments.";
                
                meta.DosAndDonts = new List<string>
                {
                    $"DO explore the beautiful natural and historic landmarks of {city}.",
                    "DO keep bottled water handy during your exploration journeys.",
                    "DON'T leave your personal bags or valuable devices unattended.",
                    "DON'T take photos of secure or military installations without asking."
                };
                
                meta.ScamAlerts = new List<string>
                {
                    "Unlicensed Guides: Fake tour guides charging double for historic excursions.",
                    "Distraction Scams: Someone spilling coffee or dropping keys while an accomplice grabs your wallet."
                };
                
                meta.EmergencyInfo = "General Emergency Support Dial: 112 or local emergency dispatch.";
                
                meta.EmergencyPhrases = cLower.Contains("pakistan") || cityLower.Contains("lahore") || cityLower.Contains("islamabad") || cityLower.Contains("rawalpindi") || cityLower.Contains("saddar") ? new List<EmergencyPhrase>
                {
                    new() { English = "I need help", Translated = "مجھے مدد چاہیے (Mujhe madad chahiye)", Pronunciation = "Moo-jay mah-dahd chah-hee-ay" },
                    new() { English = "Call the police", Translated = "پولیس کو بلائیں (Police ko bulayein)", Pronunciation = "Poh-leece koh boo-lay-een" },
                    new() { English = "I am lost", Translated = "میں راستہ بھول گیا ہوں (Mein rasta bhool gaya hoon)", Pronunciation = "May rahs-tah bhool gah-yah hoon" },
                    new() { English = "I need a doctor", Translated = "مجھے ڈاکٹر کی ضرورت ہے (Mujhe doctor ki zaroorat hai)", Pronunciation = "Moo-jay doc-tor kee zah-roo-raht hay" },
                    new() { English = "Where is the hospital?", Translated = "ہسپتال کہاں ہے؟ (Hospital kahan hai?)", Pronunciation = "Hos-pee-tahl kah-hahn hay" }
                } : cLower.Contains("turkey") || cLower.Contains("istanbul") ? new List<EmergencyPhrase>
                {
                    new() { English = "I need help", Translated = "Yardıma ihtiyacım var", Pronunciation = "Yahr-duh-mah ee-tee-yah-jum var" },
                    new() { English = "Call the police", Translated = "Polisi arayın", Pronunciation = "Poh-lee-see ah-rah-yuhn" },
                    new() { English = "I am lost", Translated = "Kayboldum", Pronunciation = "Ky-bohl-doom" },
                    new() { English = "I need a doctor", Translated = "Bir doktora ihtiyacım var", Pronunciation = "Beer dohk-tor-ah ee-tee-yah-jum var" },
                    new() { English = "Where is the hospital?", Translated = "Hastane nerede?", Pronunciation = "Hahs-tah-neh neh-reh-deh" }
                } : new List<EmergencyPhrase>
                {
                    new() { English = "I need help", Translated = "I need urgent assistance", Pronunciation = "I need help" },
                    new() { English = "Call the police", Translated = "Call the emergency services immediately", Pronunciation = "Call the police" },
                    new() { English = "I am lost", Translated = "I cannot find my way back to safety", Pronunciation = "I am lost" },
                    new() { English = "I need a doctor", Translated = "I require medical attention", Pronunciation = "I need a doctor" },
                    new() { English = "Where is the hospital?", Translated = "Could you direct me to the nearest hospital?", Pronunciation = "Where is the hospital" }
                };

                meta.EmergencyShortcuts = new List<EmergencyShortcut>
                {
                    new() { Title = "Local Police", Number = "112", Icon = "bi-shield-shaded", Color = "#e2a76f" },
                    new() { Title = "Ambulance", Number = "112", Icon = "bi-heart-pulse", Color = "#d85a5a" },
                    new() { Title = "Embassy Support", Number = "+1 202 501 4444", Icon = "bi-flag-fill", Color = "#5c7275" }
                };

                meta.Recommendations = new List<string>
                {
                    $"Wander through the early morning fog of {city} for serene exploration.",
                    "Use secure, recognized local ridesharing apps for all nature excursions.",
                    "Respect standard tipping and courtesy customs when meeting locals.",
                    "Always keep offline maps ready in your pocket database terminal."
                };

                meta.DangerAlerts = new List<DangerAlert>
                {
                    new() { Title = "Weather Shift Alert", Region = $"{city} Metro", Level = "Medium", Icon = "bi-cloud-sun" }
                };

                meta.NearbyPlaces = new List<NearbyPlace>
                {
                    new() { Name = $"{city} City Hospital", Type = "Hospital", Distance = "1.5 km", Icon = "bi-hospital" },
                    new() { Name = "Main Diplomatic Mission", Type = "Embassy", Distance = "4.0 km", Icon = "bi-building" },
                    new() { Name = $"{city} Dispatch Station", Type = "Police", Distance = "0.9 km", Icon = "bi-shield-shaded" },
                    new() { Name = "Ecosystem Safe Haven", Type = "Hotel", Distance = "2.0 km", Icon = "bi-house-door" }
                };

                meta.SafetyLocations = new List<SafetyLocation>
                {
                    new() { Name = $"{city} City Hospital", Type = "Hospital", Address = "Central District Road", Phone = "112", Distance = "1.5 km" },
                    new() { Name = $"{city} Local Dispatch", Type = "Police", Address = "Station Square", Phone = "112", Distance = "0.9 km" },
                    new() { Name = "Main Diplomatic Mission", Type = "Embassy", Address = "Consular Road", Phone = "+1 202 501 4444", Distance = "4.0 km" }
                };

                meta.OfflineCards = new List<OfflineCard>
                {
                    new() { Type = "Hotel", Title = "Ecosystem Safe Haven", Content = $"Booking Ref: NOM-5521. Address: {city}, secure zone.", MetaData = "Check-in: 14:00", Icon = "bi-building-fill" },
                    new() { Type = "Embassy", Title = "Diplomatic Mission Info", Content = "Emergency support: +1 202 501 4444. Call 24/7 in emergency.", MetaData = "Keep identity papers safe.", Icon = "bi-flag-fill" },
                    new() { Type = "Route", Title = "Secure Extraction Route", Content = "Take direct transit express line to main airport junction.", MetaData = "Last train 23:00", Icon = "bi-train-front-fill" },
                    new() { Type = "Note", Title = "Medical Allergies", Content = "Penicillin Allergy. Blood Type: O-Negative.", MetaData = "Vital Info", Icon = "bi-heart-pulse-fill" }
                };
            }

            return meta;
        }

        public WeatherInfo GetCurrentWeather(string city, string country)
        {
            var meta = Resolve(city, country);
            return new WeatherInfo
            {
                City = meta.City,
                Temperature = 24,
                Condition = "Partly Cloudy",
                Icon = "bi-cloud-sun-fill",
                Humidity = 58,
                WindSpeed = 14
            };
        }

        public List<ExchangeRate> GetExchangeRates(string city, string country)
        {
            var meta = Resolve(city, country);
            return new List<ExchangeRate>
            {
                new() { From = "USD", To = meta.Currency, Rate = meta.Currency == "JPY" ? 155.2m : meta.Currency == "EUR" ? 0.92m : meta.Currency == "GBP" ? 0.79m : meta.Currency == "PKR" ? 278.5m : 1.0m, Change = 0.4m },
                new() { From = "USD", To = "EUR", Rate = 0.92m, Change = -0.3m },
                new() { From = "USD", To = "GBP", Rate = 0.79m, Change = 0.1m },
                new() { From = "USD", To = "JPY", Rate = 154.2m, Change = 0.5m },
                new() { From = "USD", To = "PKR", Rate = 278.5m, Change = -0.2m }
            };
        }

        public List<DangerAlert> GetDangerAlerts(string city, string country)
        {
            var meta = Resolve(city, country);
            return meta.DangerAlerts;
        }

        public List<NearbyPlace> GetNearbyPlaces(string city, string country)
        {
            var meta = Resolve(city, country);
            return meta.NearbyPlaces;
        }

        public TripStatus GetActiveTripStatus(string city, string country)
        {
            var meta = Resolve(city, country);
            return new TripStatus
            {
                Destination = meta.City,
                Country = meta.Country,
                DaysRemaining = 5,
                ProgressPercent = 65,
                Status = "Active"
            };
        }

        public BudgetSummary GetBudgetSummary(string city, string country)
        {
            var meta = Resolve(city, country);
            return new BudgetSummary
            {
                TotalBudget = 3000m,
                Spent = 1560.50m,
                Currency = meta.Currency
            };
        }

        public List<QuickStat> GetQuickStats(string city, string country)
        {
            var meta = Resolve(city, country);
            return new List<QuickStat>
            {
                new() { Label = "Target Destination", Value = meta.City, Icon = "bi-geo-alt-fill", Color = "#e2a76f" },
                new() { Label = "Operational Language", Value = meta.Language, Icon = "bi-translate", Color = "#5c7275" },
                new() { Label = "Safety Standing", Value = "Optimal", Icon = "bi-shield-check", Color = "#40916c" },
                new() { Label = "Ledger Currency", Value = meta.Currency, Icon = "bi-cash-coin", Color = "#cbd2c9" }
            };
        }

        public List<string> GetAIRecommendations(string city, string country)
        {
            var meta = Resolve(city, country);
            return meta.Recommendations;
        }

        public List<EmergencyShortcut> GetEmergencyShortcuts(string city, string country)
        {
            var meta = Resolve(city, country);
            return meta.EmergencyShortcuts;
        }

        public List<TranslationTool> GetRecentTranslations(string city, string country)
        {
            var meta = Resolve(city, country);
            return meta.EmergencyPhrases.Select(p => new TranslationTool
            {
                Original = p.English,
                Translated = p.Translated,
                Language = meta.Language
            }).Take(2).ToList();
        }

        public ActiveRoute GetActiveRoute(string city, string country)
        {
            var meta = Resolve(city, country);
            return new ActiveRoute
            {
                Origin = "Base Camp",
                Destination = $"{meta.City} Hub",
                TransportMode = "Nature Trek",
                ETA = "30 mins"
            };
        }
    }
}
