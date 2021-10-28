namespace AutoHub.API.Models.BidModels
{
    public class BidCreateRequestModel
    {
        public int UserId { get; set; }

        public int LotId { get; set; }

        public decimal BidValue { get; set; }
    }
}