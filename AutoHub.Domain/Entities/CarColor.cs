namespace AutoHub.Domain.Entities;

public class CarColor
{
    public int CarColorId { get; set; }

    public string CarColorName { get; set; }

    public virtual IEnumerable<Car> Cars { get; set; } = new List<Car>();
}
