using AutoHub.Domain.Enums;

namespace AutoHub.Domain.Entities;

public class Car
{
    public int CarId { get; set; }

    public int CarBrandId { get; set; }
    public virtual CarBrand CarBrand { get; set; }

    public int CarModelId { get; set; }
    public virtual CarModel CarModel { get; set; }

    public CarStatusEnum CarStatusId { get; set; }
    public virtual CarStatus CarStatus { get; set; }

    public int CarColorId { get; set; }
    public virtual CarColor CarColor { get; set; }

    public string ImgUrl { get; set; }

    public string Description { get; set; }

    public int Year { get; set; }

    public string VIN { get; set; }

    public int Mileage { get; set; }

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }
}