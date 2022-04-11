namespace AutoHub.API.Models.BidModels;

public class BidCreateRequest
{
    public int UserId { get; set; }

    public decimal BidValue { get; set; }
}
