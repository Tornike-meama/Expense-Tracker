using ExpenseTracker.BaseController;
using ExpenseTracker.DTO.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.IdentityServices
{
    public interface IIdentityServices
    {
        public Task<IComonResponse<string>> RegisterAsync(string email, string password, string name);
        public Task<IComonResponse<string>> LoginAsync(string email, string password);
        public Task<IComonResponse<List<GetUserModel>>> GetAllUsersAsync();
        public Task<IComonResponse<GetUserModel>> GetUserByIdAsync(string id);
    }
}
