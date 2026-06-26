using System.Collections.Generic;

namespace NOMAD.Services
{
    public class CultureGuide
    {
        public string Country { get; set; } = "";
        public string HeroImage { get; set; } = "";
        public string Greeting { get; set; } = "";
        public string TippingCulture { get; set; } = "";
        public string DressCode { get; set; } = "";
        public List<string> DosAndDonts { get; set; } = new();
        public List<string> ScamAlerts { get; set; } = new();
        public string EmergencyInfo { get; set; } = "";
    }

    public class CultureService
    {
        public List<EmergencyPhrase> GetEmergencyPhrases(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return meta.EmergencyPhrases;
        }

        public string MockTranslate(string text, string city = "Istanbul", string country = "Turkey")
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            var meta = DashboardService.Resolve(city, country);
            var lower = text.ToLower();
            
            foreach (var phrase in meta.EmergencyPhrases)
            {
                if (lower.Contains(phrase.English.ToLower())) return phrase.Translated;
            }

            if (lower.Contains("hello") || lower.Contains("hi"))
            {
                return meta.LanguageCode == "ja-JP" ? "Konnichiwa (こんにちは)" :
                       meta.LanguageCode == "fr-FR" ? "Bonjour" :
                       meta.LanguageCode == "ur-PK" ? "Assalam-o-Alaikum (السلام علیکم)" :
                       meta.LanguageCode == "tr-TR" ? "Merhaba" : "Hello";
            }
            if (lower.Contains("thank you") || lower.Contains("thanks"))
            {
                return meta.LanguageCode == "ja-JP" ? "Arigatou gozaimasu (ありがとうございます)" :
                       meta.LanguageCode == "fr-FR" ? "Merci beaucoup" :
                       meta.LanguageCode == "ur-PK" ? "Shukriya (شکریہ)" :
                       meta.LanguageCode == "tr-TR" ? "Teşekkür ederim" : "Thank you";
            }
            if (lower.Contains("how much") || lower.Contains("price"))
            {
                return meta.LanguageCode == "ja-JP" ? "Ikura desuka (いくらですか)" :
                       meta.LanguageCode == "fr-FR" ? "Combien ça coûte?" :
                       meta.LanguageCode == "ur-PK" ? "Yeh kitnay ka hai? (یہ کتنے کا ہے؟)" :
                       meta.LanguageCode == "tr-TR" ? "Ne kadar?" : "How much?";
            }

            return $"[Decrypted in {meta.Language}]: " + text;
        }

        public CultureGuide GetCultureGuide(string city = "Istanbul", string country = "Turkey")
        {
            var meta = DashboardService.Resolve(city, country);
            return new CultureGuide
            {
                Country = meta.Country,
                HeroImage = meta.Country == "Japan" ? "linear-gradient(135deg, #2d4a43 0%, #e2a76f 100%)" :
                            meta.Country == "France" ? "linear-gradient(135deg, #1d3557 0%, #457b9d 100%)" :
                            meta.Country == "Turkey" ? "linear-gradient(135deg, #2d4a43 0%, #5c7275 100%)" :
                            "linear-gradient(135deg, #40916c 0%, #2d4a43 100%)",
                Greeting = meta.Greeting,
                TippingCulture = meta.TippingCulture,
                DressCode = meta.DressCode,
                DosAndDonts = meta.DosAndDonts,
                ScamAlerts = meta.ScamAlerts,
                EmergencyInfo = meta.EmergencyInfo
            };
        }
    }
}
