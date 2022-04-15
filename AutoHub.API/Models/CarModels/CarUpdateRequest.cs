namespace AutoHub.API.Models.CarModels;

public record CarUpdateRequest : CarBaseRequest
{
    public decimal CostPrice { get; init; }
    public int CarStatusId { get; init; }
}
