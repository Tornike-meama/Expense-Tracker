using ExpenseTracker.DTO.Trasnaction;
using ExpenseTracker.Services.Transactions;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllCurrency() => DataResponse(await _transactionServices.GetAllTransactionAsync());

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCurrency([FromBody]AddTransactionModel data) => DataResponse(await _transactionServices.AddTransactionAsync(data));

    }
}
