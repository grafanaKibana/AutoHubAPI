namespace AutoHub.BusinessLogic.DTOs.UserDTOs;

public record UserUpdateRequestDTO
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
}