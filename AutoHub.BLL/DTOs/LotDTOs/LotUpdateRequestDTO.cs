namespace AutoHub.BLL.DTOs.LotDTOs
{
    public class LotUpdateRequestDTO
    {
        public int LotStatusId { get; set; }
        public int? WinnerId { get; set; }
        public int DurationInDays { get; set; }
    }
}