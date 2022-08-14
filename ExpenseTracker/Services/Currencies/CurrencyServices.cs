using ExpenseTracker.BaseController;
using ExpenseTracker.DbContexts;
using ExpenseTracker.DTO.Currency;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Currencies
{
    public class CurrencyServices : ICurrencyServices
    {

        private readonly MyDbContext _dbContext;

        public CurrencyServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IComonResponse<List<Currency>>>GetAllCurrencyAsync()
        {
            try
            {
                return new ComonResponse<List<Currency>>(await _dbContext.Currencies.ToListAsync());
            }
            catch (Exception ex)
            {
                return new BadRequest<List<Currency>>(ex.Message);
            }
        }
        public async Task<IComonResponse<AddCurrencyModel>> AddCurrencyAsync(AddCurrencyModel data)
        {
            try
            {
                if (data == null)
                {
                    return new BadRequest<AddCurrencyModel>("Data is null");
                }

                if(await _dbContext.Currencies.AnyAsync(o => o.Name.ToLower() == data.Name.ToLower()))
                {
                    return new BadRequest<AddCurrencyModel>("name must be uniqe");
                }

                var currency = new Currency()
                {
                    Name = data.Name,
                    ShortName = data.ShortName,
                    Symbol = data.Symbol,
                };

                _dbContext.Currencies.Add(currency);
                await _dbContext.SaveChangesAsync();
                return new ComonResponse<AddCurrencyModel>(data);
            }
            catch (Exception ex)
            {
                return new BadRequest<AddCurrencyModel>(ex.Message);
            }
        }
    }
}
