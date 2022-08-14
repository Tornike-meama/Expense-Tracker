using ExpenseTracker.DTO.Trasnaction;
using FluentValidation;

namespace ExpenseTracker.Validators.Transaction
{
    public class AddTransactionvalidator : AbstractValidator<AddTransactionModel>
    {
        public AddTransactionvalidator()
        {
            RuleFor(transaction => transaction.Amount).NotNull().NotEmpty();
            RuleFor(transaction => transaction.IsIncome).NotNull().NotEmpty();
            RuleFor(transaction => transaction.CurrencyId).NotNull().NotEmpty();
            RuleFor(transaction => transaction.TypeId).NotNull().NotEmpty();
        }
    }
}
