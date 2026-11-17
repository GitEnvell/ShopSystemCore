using System;

namespace ShopSystemWeb
{
    // Класс документа "Акт расхождения", фиксирующего ошибки при приемке
    public class DiscrepancyAct
    {
        public string ActNumber { get; private set; } // Номер акта
        public DateTime CreationDate { get; private set; } // Дата создания
        public string Reason { get; private set; } // Причина расхождения
        public bool IsConfirmed { get; private set; } // Статус (подтвержден/отклонен)

        public DiscrepancyAct(string actNumber, string reason)
        {
            if (string.IsNullOrWhiteSpace(actNumber)) throw new ArgumentException("Номер акта обязателен");
            ActNumber = actNumber;
            CreationDate = DateTime.Now;
            Reason = reason;
            IsConfirmed = false; // По умолчанию не подтвержден
        }

        // Метод подтверждения акта (расхождение действительное)
        public void Confirm() { IsConfirmed = true; }

        // Метод отклонения акта (расхождения нет)
        public void Reject() { IsConfirmed = false; }
    }
}