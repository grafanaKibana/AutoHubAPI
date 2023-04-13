namespace AutoHub.API.Models.LotModels;

public class LotUpdateRequest
{
    /// <example>3</example>
    public int LotStatusId { get; init; }
    
    /// <example>2</example>
    public int? WinnerId { get; init; }
    
    /// <example>7</example>
    public int DurationInDays { get; init; }
}
