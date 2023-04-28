namespace AutoHub.API.Models.CarModels;

public record CarBaseRequest
{
    /// <example>Audi</example>
    public string CarBrand { get; init; }
    
    /// <example>e-tron GT</example>
    public string CarModel { get; init; }
    
    /// <example>Yellow</example>
    public string CarColor { get; init; }
    
    public string ImgUrl { get; init; }
    
    /// <example>The Audi e-tron GT is a sleek and powerful electric sports car that offers impressive performance and advanced technology</example>
    public string Description { get; init; }
    
    /// <example>2021</example>
    public int Year { get; init; }
    
    /// <example>2T1KR32EX4C175599</example>
    public string VIN { get; init; }
    
    /// <example>56752</example>
    public int Mileage { get; init; }
    
    /// <example>100500</example>
    public decimal SellingPrice { get; init; }
}
