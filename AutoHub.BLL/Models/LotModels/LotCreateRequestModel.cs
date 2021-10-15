namespace AutoHub.BLL.Models.LotModels
{
    public class LotCreateRequestModel
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int DurationInDays { get; set; }
    }
}