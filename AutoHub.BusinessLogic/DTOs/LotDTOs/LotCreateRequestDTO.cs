namespace AutoHub.BusinessLogic.DTOs.LotDTOs;

public record LotCreateRequestDTO
{
    public int CreatorId { get; set; }
    public int CarId { get; set; }
    public int DurationInDays { get; set; }
}