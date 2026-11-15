using System;

namespace GroceryStore
{
    public class DiscrepancyAct
    {
        public string ActNumber { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Reason { get; private set; }
        public bool IsConfirmed { get; private set; } 

        public DiscrepancyAct(string actNumber, string reason)
        {
            if (string.IsNullOrWhiteSpace(actNumber)) throw new ArgumentException("Номер акта обязателен");
            ActNumber = actNumber;
            CreationDate = DateTime.Now;
            Reason = reason;
            IsConfirmed = false;
        }

        public void Confirm() { IsConfirmed = true; }
        public void Reject() { IsConfirmed = false; }
    }
}