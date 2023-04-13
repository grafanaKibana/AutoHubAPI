namespace AutoHub.API.Models.CarModelModels;

public record CarModelUpdateRequest
{
    /// <example>e-tron GT</example>
    public string CarModelName { get; init; }
}
