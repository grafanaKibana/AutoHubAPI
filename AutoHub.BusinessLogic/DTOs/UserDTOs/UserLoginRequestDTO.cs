namespace AutoHub.BusinessLogic.DTOs.UserDTOs;

public record UserLoginRequestDTO
{
    public string Username { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; }
}