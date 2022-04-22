using AutoHub.Domain.Enums;

namespace AutoHub.Domain.Entities;

public class CarStatus
{
    public CarStatus()
    {
        Cars = new List<Car>();
    }

    public CarStatusEnum CarStatusId { get; set; }
    public string CarStatusName { get; set; }

    public virtual IEnumerable<Car> Cars { get; }
}