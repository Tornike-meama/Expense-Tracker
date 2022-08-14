namespace ExpenseTracker.DTO.Trasnaction
{
    public class UpdateTransactionModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
    }
}
