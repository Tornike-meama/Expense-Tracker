using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.DTO.User
{
    public class UpdateCurrentUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool DeleteAvatar { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
