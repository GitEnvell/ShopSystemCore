using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSystemWeb
{
    // Вспомогательный класс корзины (для промежуточного хранения товаров)
    public class ShoppingCart
    {
        // Словарь: Товар -> Количество
        private Dictionary<Product, int> _items = new Dictionary<Product, int>();

        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Количество должно быть больше нуля");

            // Если товар уже есть в корзине - увеличиваем количество, иначе добавляем
            if (_items.ContainsKey(product)) _items[product] += quantity;
            else _items[product] = quantity;
        }

        // Получить все товары в корзине
        public IReadOnlyDictionary<Product, int> GetItems() => _items;

        // Очистить корзину
        public void Clear() => _items.Clear();
    }
}