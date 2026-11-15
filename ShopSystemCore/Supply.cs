using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore
{
    public class Supply
    {
        public Manufacturer Manufacturer { get; private set; }
        public DateTime SupplyDate { get; private set; }
        public List<SupplyItem> Items { get; private set; }
        public bool IsAccepted { get; private set; }

        
        public DiscrepancyAct DiscrepancyAct { get; private set; }

        public Supply(Manufacturer manufacturer)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            SupplyDate = DateTime.Now;
            Items = new List<SupplyItem>();
            IsAccepted = false;
        }

        public void AddItem(SupplyItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        
        public void CreateDiscrepancyAct(string actNumber, string reason)
        {
            if (IsAccepted)
                throw new InvalidOperationException("Нельзя создать акт для уже принятой поставки");

            DiscrepancyAct = new DiscrepancyAct(actNumber, reason);
        }

        public void AcceptSupply()
        {
            if (IsAccepted) throw new InvalidOperationException("Поставка уже принята");
            if (!Items.Any()) throw new InvalidOperationException("Поставка пуста");

            
            foreach (var item in Items)
            {
                item.Product.AddStock(item.Quantity);
            }
            IsAccepted = true;
        }

        public decimal GetTotalAmount() => Items.Sum(i => i.GetTotalCost());
    }
}