using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSystemWeb
{
    // Класс документа "Продажа" (расход товара)
    public class Sale
    {
        public Seller Seller { get; private set; } // Кто оформил продажу
        public DateTime SaleDate { get; private set; } // Дата продажи
        public List<SaleItem> Items { get; private set; } // Список позиций в чеке
        public bool IsCompleted { get; private set; } // Флаг: завершена ли продажа

        public Sale(Seller seller)
        {
            Seller = seller ?? throw new ArgumentNullException(nameof(seller));
            SaleDate = DateTime.Now;
            Items = new List<SaleItem>();
            IsCompleted = false;
        }

        // Добавление товара в чек
        public void AddItem(SaleItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        // Завершение продажи (списание товара и фиксация)
        public void CompleteSale()
        {
            if (IsCompleted) throw new InvalidOperationException("Продажа уже завершена");
            if (!Items.Any()) throw new InvalidOperationException("Продажа пуста");

            // Сначала проверяем, хватает ли ВСЕГО товара на складе
            foreach (var item in Items)
            {
                if (item.Product.StockQuantity < item.Quantity)
                    throw new InvalidOperationException($"Недостаточно товара '{item.Product.Name}' на складе");
            }

            // Если всё в порядке, списываем остатки
            foreach (var item in Items)
            {
                item.Product.RemoveStock(item.Quantity);
            }
            IsCompleted = true; // Помечаем продажу как завершенную
        }

        // Подсчет итоговой суммы чека
        public decimal GetTotalAmount() => Items.Sum(i => i.GetTotalCost());
    }
}