namespace AutoHub.BusinessLogic.DTOs.CarDTOs;

public record CarUpdateRequestDTO : CarBaseRequestDTO
{
    public decimal CostPrice { get; set; }
    public int CarStatusId { get; set; }
}