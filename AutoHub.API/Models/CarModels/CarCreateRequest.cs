namespace AutoHub.API.Models.CarModels;

public record CarCreateRequest : CarBaseRequest
{
    public decimal CostPrice { get; init; }
}