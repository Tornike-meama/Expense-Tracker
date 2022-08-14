using ExpenseTracker.DTO.Currency;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseTracker.Validators
{
    public class ResponseHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public ResponseHeaderAttribute(string name, string value) =>
            (_name, _value) = (name, value);

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var objResult = (AddCurrencyModel)context.Result;


            base.OnResultExecuting(context);
        }
    }
}
