namespace AutoHub.API.Models.LotModels
{
    public class LotUpdateRequestModel
    {
        public int LotStatusId { get; set; }
        public int? WinnerId { get; set; }
        public int DurationInDays { get; set; }
    }
}