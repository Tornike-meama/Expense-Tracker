using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        [ForeignKey("TransactionType")]
        public int TypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
