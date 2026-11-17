using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSystemWeb
{
    // Класс для формирования отчетов по продажам
    public class Report
    {
        public DateTime StartDate { get; private set; } // Начало периода
        public DateTime EndDate { get; private set; } // Конец периода
        public decimal TotalRevenue { get; private set; } // Итоговая выручка за период

        public Report(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) throw new ArgumentException("Дата начала не может быть позже даты окончания");
            StartDate = startDate;
            EndDate = endDate;
        }

        // Метод генерации отчета на основе списка всех продаж
        public void Generate(List<Sale> sales)
        {
            if (sales == null) throw new ArgumentNullException(nameof(sales));

            // Фильтруем продажи: только завершенные и попадающие в заданный период
            var filteredSales = sales.Where(s => s.IsCompleted && s.SaleDate >= StartDate && s.SaleDate <= EndDate);

            // Суммируем выручку по отфильтрованным продажам
            TotalRevenue = filteredSales.Sum(s => s.GetTotalAmount());
        }
    }
}