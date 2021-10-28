namespace AutoHub.BLL.DTOs.CarDTOs
{
    public class CarUpdateRequestDTO : CarBaseRequestDTO
    {
        public int CarId { get; set; }
        public decimal CostPrice { get; set; }
        public int CarStatusId { get; set; }
    }
}