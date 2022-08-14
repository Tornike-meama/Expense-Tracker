using ExpenseTracker.DTO.Currency;
using ExpenseTracker.Services.Currencies;
using ExpenseTracker.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("api/Currency")]
    [ApiController]
    public class CurrencyController : BaseController.BaseController
    {
        private readonly ICurrencyServices _currencyServices;

        public CurrencyController(ICurrencyServices currencyServices)
        {
            _currencyServices = currencyServices;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllCurrency() => DataResponse(await _currencyServices.GetAllCurrencyAsync());


        [HttpPost]
        [Route("add")]
        [RequestsValidator(Arguments = new object[] { typeof(AddCurrencyValidator) })]
        public async Task<IActionResult> AddCurrency([FromBody] AddCurrencyModel currency) => DataResponse(await _currencyServices.AddCurrencyAsync(currency));
    }
}
