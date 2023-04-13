namespace AutoHub.API.Models.CarModelModels;

public record CarModelCreateRequest
{
    /// <example>e-tron GT</example>
    public string CarModelName { get; init; }
}
