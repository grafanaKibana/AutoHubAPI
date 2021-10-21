using AutoHub.DAL.Enums;

namespace AutoHub.BLL.Models.CarModels
{
    public class CarUpdateRequestModel : CarBaseRequestModel
    {
        public int CarId { get; set; }
        public decimal CostPrice { get; set; }
        public CarStatusEnum CarStatusId { get; set; }
    }
}