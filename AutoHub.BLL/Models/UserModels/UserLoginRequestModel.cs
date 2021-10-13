namespace AutoHub.BLL.Models.UserModels
{
    public class UserLoginRequestModel
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}