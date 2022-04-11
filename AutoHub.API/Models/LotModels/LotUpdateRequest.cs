namespace AutoHub.API.Models.LotModels;

public class LotUpdateRequest
{
    public int LotStatusId { get; set; }
    public int? WinnerId { get; set; }
    public int DurationInDays { get; set; }
}
