namespace AutoHub.BLL.DTOs.LotDTOs
{
    public class LotUpdateRequestDTO
    {
        public int LotId { get; set; }
        public int LotStatusId { get; set; }
        public int CreatorId { get; set; }
        public int CarId { get; set; }
        public int? WinnerId { get; set; }
        public int DurationInDays { get; set; }
    }
}