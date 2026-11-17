using System;

namespace ShopSystemWeb
{
    // Класс, описывающий компанию-производителя
    public class Manufacturer
    {
        public string Name { get; private set; } // Название компании
        public string INN { get; private set; } // ИНН производителя

        public Manufacturer(string name, string inn)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название не может быть пустым");
            Name = name;
            INN = inn;
        }
    }
}