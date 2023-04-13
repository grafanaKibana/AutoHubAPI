namespace AutoHub.API.Models.CarColorModels;

public record CarColorUpdateRequest
{
    /// <example>Yellow</example>
    public string CarColorName { get; init; }
}
