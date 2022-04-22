namespace AutoHub.API.Models.UserModels;

public class UserRegisterRequest
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Username { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public string Password { get; init; }
}