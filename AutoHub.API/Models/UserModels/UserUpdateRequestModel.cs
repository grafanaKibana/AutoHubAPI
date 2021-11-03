namespace AutoHub.API.Models.UserModels
{
    public class UserUpdateRequestModel
    {
        public int UserRoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
    }
}