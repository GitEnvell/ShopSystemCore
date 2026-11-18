namespace ShopSystemWeb
{
    // Позиция чека — одна строка в продаже (товар + количество)
    public class SaleItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Цена на момент продажи

        public SaleItem() { }

        public SaleItem(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным");

            Product = product;
            Quantity = quantity;
            Price = product.Price; // Фиксируем цену на момент продажи
        }

        // Рассчитать стоимость позиции
        public decimal CalculateCost()
        {
            return Price * Quantity;
        }
    }
}