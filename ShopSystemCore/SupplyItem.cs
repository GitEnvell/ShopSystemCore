using System;

namespace GroceryStore
{
    public class SupplyItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public SupplyItem(Product product, int quantity, decimal unitPrice)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Количество должно быть больше нуля");
            if (unitPrice < 0) throw new ArgumentException("Цена не может быть отрицательной");

            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal GetTotalCost() => Quantity * UnitPrice;
    }
}