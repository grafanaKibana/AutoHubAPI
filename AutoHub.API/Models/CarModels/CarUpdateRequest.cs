namespace AutoHub.API.Models.CarModels;

public record CarUpdateRequest : CarBaseRequest
{
    /// <example>96750</example>
    public decimal CostPrice { get; init; }
    
    /// <example>3</example>
    public int CarStatusId { get; init; }
}
