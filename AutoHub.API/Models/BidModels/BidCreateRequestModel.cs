namespace AutoHub.API.Models.BidModels
{
    public class BidCreateRequestModel
    {
        public int UserId { get; set; }

        public decimal BidValue { get; set; }
    }
}