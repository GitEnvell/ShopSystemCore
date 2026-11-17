using Microsoft.AspNetCore.Mvc;

namespace ShopSystemWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        // Статические списки для хранения данных в памяти (имитация БД)
        private static List<Product> _products = new List<Product>();
        private static List<Manufacturer> _manufacturers = new List<Manufacturer>();
        private static List<Sale> _sales = new List<Sale>();
        private static List<DiscrepancyAct> _acts = new List<DiscrepancyAct>(); // Список для актов

        // Тестовые пользователи
        private static Manager _manager = new Manager("admin", "Иванов И.И.", "Администрация");
        private static Seller _seller = new Seller("seller", "Петров П.П.", 1);

        // Инициализация тестовыми данными при первом запуске сервера
        static StoreController()
        {
            _products.Add(new Product("Молоко", 80m, 50));
            _products.Add(new Product("Хлеб", 40m, 30));
            _manufacturers.Add(new Manufacturer("Молокозавод №1", "1234567890"));
            _manufacturers.Add(new Manufacturer("Хлебозавод", "0987654321"));
        }

        // 1. Показать все товары на складе
        [HttpGet("stock")]
        public IActionResult GetStock()
        {
            return Ok(_products);
        }

        // 2. Добавить товар в справочник
        [HttpPost("add-product")]
        public IActionResult AddProduct([FromQuery] string name, [FromQuery] decimal price, [FromQuery] int stock)
        {
            try
            {
                var newProduct = new Product(name, price, stock);
                _products.Add(newProduct);
                return Ok($"Товар '{name}' успешно добавлен!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 3. Показать всех производителей
        [HttpGet("manufacturers")]
        public IActionResult GetManufacturers()
        {
            return Ok(_manufacturers);
        }

        // 4. Оформить продажу
        [HttpPost("make-sale")]
        public IActionResult MakeSale([FromQuery] int productId, [FromQuery] int quantity)
        {
            if (productId < 0 || productId >= _products.Count)
                return NotFound("Товар не найден");

            var product = _products[productId];

            try
            {
                var sale = new Sale(_seller);
                sale.AddItem(new SaleItem(product, quantity));
                sale.CompleteSale();
                _sales.Add(sale);

                return Ok($"Продажа оформлена! Сумма: {sale.GetTotalAmount()} руб. Остаток товара: {product.StockQuantity}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 5. Получить отчет за сегодня (упрощенно)
        [HttpGet("report")]
        public IActionResult GetReport()
        {
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            report.Generate(_sales);
            return Ok($"Общая выручка за сегодня: {report.TotalRevenue} руб.");
        }

        // 6. Удалить товар из справочника
        [HttpDelete("delete-product")]
        public IActionResult DeleteProduct([FromQuery] int productId)
        {
            if (productId < 0 || productId >= _products.Count)
                return NotFound("Товар не найден");

            var product = _products[productId];
            _products.RemoveAt(productId);
            return Ok($"Товар '{product.Name}' удален из справочника.");
        }

        // 7. Поиск товара по названию
        [HttpGet("find-product")]
        public IActionResult FindProduct([FromQuery] string name)
        {
            var foundProducts = _products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            if (foundProducts.Count == 0)
                return NotFound("Товары не найдены");

            return Ok(foundProducts);
        }

        // 8. Оформить поставку (приёмка товара)
        [HttpPost("add-supply")]
        public IActionResult AddSupply([FromQuery] int productId, [FromQuery] int quantity)
        {
            if (productId < 0 || productId >= _products.Count)
                return NotFound("Товар не найден");

            var product = _products[productId];

            try
            {
                var manufacturer = _manufacturers[0]; // Берем первого производителя для примера
                var supply = new Supply(manufacturer);
                supply.AddItem(new SupplyItem(product, quantity, product.Price / 2)); // Закупочная цена = половина от продажной
                supply.AcceptSupply(); // Принимаем (добавляем остатки)

                return Ok($"Поставка принята! Товар '{product.Name}' добавлен на склад. Новый остаток: {product.StockQuantity}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 9. Оформить акт расхождения (с сохранением в список)
        [HttpPost("create-discrepancy-act")]
        public IActionResult CreateDiscrepancyAct([FromQuery] string actNumber, [FromQuery] string reason)
        {
            try
            {
                var act = new DiscrepancyAct(actNumber, reason);
                _acts.Add(act); // Сохраняем акт в список
                return Ok($"Акт расхождения №{act.ActNumber} создан. Причина: {act.Reason}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 10. Экспорт отчета (упрощенно: просто возвращает строку)
        [HttpGet("export-report")]
        public IActionResult ExportReport()
        {
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            report.Generate(_sales);
            return Ok($"ЭКСПОРТ ОТЧЕТА:\nВыручка за сутки: {report.TotalRevenue} руб.\nКоличество продаж: {_sales.Count}");
        }

        // 11. Изменить данные товара (цену и остаток)
        [HttpPut("update-product")]
        public IActionResult UpdateProduct([FromQuery] int productId, [FromQuery] decimal newPrice, [FromQuery] int newStock)
        {
            if (productId < 0 || productId >= _products.Count)
                return NotFound("Товар не найден");

            try
            {
                var oldProduct = _products[productId];
                var updatedProduct = new Product(oldProduct.Name, newPrice, newStock);
                _products[productId] = updatedProduct;

                return Ok($"Товар '{updatedProduct.Name}' обновлен! Новая цена: {newPrice}, остаток: {newStock}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 12. Добавить производителя
        [HttpPost("add-manufacturer")]
        public IActionResult AddManufacturer([FromQuery] string name, [FromQuery] string inn)
        {
            try
            {
                var manufacturer = new Manufacturer(name, inn);
                _manufacturers.Add(manufacturer);
                return Ok($"Производитель '{name}' успешно добавлен!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 13. Получить список всех актов
        [HttpGet("get-acts")]
        public IActionResult GetActs()
        {
            return Ok(_acts);
        }

        // 14. Изменить статус акта (подтвердить или отклонить)
        [HttpPut("change-act-status")]
        public IActionResult ChangeActStatus([FromQuery] string actNumber, [FromQuery] bool confirm)
        {
            var act = _acts.FirstOrDefault(a => a.ActNumber == actNumber);
            if (act == null)
                return NotFound("Акт с таким номером не найден");

            if (confirm)
                act.Confirm();
            else
                act.Reject();

            return Ok($"Статус акта №{actNumber} изменен на: {(confirm ? "Подтвержден" : "Отклонен")}");
        }
    }
}