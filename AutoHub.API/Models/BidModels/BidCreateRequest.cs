namespace AutoHub.API.Models.BidModels;

public record BidCreateRequest
{
    public int UserId { get; init; }

    public decimal BidValue { get; init; }
}