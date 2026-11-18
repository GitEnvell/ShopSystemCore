namespace ShopSystemWeb
{
    // Сущность "Поставка" — документ прихода товара от производителя
    public class Supply
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SupplyItem> Items { get; set; } = new List<SupplyItem>();

        public Supply() { }

        public Supply(Manufacturer manufacturer)
        {
            Date = DateTime.Now;
            Items = new List<SupplyItem>();
        }

        public void AddItem(SupplyItem item)
        {
            Items.Add(item);
        }

        // Принять поставку: увеличить остатки товаров на складе
        public void AcceptSupply()
        {
            foreach (var item in Items)
            {
                item.Product.StockQuantity += item.Quantity;
            }
            TotalAmount = Items.Sum(i => i.CalculateCost());
        }
    }
}