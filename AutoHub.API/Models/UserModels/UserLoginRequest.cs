namespace AutoHub.API.Models.UserModels;

public record UserLoginRequest
{
    public string Username { get; init; }

    public string Password { get; init; }

    public bool RememberMe { get; init; }
}
