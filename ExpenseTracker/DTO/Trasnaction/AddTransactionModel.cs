namespace ExpenseTracker.DTO.Trasnaction
{
    public class AddTransactionModel
    {
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public int TypeId { get; set; }
        public int CurrencyId { get; set; }
    }
}
