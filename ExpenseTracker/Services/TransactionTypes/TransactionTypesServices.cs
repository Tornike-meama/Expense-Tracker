using ExpenseTracker.BaseController;
using ExpenseTracker.DbContexts;
using ExpenseTracker.DTO.TransactionType;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.TransactionTypes
{
    public class TransactionTypesServices : ITransactionTypesServices
    {
        private readonly MyDbContext _dbContext;

        public TransactionTypesServices(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IComonResponse<TransactionType>> AddTransactionTypeAsync(AddTransactionTypeModel data)
        {
            try
            {
                if (data == null) return new BadRequest<TransactionType>("Data is null");


                var transactionType = new TransactionType()
                {
                    Name = data.Name,
                };

                _dbContext.TransactionTypes.Add(transactionType);
                await _dbContext.SaveChangesAsync();
                return new ComonResponse<TransactionType>(transactionType);
            }
            catch (Exception ex)
            {
                return new BadRequest<TransactionType>(ex.Message);
            }
        }

        public async Task<IComonResponse<List<TransactionType>>> GetAllTransactionTypeAsync()
        {
            try
            {
                return new ComonResponse<List<TransactionType>>(await _dbContext.TransactionTypes.ToListAsync());
            }
            catch (Exception ex)
            {
                return new BadRequest<List<TransactionType>>(ex.Message);
            }
        }
    }
}
