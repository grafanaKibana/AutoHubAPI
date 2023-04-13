namespace AutoHub.API.Models.CarBrandModels;

public record CarBrandUpdateRequest
{
    /// <example>Audi</example>
    public string CarBrandName { get; init; }
}
