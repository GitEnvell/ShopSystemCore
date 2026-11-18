namespace ShopSystemWeb
{
    // Сущность "Продажа" — чек, содержащий список позиций
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();

        public Sale() { }

        public Sale(Seller seller)
        {
            Date = DateTime.Now;
            Items = new List<SaleItem>();
        }

        // Добавить позицию в чек
        public void AddItem(SaleItem item)
        {
            Items.Add(item);
        }

        // Посчитать общую сумму чека
        public decimal GetTotalAmount()
        {
            return Items.Sum(i => i.CalculateCost());
        }

        // Завершить продажу: проверить остатки и списать товар
        public void CompleteSale()
        {
            foreach (var item in Items)
            {
                if (item.Product.StockQuantity < item.Quantity)
                    throw new InvalidOperationException($"Недостаточно товара '{item.Product.Name}' на складе");
                item.Product.StockQuantity -= item.Quantity; // Списываем со склада
            }
            TotalAmount = GetTotalAmount();
        }
    }
}