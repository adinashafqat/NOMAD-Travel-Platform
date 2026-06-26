using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NOMAD.Services
{
    public class ToastNotification
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public string Type { get; set; } = "Info"; 
        public string Icon => Type switch
        {
            "Warning" => "bi-exclamation-triangle-fill",
            "Danger" => "bi-shield-fill-exclamation",
            "Success" => "bi-check-circle-fill",
            _ => "bi-info-circle-fill"
        };
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class NotificationService : IDisposable
    {
        public event Action? OnNotify;
        private readonly List<ToastNotification> _notifications = new();
        private Timer? _simulationTimer;
        private readonly Random _random = new();

        private readonly List<(string Title, string Message, string Type)> _simulatedAlerts = new()
        {
            ("METEOROLOGICAL INTEL 🌧️", "Cold atmospheric front moving into your sector. High winds up to 45 km/h expected.", "Warning"),
            ("TACTICAL WEATHER UPDATE 🌤️", "Golden sunlight through trees is increasing trail visibility in regional forests.", "Success"),
            ("COMMERCE TELEMETRY 📈", "Local currency exchange spreads have fluctuated by 1.45%. Optimal exchange window open.", "Info"),
            ("SECURITY ALERT 🚨", "Anomalous network ping detected on encrypted backup proxy node. Monitoring status.", "Danger")
        };

        public List<ToastNotification> GetActiveNotifications()
        {
            lock (_notifications)
            {
                return new List<ToastNotification>(_notifications);
            }
        }

        public void ShowAlert(string title, string message, string type = "Info")
        {
            var toast = new ToastNotification
            {
                Title = title,
                Message = message,
                Type = type
            };

            lock (_notifications)
            {
                _notifications.Add(toast);
            }

            OnNotify?.Invoke();

            Task.Run(async () =>
            {
                await Task.Delay(6000);
                RemoveAlert(toast.Id);
            });
        }

        public void RemoveAlert(string id)
        {
            lock (_notifications)
            {
                var toast = _notifications.Find(n => n.Id == id);
                if (toast != null)
                {
                    _notifications.Remove(toast);
                }
            }
            OnNotify?.Invoke();
        }

        public void ClearAll()
        {
            lock (_notifications)
            {
                _notifications.Clear();
            }
            OnNotify?.Invoke();
        }

        public void StartSimulation()
        {
            if (_simulationTimer != null) return;

            _simulationTimer = new Timer(TriggerSimulatedAlert, null, 3000, 20000);
        }

        public void StopSimulation()
        {
            _simulationTimer?.Dispose();
            _simulationTimer = null;
        }

        private void TriggerSimulatedAlert(object? state)
        {
            int index = _random.Next(_simulatedAlerts.Count);
            var alert = _simulatedAlerts[index];
            ShowAlert(alert.Title, alert.Message, alert.Type);
        }

        public void Dispose()
        {
            StopSimulation();
        }
    }
}