namespace AutoHub.API.Models.UserModels;

public class UserUpdateRequest
{
    /// <example>Steve</example>
    public string FirstName { get; init; }

    /// <example>Jobs</example>
    public string LastName { get; init; }

    /// <example>steve.jobs@icloud.com</example>
    public string Email { get; init; }

    /// <example>+380685553535</example>
    public string PhoneNumber { get; init; }
}
