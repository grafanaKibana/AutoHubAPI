namespace AutoHub.BLL.DTOs.BidDTOs
{
    public class BidCreateRequestDTO
    {
        public int UserId { get; set; }

        public int LotId { get; set; }

        public decimal BidValue { get; set; }
    }
}