namespace AutoHub.BusinessLogic.DTOs.LotDTOs;

public record LotUpdateRequestDTO
{
    public int LotStatusId { get; set; }
    public int? WinnerId { get; set; }
    public int DurationInDays { get; set; }
}