namespace AutoHub.API.Models.LotModels;

public record LotCreateRequest
{
    public int CreatorId { get; init; }
    public int CarId { get; init; }
    public int DurationInDays { get; init; }
}