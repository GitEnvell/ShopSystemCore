namespace ShopSystemWeb
{
    // Сущность "Товар" — карточка товара в справочнике магазина
    public class Product
    {
        public int Id { get; set; } // Первичный ключ для БД
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } // Остаток на складе

        // Пустой конструктор для EF Core (требование ORM)
        public Product() { }

        // Основной конструктор с валидацией данных
        public Product(string name, decimal price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым");
            if (price <= 0)
                throw new ArgumentException("Цена должна быть положительной");
            if (stockQuantity < 0)
                throw new ArgumentException("Остаток не может быть отрицательным");

            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}