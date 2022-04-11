namespace AutoHub.API.Models.UserModels;

public class UserLoginResponse
{
    public string FullName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string Token { get; set; }
}
