using System;

namespace ShopSystemWeb
{
    // Класс, описывающий товар в магазине
    public class Product
    {
        // Свойства товара
        public string Name { get; private set; } // Название товара
        public decimal Price { get; private set; } // Цена за единицу
        public int StockQuantity { get; private set; } // Количество на складе

        // Конструктор с проверкой входных данных
        public Product(string name, decimal price, int initialStock)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название не может быть пустым");
            if (price < 0) throw new ArgumentException("Цена не может быть отрицательной");
            if (initialStock < 0) throw new ArgumentException("Остаток не может быть отрицательным");

            Name = name;
            Price = price;
            StockQuantity = initialStock;
        }

        // Метод для добавления товара на склад (при поставке)
        public void AddStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Количество для добавления должно быть больше нуля");
            StockQuantity += quantity;
        }

        // Метод для списания товара со склада (при продаже)
        public void RemoveStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Количество для списания должно быть больше нуля");
            if (quantity > StockQuantity) throw new InvalidOperationException($"Недостаточно товара '{Name}' на складе");
            StockQuantity -= quantity;
        }
    }
}