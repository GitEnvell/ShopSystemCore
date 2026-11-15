using System;

namespace GroceryStore
{
    public class SaleItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public SaleItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Количество должно быть больше нуля");

            Quantity = quantity;
            Price = product.Price;
        }

        public decimal GetTotalCost() => Quantity * Price;
    }
}