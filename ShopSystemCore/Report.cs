using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore
{
    public class Report
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal TotalRevenue { get; private set; }

        public Report(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) throw new ArgumentException("Дата начала не может быть позже даты окончания");
            StartDate = startDate;
            EndDate = endDate;
        }

        public void Generate(List<Sale> sales)
        {
            if (sales == null) throw new ArgumentNullException(nameof(sales));

            var filteredSales = sales.Where(s => s.IsCompleted && s.SaleDate >= StartDate && s.SaleDate <= EndDate);
            TotalRevenue = filteredSales.Sum(s => s.GetTotalAmount());
        }
    }
}