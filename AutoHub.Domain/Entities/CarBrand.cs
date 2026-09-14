namespace AutoHub.Domain.Entities;

public class CarBrand
{
    public int CarBrandId { get; set; }

    public string CarBrandName { get; set; }

    public virtual IEnumerable<Car> Cars { get; set; } = new List<Car>();
}
