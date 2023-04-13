namespace AutoHub.API.Models.UserModels;

public class UserRegisterRequest
{
    /// <example>Steve</example>
    public string FirstName { get; init; }

    /// <example>Jobs</example>
    public string LastName { get; init; }

    /// <example>sjobs</example>
    public string Username { get; init; }

    /// <example>steve.jobs@icloud.com</example>
    public string Email { get; init; }

    /// <example>+380685553535</example>
    public string PhoneNumber { get; init; }

    /// <example>PasSw0Rd!@#</example>
    public string Password { get; init; }
}
