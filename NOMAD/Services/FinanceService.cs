namespace NOMAD.Services
{
    public class ExpenseRecord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; } = "USD";
        public string Icon { get; set; } = "bi-cash";
        public string Color { get; set; } = "#ffffff";
    }

    public class FinanceService
    {
        private List<ExpenseRecord> _expenses = new();

        public FinanceService()
        {
        }

        public void SeedForDestination(string city, string country)
        {
            var meta = DashboardService.Resolve(city, country);
            _expenses.Clear();
            _expenses.Add(new ExpenseRecord { Title = $"{meta.City} Eco Lodge Stay", Category = "Hotels", Amount = 350.00m, Date = DateTime.Now.AddDays(-3), Icon = "bi-building-fill", Color = "#2d4a43", Currency = meta.Currency });
            _expenses.Add(new ExpenseRecord { Title = $"{meta.Country} Transit Card", Category = "Transport", Amount = 45.00m, Date = DateTime.Now.AddDays(-5), Icon = "bi-airplane-fill", Color = "#e2a76f", Currency = meta.Currency });
            _expenses.Add(new ExpenseRecord { Title = "Organic Local Rations", Category = "Shopping", Amount = 80.50m, Date = DateTime.Now.AddDays(-1), Icon = "bi-bag-fill", Color = "#cbd2c9", Currency = meta.Currency });
            _expenses.Add(new ExpenseRecord { Title = "Forest Feast Gathering", Category = "Food", Amount = 65.00m, Date = DateTime.Now.AddDays(-2), Icon = "bi-cup-hot-fill", Color = "#5c7275", Currency = meta.Currency });
            _expenses.Add(new ExpenseRecord { Title = "Nature Reserve Entry Pass", Category = "Entertainment", Amount = 30.00m, Date = DateTime.Now.AddDays(-1), Icon = "bi-ticket-perforated-fill", Color = "#40916c", Currency = meta.Currency });
            _expenses.Add(new ExpenseRecord { Title = "Emergency Medical Kit", Category = "Emergencies", Amount = 15.00m, Date = DateTime.Now, Icon = "bi-heart-pulse-fill", Color = "#d85a5a", Currency = meta.Currency });
        }

        public List<ExpenseRecord> GetExpenses()
        {
            return _expenses.OrderByDescending(e => e.Date).ToList();
        }

        public void AddExpense(ExpenseRecord expense)
        {
            _expenses.Add(expense);
        }

        public decimal GetTotalSpent()
        {
            return _expenses.Sum(e => e.Amount);
        }

        public Dictionary<string, decimal> GetSpendingByCategory()
        {
            return _expenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }

        public List<decimal> GetDailySpendingPattern()
        {
            return new List<decimal> { 120, 850, 450, 85, 160, 0, 15 };
        }

        public string GetCategoryColor(string category)
        {
            return category switch
            {
                "Hotels" => "#2d4a43",
                "Food" => "#5c7275", 
                "Transport" => "#e2a76f", 
                "Shopping" => "#cbd2c9", 
                "Entertainment" => "#40916c", 
                "Emergencies" => "#d85a5a", 
                _ => "#94a3b8"
            };
        }

        public string GetCategoryIcon(string category)
        {
            return category switch
            {
                "Hotels" => "bi-building-fill",
                "Food" => "bi-cup-hot-fill",
                "Transport" => "bi-airplane-fill",
                "Shopping" => "bi-bag-fill",
                "Entertainment" => "bi-ticket-perforated-fill",
                "Emergencies" => "bi-heart-pulse-fill",
                _ => "bi-cash"
            };
        }
    }
}
