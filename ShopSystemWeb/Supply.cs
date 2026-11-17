using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSystemWeb
{
    // Класс документа "Поставка" (приход товара)
    public class Supply
    {
        public Manufacturer Manufacturer { get; private set; } // Кто поставил товар
        public DateTime SupplyDate { get; private set; } // Дата поставки
        public List<SupplyItem> Items { get; private set; } // Список позиций в поставке
        public bool IsAccepted { get; private set; } // Флаг: принята ли поставка
        public DiscrepancyAct DiscrepancyAct { get; private set; } // Ссылка на акт расхождения (если есть)

        public Supply(Manufacturer manufacturer)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            SupplyDate = DateTime.Now;
            Items = new List<SupplyItem>(); // Инициализируем пустой список позиций
            IsAccepted = false;
        }

        // Добавление товара в документ поставки
        public void AddItem(SupplyItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        // Создание акта расхождения (вызывается, если есть ошибки)
        public void CreateDiscrepancyAct(string actNumber, string reason)
        {
            if (IsAccepted) throw new InvalidOperationException("Нельзя создать акт для уже принятой поставки");
            DiscrepancyAct = new DiscrepancyAct(actNumber, reason);
        }

        // Проведение поставки (оприходование товара на склад)
        public void AcceptSupply()
        {
            if (IsAccepted) throw new InvalidOperationException("Поставка уже принята");
            if (!Items.Any()) throw new InvalidOperationException("Поставка пуста");

            // Проходим по всем позициям и увеличиваем остатки на складе
            foreach (var item in Items)
            {
                item.Product.AddStock(item.Quantity);
            }
            IsAccepted = true; // Помечаем поставку как принятую
        }

        // Подсчет общей суммы поставки
        public decimal GetTotalAmount() => Items.Sum(i => i.GetTotalCost());
    }
}