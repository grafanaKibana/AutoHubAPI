namespace AutoHub.API.Models.UserModels
{
    public class UserLoginRequestModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}