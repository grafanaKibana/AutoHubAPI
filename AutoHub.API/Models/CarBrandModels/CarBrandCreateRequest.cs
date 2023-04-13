namespace AutoHub.API.Models.CarBrandModels;

public record CarBrandCreateRequest
{
    /// <example>Audi</example>
    public string CarBrandName { get; init; }
}
