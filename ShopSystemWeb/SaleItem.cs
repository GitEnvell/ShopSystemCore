using System;

namespace ShopSystemWeb
{
    // Класс, описывающий одну строку (позицию) в чеке продажи
    public class SaleItem
    {
        public Product Product { get; private set; } // Ссылка на товар
        public int Quantity { get; private set; } // Количество проданного товара
        public decimal Price { get; private set; } // Цена на момент продажи

        public SaleItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Количество должно быть больше нуля");

            Quantity = quantity;
            Price = product.Price; // Фиксируем цену товара на момент продажи
        }

        // Подсчет стоимости позиции (Кол-во * Цена)
        public decimal GetTotalCost() => Quantity * Price;
    }
}