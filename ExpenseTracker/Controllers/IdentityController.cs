using ExpenseTracker.DTO.Identity;
using ExpenseTracker.DTO.User;
using ExpenseTracker.Services.IdentityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : BaseController.BaseController
    {
        private readonly IIdentityServices _identityServices;

        public IdentityController(IIdentityServices identityServices)
        {
            _identityServices = identityServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request) => DataResponse(await _identityServices.RegisterAsync(request.Email, request.Password, request.Name));

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) => DataResponse(await _identityServices.LoginAsync(request.Email, request.Password));

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("updateCurrentUser")]
        public async Task<IActionResult> UpdateCurrentUser([FromForm] UpdateCurrentUserModel data) => DataResponse(await _identityServices.UpdatedCurrentuserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), data));

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("getUserData")]
        public async Task<IActionResult> GetUserData() => DataResponse(await _identityServices.GetuserDataAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUser() => DataResponse(await _identityServices.GetAllUsersAsync());

        [HttpGet]
        [Route("getUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id) => DataResponse(await _identityServices.GetUserByIdAsync(id));

    }
}
