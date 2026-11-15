using System;

namespace GroceryStore
{
    public abstract class User
    {
        public string Login { get; private set; }
        public string FullName { get; private set; }

        protected User(string login, string fullName)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentException("Логин не может быть пустым");
            Login = login;
            FullName = fullName;
        }
    }

    public class Manager : User
    {
        public string Department { get; private set; }
        public Manager(string login, string fullName, string department) : base(login, fullName)
        { Department = department; }
    }

    public class Seller : User
    {
        public int ShiftNumber { get; private set; }
        public Seller(string login, string fullName, int shiftNumber) : base(login, fullName)
        { ShiftNumber = shiftNumber; }
    }
}