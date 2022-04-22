namespace AutoHub.API.Models.CarModels;

public record CarBaseRequest
{
    public string CarBrand { get; init; }
    public string CarModel { get; init; }
    public string CarColor { get; init; }
    public string ImgUrl { get; init; }
    public string Description { get; init; }
    public int Year { get; init; }
    public string VIN { get; init; }
    public int Mileage { get; init; }
    public decimal SellingPrice { get; init; }
}