using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.BaseController
{
    public class BaseController : ControllerBase
    {
        public BaseController() {}

        public IActionResult DataResponse<T>(IComonResponse<T> response)
        {
            if(response is BadRequest<T>)
            {
                return BadRequest(response);
            }
            if(response is NotFound<T>)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
