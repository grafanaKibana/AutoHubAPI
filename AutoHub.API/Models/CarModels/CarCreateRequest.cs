namespace AutoHub.API.Models.CarModels;

public record CarCreateRequest : CarBaseRequest
{
    /// <example>96750</example>
    public decimal CostPrice { get; init; }
}
