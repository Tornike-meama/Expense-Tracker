using ExpenseTracker.BaseController;
using ExpenseTracker.DTO.Currency;
using ExpenseTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Currencies
{
    public interface ICurrencyServices
    {
        public Task<IComonResponse<List<Currency>>> GetAllCurrencyAsync();
        public Task<IComonResponse<AddCurrencyModel>> AddCurrencyAsync(AddCurrencyModel data);

    }
}
