namespace AutoHub.BusinessLogic.DTOs.UserDTOs;

public record UserLoginResponseDTO
{
    public string FullName { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }
}
