using System;

namespace ShopSystemWeb
{
    // Класс, описывающий одну строку (позицию) в документе поставки
    public class SupplyItem
    {
        public Product Product { get; private set; } // Ссылка на товар
        public int Quantity { get; private set; } // Количество товара
        public decimal UnitPrice { get; private set; } // Закупочная цена за единицу

        public SupplyItem(Product product, int quantity, decimal unitPrice)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Количество должно быть больше нуля");
            if (unitPrice < 0) throw new ArgumentException("Цена не может быть отрицательной");

            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        // Подсчет стоимости данной позиции (Кол-во * Цена)
        public decimal GetTotalCost() => Quantity * UnitPrice;
    }
}