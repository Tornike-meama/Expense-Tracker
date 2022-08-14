using ExpenseTracker.BaseController;
using ExpenseTracker.DTO.TransactionType;
using ExpenseTracker.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.TransactionTypes
{
    public interface ITransactionTypesServices
    {
        public Task<IComonResponse<List<TransactionType>>> GetAllTransactionTypeAsync();
        public Task<IComonResponse<TransactionType>> AddTransactionTypeAsync(AddTransactionTypeModel data);
    }
}
