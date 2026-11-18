namespace ShopSystemWeb
{
    // Класс отчёта — агрегирует данные о продажах за период
    public class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalRevenue { get; set; }

        public Report(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            TotalRevenue = 0;
        }

        // Сформировать отчёт: отфильтровать продажи по датам и посчитать сумму
        public void Generate(List<Sale> sales)
        {
            TotalRevenue = sales
                .Where(s => s.Date >= StartDate && s.Date < EndDate)
                .Sum(s => s.TotalAmount);
        }
    }
}