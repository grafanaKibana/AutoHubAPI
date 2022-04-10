namespace AutoHub.BLL.DTOs.UserDTOs
{
    public class UserLoginRequestDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}