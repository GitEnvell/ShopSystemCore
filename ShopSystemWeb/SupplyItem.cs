namespace ShopSystemWeb
{
    // Позиция поставки — одна строка в документе прихода
    public class SupplyItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Закупочная цена

        public SupplyItem() { }

        public SupplyItem(Product product, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным");
            if (unitPrice <= 0)
                throw new ArgumentException("Цена должна быть положительной");

            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal CalculateCost()
        {
            return UnitPrice * Quantity;
        }
    }
}