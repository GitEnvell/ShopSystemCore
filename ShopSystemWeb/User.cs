using System;

namespace ShopSystemWeb
{
    // Абстрактный базовый класс для всех пользователей системы
    public abstract class User
    {
        public string Login { get; private set; } // Логин пользователя
        public string FullName { get; private set; } // ФИО пользователя

        protected User(string login, string fullName)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentException("Логин не может быть пустым");
            Login = login;
            FullName = fullName;
        }
    }

    // Класс Менеджера (наследуется от User)
    public class Manager : User
    {
        public string Department { get; private set; } // Отдел, в котором работает менеджер
        public Manager(string login, string fullName, string department) : base(login, fullName)
        { Department = department; }
    }

    // Класс Продавца (наследуется от User)
    public class Seller : User
    {
        public int ShiftNumber { get; private set; } // Номер смены продавца
        public Seller(string login, string fullName, int shiftNumber) : base(login, fullName)
        { ShiftNumber = shiftNumber; }
    }
}