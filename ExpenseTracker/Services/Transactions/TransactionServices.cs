using ExpenseTracker.BaseController;
using ExpenseTracker.DbContexts;
using ExpenseTracker.DTO.Trasnaction;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Transactions
{
    public class TransactionServices : ITransactionServices
    {
        private readonly MyDbContext _dbContext;

        public TransactionServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IComonResponse<AddTransactionModel>> AddTransactionAsync(AddTransactionModel data, string userId)
        {
            try
            {
                if(await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == userId) == null)
                {
                    return new NotFound<AddTransactionModel>("user not found");
                }

                if (data == null)
                {
                    return new BadRequest<AddTransactionModel>("Data is null");
                }

                if(!await _dbContext.TransactionTypes.AnyAsync(o => o.Id == data.TypeId))
                {
                    return new BadRequest<AddTransactionModel>("type id is not correct. this type is not in DB");
                }

                if (!await _dbContext.Currencies.AnyAsync(o => o.Id == data.CurrencyId))
                {
                    return new BadRequest<AddTransactionModel>("currency id is not correct. this currency is not in DB");
                }

                var transaction = new Transaction()
                {
                    Amount = data.Amount,
                    IsIncome = data.IsIncome,
                    TypeId = data.TypeId,
                    CurrencyId = data.CurrencyId,
                    UserId = userId
                };

                _dbContext.Transactions.Add(transaction);
                await _dbContext.SaveChangesAsync();
                return new ComonResponse<AddTransactionModel>(data);
            }
            catch (Exception ex)
            {
                return new BadRequest<AddTransactionModel>(ex.Message);
            }
        }

        public async Task<IComonResponse<UpdateTransactionModel>> UpdateTransactionAsync(UpdateTransactionModel data)
        {
            try
            {
                if (data == null)
                {
                    return new BadRequest<UpdateTransactionModel>("Data is null");
                }

                var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(o => o.Id == data.Id);

                if (transaction == null)
                {
                    return new NotFound<UpdateTransactionModel>("Transaction not found");
                }

                if (!await _dbContext.TransactionTypes.AllAsync(o => o.Id == data.TypeId))
                {
                    return new BadRequest<UpdateTransactionModel>("type id is not correct. this type is not in DB");
                }

                if (!await _dbContext.Currencies.AllAsync(o => o.Id == data.CurrencyId))
                {
                    return new BadRequest<UpdateTransactionModel>("currency id is not correct. this currency is not in DB");
                }

                transaction.Amount = data.Amount;
                transaction.IsIncome = data.IsIncome;
                transaction.TypeId = data.TypeId;
                transaction.CurrencyId = data.CurrencyId;

                await _dbContext.SaveChangesAsync();
                return new ComonResponse<UpdateTransactionModel>(data);
            }
            catch (Exception ex)
            {
                return new BadRequest<UpdateTransactionModel>(ex.Message);
            }
        }

        public async Task<IComonResponse<List<Transaction>>> GetAllTransactionAsync()
        {
            try
            {
                return new ComonResponse<List<Transaction>>(await _dbContext.Transactions.Include(o => o.TransactionType).Include(o => o.Currency).ToListAsync());
            }
            catch (Exception ex)
            {
                return new BadRequest<List<Transaction>>(ex.Message);
            }
        }

        public async Task<IComonResponse<List<Transaction>>> GetAllTransactionsForCurrentUsetAsync(string userid)
        {
            try
            {
                var user =  await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == userid);

                if(user == null)
                {
                    return new BadRequest<List<Transaction>>("User not found");
                }

                return new ComonResponse<List<Transaction>>(await _dbContext.Transactions.Where(o => o.UserId == userid).Include(o => o.TransactionType).Include(o => o.Currency).ToListAsync());
            }
            catch (Exception ex)
            {
                return new BadRequest<List<Transaction>>(ex.Message);
            }
        }
    }
}
