using System;

namespace ShopSystemWeb
{
    // Базовый класс пользователя системы (реализует наследование)
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }

        public User() { } // Пустой конструктор для EF Core (уже был)

        public User(string login, string fullName)
        {
            Login = login;
            FullName = fullName;
        }
    }

    // Класс Менеджера (наследуется от User)
    public class Manager : User
    {
        public string Department { get; set; } // Отдел, в котором работает менеджер

        public Manager() { } // ДОБАВЛЕНО: Пустой конструктор для EF Core

        public Manager(string login, string fullName, string department) : base(login, fullName)
        {
            Department = department;
        }
    }

    // Класс Продавца (наследуется от User)
    public class Seller : User
    {
        public int ShiftNumber { get; set; } // Номер смены продавца

        public Seller() { } // ДОБАВЛЕНО: Пустой конструктор для EF Core

        public Seller(string login, string fullName, int shiftNumber) : base(login, fullName)
        {
            ShiftNumber = shiftNumber;
        }
    }
}