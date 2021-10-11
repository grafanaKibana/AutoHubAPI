namespace AutoHub.BLL.Models.CarModels
{
    public class CarViewModel
    {
        public int CarId { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int CarColorId { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public int Mileage { get; set; }
        public decimal SellingPrice { get; set; }
    }
}