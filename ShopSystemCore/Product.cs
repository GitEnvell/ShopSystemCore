using System;

namespace GroceryStore
{
    public class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        public Product(string name, decimal price, int initialStock)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название не может быть пустым");
            if (price < 0) throw new ArgumentException("Цена не может быть отрицательной");
            if (initialStock < 0) throw new ArgumentException("Остаток не может быть отрицательным");

            Name = name;
            Price = price;
            StockQuantity = initialStock;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Количество для добавления должно быть больше нуля");
            StockQuantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Количество для списания должно быть больше нуля");
            if (quantity > StockQuantity) throw new InvalidOperationException($"Недостаточно товара '{Name}' на складе");
            StockQuantity -= quantity;
        }
    }
}