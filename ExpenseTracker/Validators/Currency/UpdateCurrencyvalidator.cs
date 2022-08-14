using ExpenseTracker.DTO.Currency;
using FluentValidation;

namespace ExpenseTracker.Validators.Currency
{
    public class UpdateCurrencyvalidator : AbstractValidator<UpdateCurrencyModel>
    {
        public UpdateCurrencyvalidator()
        {
            //RuleFor(currency => currency.Id).NotNull().NotEmpty();
            //RuleFor(currency => currency.Name).NotNull().NotEmpty();
            //RuleFor(currency => currency.ShortName).NotNull().Length(0, 5);
            //RuleFor(currency => currency.Symbol).NotNull().NotEmpty();
        }
    }
}
