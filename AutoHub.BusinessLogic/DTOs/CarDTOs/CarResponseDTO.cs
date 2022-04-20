namespace AutoHub.BusinessLogic.DTOs.CarDTOs;

public record CarResponseDTO
{
    public int CarId { get; set; }
    public string CarBrand { get; set; }
    public string CarModel { get; set; }
    public string CarStatus { get; set; }
    public string CarColor { get; set; }
    public string ImgUrl { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public string VIN { get; set; }
    public int Mileage { get; set; }
    public decimal SellingPrice { get; set; }
}
