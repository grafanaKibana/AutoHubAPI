namespace AutoHub.API.Models.CarColorModels;

public record CarColorCreateRequest
{
    /// <example>Yellow</example>
    public string CarColorName { get; init; }
}
