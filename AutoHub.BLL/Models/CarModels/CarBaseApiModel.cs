namespace AutoHub.BLL.Models.CarModels
{
    public class CarBaseApiModel
    {
        public int CarId { get; set; }
        public string ImgUrl { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public int Mileage { get; set; }
        public decimal SellingPrice { get; set; }
    }
}