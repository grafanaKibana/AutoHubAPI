namespace AutoHub.API.Models.LotModels
{
    public class LotCreateRequestModel
    {
        public int CreatorId { get; set; }
        public int CarId { get; set; }
        public int DurationInDays { get; set; }
    }
}