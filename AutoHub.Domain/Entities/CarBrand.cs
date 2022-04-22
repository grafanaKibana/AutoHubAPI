namespace AutoHub.Domain.Entities;

public class CarBrand
{
    public CarBrand()
    {
        Cars = new List<Car>();
    }

    public int CarBrandId { get; set; }

    public string CarBrandName { get; set; }

    public virtual IEnumerable<Car> Cars { get; set; }
}