using System;
using System.Collections.Generic;

namespace AutoHub.API.Models.UserModels;

public class UserResponse
{
    public int UserId { get; init; }

    public string Username { get; init; }

    public IList<string> UserRoles { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public DateTime RegistrationTime { get; init; }
}
