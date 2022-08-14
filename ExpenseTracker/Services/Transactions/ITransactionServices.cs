using ExpenseTracker.BaseController;
using ExpenseTracker.DTO.Trasnaction;
using ExpenseTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Transactions
{
    public interface ITransactionServices
    {
        public Task<IComonResponse<List<Transaction>>> GetAllTransactionAsync();
        public Task<IComonResponse<AddTransactionModel>> AddTransactionAsync(AddTransactionModel data);
    }
}
