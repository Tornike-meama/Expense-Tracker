using ExpenseTracker.DTO.Trasnaction;
using ExpenseTracker.Services.Transactions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : BaseController.BaseController
    {
        private readonly ITransactionServices _transactionServices;

        public TransactionController(ITransactionServices currencyServices)
        {
            _transactionServices = currencyServices;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllTransaction() => DataResponse(await _transactionServices.GetAllTransactionAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddTransaction([FromBody]AddTransactionModel data) => DataResponse(await _transactionServices.AddTransactionAsync(data, User.FindFirstValue(ClaimTypes.NameIdentifier)));

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("getAllForCurrentUser")]
        public async Task<IActionResult> GetAllTransactionsForCurrentUset() => DataResponse(await _transactionServices.GetAllTransactionsForCurrentUsetAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
    }
}
