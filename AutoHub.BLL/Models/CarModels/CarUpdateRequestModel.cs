using AutoHub.DAL.Enums;

namespace AutoHub.BLL.Models.CarModels
{
    public class CarUpdateRequestModel : CarBaseRequestModel
    {
        public decimal CostPrice { get; set; }
        public CarStatusId CarStatusId { get; set; }
    }
}