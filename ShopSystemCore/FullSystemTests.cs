using NUnit.Framework;
using GroceryStore;
using System;
using System.Collections.Generic;

namespace GroceryStoreTests
{
    [TestFixture]
    public class FullSystemTests
    {
        [Test]
        public void Sale_CompleteSale_WithItems_ShouldReduceStock()
        {
            var seller = new Seller("seller1", "Иванов И.И.", 1);
            var product = new Product("Молоко", 80m, 10);
            var sale = new Sale(seller);
            sale.AddItem(new SaleItem(product, 3));

            sale.CompleteSale();

            Assert.IsTrue(sale.IsCompleted);
            Assert.AreEqual(240m, sale.GetTotalAmount());
            Assert.AreEqual(7, product.StockQuantity);
        }

        [Test]
        public void Supply_AcceptSupply_WithItems_ShouldIncreaseStock()
        {
            var manufacturer = new Manufacturer("Молокозавод №1", "1234567890");
            var product = new Product("Сыр", 200m, 5);
            var supply = new Supply(manufacturer);
            supply.AddItem(new SupplyItem(product, 10, 150m));

            supply.AcceptSupply();

            Assert.IsTrue(supply.IsAccepted);
            Assert.AreEqual(15, product.StockQuantity);
            Assert.AreEqual(1500m, supply.GetTotalAmount());
        }

        [Test]
        public void Report_Generate_ShouldCalculateTotalRevenue()
        {
            var seller = new Seller("s1", "Петров", 2);
            var product = new Product("Хлеб", 40m, 100);
            var sale1 = new Sale(seller);
            sale1.AddItem(new SaleItem(product, 2));
            sale1.CompleteSale();

            var salesList = new List<Sale> { sale1 };
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            report.Generate(salesList);

            Assert.AreEqual(80m, report.TotalRevenue);
        }
    }
}