namespace AutoHub.API.Models.LotModels;

public record LotCreateRequest
{
    /// <example>1</example>
    public int CreatorId { get; init; }
    
    /// <example>1</example>
    public int CarId { get; init; }
    
    /// <example>5</example>
    public int DurationInDays { get; init; }
}
