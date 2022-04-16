namespace AutoHub.BusinessLogic.DTOs.BidDTOs;

public record BidCreateRequestDTO
{
    public int UserId { get; set; }

    public decimal BidValue { get; set; }
}
