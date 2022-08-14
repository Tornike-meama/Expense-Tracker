
using ExpenseTracker.DTO.TransactionType;
using ExpenseTracker.Services.TransactionTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("api/transactionType")]
    [ApiController]
    public class TransactionTypeController : BaseController.BaseController
    {
        private readonly ITransactionTypesServices _transactionTypesServices;
        public TransactionTypeController(ITransactionTypesServices transactionTypeServices)
        {
            _transactionTypesServices = transactionTypeServices;
        }


        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllTransactionType() => DataResponse(await _transactionTypesServices.GetAllTransactionTypeAsync());

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCurrency([FromBody] AddTransactionTypeModel data) => DataResponse(await _transactionTypesServices.AddTransactionTypeAsync(data));

    }
}
