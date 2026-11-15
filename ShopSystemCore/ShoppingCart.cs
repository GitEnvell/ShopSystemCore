using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore
{
    public class ShoppingCart
    {
        private Dictionary<Product, int> _items = new Dictionary<Product, int>();

        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Количество должно быть больше нуля");

            if (_items.ContainsKey(product)) _items[product] += quantity;
            else _items[product] = quantity;
        }

        public IReadOnlyDictionary<Product, int> GetItems() => _items;
        public void Clear() => _items.Clear();
    }
}