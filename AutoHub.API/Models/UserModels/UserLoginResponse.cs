namespace AutoHub.API.Models.UserModels;

public class UserLoginResponse
{
    public string FullName { get; init; }

    public string Email { get; init; }

    public string UserName { get; init; }

    public string Token { get; init; }
}
