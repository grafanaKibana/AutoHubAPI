using AutoHub.DAL.Enums;

namespace AutoHub.BLL.Models.CarModels
{
    public class CarUpdateApiModel : CarBaseApiModel
    {
        public decimal CostPrice { get; set; }
        public CarStatusId CarStatusId { get; set; }
    }
}