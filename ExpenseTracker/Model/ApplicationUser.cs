using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Model
{
    public class ApplicationUser : IdentityUser
    {
        public int CardId { get; set; }
        public string AvatarUrl { get; set; }
    }
}
