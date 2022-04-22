namespace AutoHub.API.Models.LotModels;

public class LotUpdateRequest
{
    public int LotStatusId { get; init; }
    public int? WinnerId { get; init; }
    public int DurationInDays { get; init; }
}