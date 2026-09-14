using AutoHub.Domain.Enums;

namespace AutoHub.Domain.Entities;

public class LotStatus
{
    public LotStatusEnum LotStatusId { get; set; }
    public string LotStatusName { get; set; }

    public virtual IEnumerable<Lot> Lots { get; } = new List<Lot>();
}
