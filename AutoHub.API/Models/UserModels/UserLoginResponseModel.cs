namespace AutoHub.API.Models.UserModels
{
    public class UserLoginResponseModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}