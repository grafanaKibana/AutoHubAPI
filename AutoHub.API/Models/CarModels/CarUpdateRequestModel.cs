namespace AutoHub.API.Models.CarModels
{
    public class CarUpdateRequestModel : CarBaseRequestModel
    {
        public decimal CostPrice { get; set; }
        public int CarStatusId { get; set; }
    }
}