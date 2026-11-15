using System;

namespace GroceryStore
{
    public class Manufacturer
    {
        public string Name { get; private set; }
        public string INN { get; private set; }

        public Manufacturer(string name, string inn)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название не может быть пустым");
            Name = name;
            INN = inn;
        }
    }
}