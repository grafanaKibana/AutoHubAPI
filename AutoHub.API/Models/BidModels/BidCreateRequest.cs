namespace AutoHub.API.Models.BidModels;

public record BidCreateRequest
{
    /// <example>1</example>
    public int UserId { get; init; }

    /// <example>94250</example>
    public decimal BidValue { get; init; }
}
