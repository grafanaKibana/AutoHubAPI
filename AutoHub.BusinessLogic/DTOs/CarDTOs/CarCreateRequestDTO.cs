namespace AutoHub.BusinessLogic.DTOs.CarDTOs;

public record CarCreateRequestDTO : CarBaseRequestDTO
{
    public decimal CostPrice { get; set; }
}