namespace ShopSystemWeb
{
    // Сущность "Акт расхождения" — документ при несоответствии поставки
    // Реализует конечный автомат: Создан → Подтверждён/Отклонён
    public class DiscrepancyAct
    {
        public int Id { get; set; }
        public string ActNumber { get; set; }
        public string Reason { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsConfirmed { get; set; }

        public DiscrepancyAct() { }

        public DiscrepancyAct(string actNumber, string reason)
        {
            if (string.IsNullOrWhiteSpace(actNumber))
                throw new ArgumentException("Номер акта не может быть пустым");
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Причина не может быть пустой");

            ActNumber = actNumber;
            Reason = reason;
            CreationDate = DateTime.Now;
            IsConfirmed = false;
        }

        // Подтвердить акт (руководителем)
        public void Confirm()
        {
            IsConfirmed = true;
        }

        // Отклонить акт
        public void Reject()
        {
            IsConfirmed = false;
        }
    }
}