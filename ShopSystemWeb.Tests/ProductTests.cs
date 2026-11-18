using Xunit;
using ShopSystemWeb;

namespace ShopSystemWeb.Tests
{
    // Тесты для класса Product (Товар)
    public class ProductTests
    {
        // Тест: корректное создание товара
        [Fact]
        public void CreateProduct_ValidData_Success()
        {
            // Создаём товар с валидными данными
            var product = new Product("Молоко", 80m, 50);

            // Проверяем, что свойства заполнены верно
            Assert.Equal("Молоко", product.Name);
            Assert.Equal(80m, product.Price);
            Assert.Equal(50, product.StockQuantity);
        }

        // Тест: отрицательная цена должна вызывать ошибку
        [Fact]
        public void CreateProduct_NegativePrice_ThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () => new Product("Хлеб", -10m, 30)
            );
        }

        // Тест: отрицательный остаток должен вызывать ошибку
        [Fact]
        public void CreateProduct_NegativeStock_ThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () => new Product("Сыр", 150m, -5)
            );
        }
    }

    // Тесты для класса Sale (Продажа)
    public class SaleTests
    {
        // Тест: после продажи остаток товара уменьшается
        [Fact]
        public void CompleteSale_ValidSale_StockDecreases()
        {
            var product = new Product("Молоко", 80m, 50);
            var seller = new Seller("seller", "Петров", 1);
            var sale = new Sale(seller);
            sale.AddItem(new SaleItem(product, 5)); // добавляем 5 шт. в чек

            sale.CompleteSale(); // завершаем продажу

            // Было 50, продали 5 → должно остаться 45
            Assert.Equal(45, product.StockQuantity);
        }

        // Тест: нельзя продать больше, чем есть на складе
        [Fact]
        public void CompleteSale_InsufficientStock_ThrowsException()
        {
            var product = new Product("Хлеб", 40m, 2); // на складе только 2 шт.
            var seller = new Seller("seller", "Петров", 1);
            var sale = new Sale(seller);
            sale.AddItem(new SaleItem(product, 10)); // пытаемся продать 10

            // Ожидаем исключение — товара не хватает
            Assert.Throws<InvalidOperationException>(() => sale.CompleteSale());
        }
    }

    // Тесты для класса Report (Отчёт)
    public class ReportTests
    {
        // Тест: при отсутствии продаж выручка = 0
        [Fact]
        public void Generate_EmptySalesList_ZeroRevenue()
        {
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            var sales = new List<Sale>(); // пустой список

            report.Generate(sales);

            Assert.Equal(0m, report.TotalRevenue);
        }

        // Тест: отчёт корректно считает выручку по продажам
        [Fact]
        public void Generate_WithSales_CorrectRevenue()
        {
            var product = new Product("Молоко", 80m, 100);
            var seller = new Seller("seller", "Петров", 1);
            var sale = new Sale(seller);
            sale.AddItem(new SaleItem(product, 2)); // 2 шт. по 80 руб.
            sale.CompleteSale();

            var sales = new List<Sale> { sale };
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));

            report.Generate(sales);

            // 2 × 80 = 160 рублей
            Assert.Equal(160m, report.TotalRevenue);
        }
    }
}