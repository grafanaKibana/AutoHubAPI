namespace AutoHub.API.Models.CarModels;

public class CarUpdateRequest : CarBaseRequest
{
    public decimal CostPrice { get; set; }
    public int CarStatusId { get; set; }
}
