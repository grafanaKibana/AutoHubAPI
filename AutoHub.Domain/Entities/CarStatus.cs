using AutoHub.Domain.Enums;

namespace AutoHub.Domain.Entities;

public class CarStatus
{
    public CarStatusEnum CarStatusId { get; set; }
    public string CarStatusName { get; set; }

    public virtual IEnumerable<Car> Cars { get; } = new List<Car>();
}
