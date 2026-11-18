namespace ShopSystemWeb
{
    // Сущность "Производитель/Поставщик" — контрагент, от которого приходят товары
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string INN { get; set; } // ИНН для идентификации

        public Manufacturer() { }

        public Manufacturer(string name, string inn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым");
            if (string.IsNullOrWhiteSpace(inn))
                throw new ArgumentException("ИНН не может быть пустым");

            Name = name;
            INN = inn;
        }
    }
}