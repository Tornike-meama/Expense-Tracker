using System.Collections.Generic;

namespace ExpenseTracker.DTO.User
{
    public class GetUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public List<string> UserRoles { get; set; }

    }
}
