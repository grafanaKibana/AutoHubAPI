namespace AutoHub.API.Models.UserModels;

public record UserLoginRequest
{
    /// <example>admin</example>
    public string Username { get; init; }

    /// <example>adminPasSw0Rd!@#</example>
    public string Password { get; init; }

    /// <example>true</example>
    public bool RememberMe { get; init; }
}
