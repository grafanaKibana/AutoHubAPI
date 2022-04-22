namespace AutoHub.API.Models.CarModelModels;

public record CarModelUpdateRequest
{
    public string CarModelName { get; init; }
}