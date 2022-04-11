namespace AutoHub.BusinessLogic.DTOs.CarDTOs;

public class CarUpdateRequestDTO : CarBaseRequestDTO
{
    public decimal CostPrice { get; set; }
    public int CarStatusId { get; set; }
}
