using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore
{
    public class Sale
    {
        public Seller Seller { get; private set; }
        public DateTime SaleDate { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool IsCompleted { get; private set; }

        public Sale(Seller seller)
        {
            Seller = seller ?? throw new ArgumentNullException(nameof(seller));
            SaleDate = DateTime.Now;
            Items = new List<SaleItem>();
            IsCompleted = false;
        }

        public void AddItem(SaleItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        public void CompleteSale()
        {
            if (IsCompleted) throw new InvalidOperationException("Продажа уже завершена");
            if (!Items.Any()) throw new InvalidOperationException("Продажа пуста");

            foreach (var item in Items)
            {
                if (item.Product.StockQuantity < item.Quantity)
                    throw new InvalidOperationException($"Недостаточно товара '{item.Product.Name}' на складе");
            }

            foreach (var item in Items) item.Product.RemoveStock(item.Quantity);
            IsCompleted = true;
        }

        public decimal GetTotalAmount() => Items.Sum(i => i.GetTotalCost());
    }
}