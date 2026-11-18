using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopSystemWeb.Controllers
{
    // Основной API-контроллер: принимает HTTP-запросы от клиента
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _context; // Контекст БД (внедряется через DI)

        // Конструктор: EF Core сам передаст сюда экземпляр AppDbContext
        public StoreController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Получить все товары со склада
        [HttpGet("stock")]
        public IActionResult GetStock()
        {
            return Ok(_context.Products.ToList());
        }

        // 2. Добавить новый товар в справочник
        [HttpPost("add-product")]
        public IActionResult AddProduct([FromQuery] string name, [FromQuery] decimal price, [FromQuery] int stock)
        {
            try
            {
                var product = new Product(name, price, stock);
                _context.Products.Add(product);
                _context.SaveChanges(); // Сохраняем в БД
                return Ok($"Товар '{name}' добавлен! ID: {product.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка валидации: {ex.Message}");
            }
        }

        // 3. Получить список производителей
        [HttpGet("manufacturers")]
        public IActionResult GetManufacturers()
        {
            return Ok(_context.Manufacturers.ToList());
        }

        // 4. Оформить продажу (списывает товар со склада)
        [HttpPost("make-sale")]
        public IActionResult MakeSale([FromQuery] int productId, [FromQuery] int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound("Товар не найден");

            try
            {
                var seller = _context.Users.OfType<Seller>().FirstOrDefault();
                if (seller == null)
                    return BadRequest("Продавец не найден. Добавьте пользователя с ролью Seller.");

                var sale = new Sale(seller);
                sale.AddItem(new SaleItem(product, quantity));
                sale.CompleteSale(); // Списывает остаток у product

                _context.Sales.Add(sale);
                _context.SaveChanges();

                return Ok($"Продажа оформлена! Сумма: {sale.GetTotalAmount()} руб. Остаток: {product.StockQuantity}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 5. Отчёт о выручке за текущие сутки
        [HttpGet("report")]
        public IActionResult GetReport()
        {
            var sales = _context.Sales.ToList();
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            report.Generate(sales);
            return Ok($"Выручка за сегодня: {report.TotalRevenue} руб.");
        }

        // 6. Удалить товар из справочника
        [HttpDelete("delete-product")]
        public IActionResult DeleteProduct([FromQuery] int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound("Товар не найден");

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok($"Товар '{product.Name}' удалён.");
        }

        // 7. Поиск товара по названию (без учёта регистра)
        [HttpGet("find-product")]
        public IActionResult FindProduct([FromQuery] string name)
        {
            var found = _context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToList();

            if (found.Count == 0)
                return NotFound("Товары не найдены");

            return Ok(found);
        }

        // 8. Оформить приход товара (поставку)
        [HttpPost("add-supply")]
        public IActionResult AddSupply([FromQuery] int productId, [FromQuery] int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound("Товар не найден");

            try
            {
                var manufacturer = _context.Manufacturers.FirstOrDefault();
                if (manufacturer == null)
                    return BadRequest("Сначала добавьте производителя");

                var supply = new Supply(manufacturer);
                supply.AddItem(new SupplyItem(product, quantity, product.Price / 2));
                supply.AcceptSupply(); // Увеличивает StockQuantity

                _context.Supplies.Add(supply);
                _context.SaveChanges();

                return Ok($"Поставка принята! Новый остаток: {product.StockQuantity}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 9. Создать акт расхождения
        [HttpPost("create-discrepancy-act")]
        public IActionResult CreateDiscrepancyAct([FromQuery] string actNumber, [FromQuery] string reason)
        {
            try
            {
                var act = new DiscrepancyAct(actNumber, reason);
                _context.Acts.Add(act);
                _context.SaveChanges();
                return Ok($"Акт №{act.ActNumber} создан.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 10. Экспорт отчёта (текстовое представление)
        [HttpGet("export-report")]
        public IActionResult ExportReport()
        {
            var sales = _context.Sales.ToList();
            var report = new Report(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            report.Generate(sales);
            return Ok($"ЭКСПОРТ:\nВыручка: {report.TotalRevenue} руб.\nПродаж: {sales.Count}");
        }

        // 11. Обновить данные товара (цену и остаток)
        [HttpPut("update-product")]
        public IActionResult UpdateProduct([FromQuery] int productId, [FromQuery] decimal newPrice, [FromQuery] int newStock)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound("Товар не найден");

            try
            {
                product.Price = newPrice;
                product.StockQuantity = newStock;
                _context.SaveChanges();
                return Ok($"Товар обновлён: {product.Name}, цена: {newPrice}, остаток: {newStock}");
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
                _context.Manufacturers.Add(manufacturer);
                _context.SaveChanges();
                return Ok($"Производитель '{name}' добавлен!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message}");
            }
        }

        // 13. Получить все акты расхождения
        [HttpGet("get-acts")]
        public IActionResult GetActs()
        {
            return Ok(_context.Acts.ToList());
        }

        // 14. Изменить статус акта (подтвердить/отклонить)
        [HttpPut("change-act-status")]
        public IActionResult ChangeActStatus([FromQuery] string actNumber, [FromQuery] bool confirm)
        {
            var act = _context.Acts.FirstOrDefault(a => a.ActNumber == actNumber);
            if (act == null)
                return NotFound("Акт не найден");

            if (confirm)
                act.Confirm();
            else
                act.Reject();

            _context.SaveChanges();
            return Ok($"Статус акта №{actNumber}: {(confirm ? "Подтверждён" : "Отклонён")}");
        }
    }
}