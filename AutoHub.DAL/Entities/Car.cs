using AutoHub.DAL.Enums;

namespace AutoHub.DAL.Entities
{
    public class Car
    {
        public int CarId { get; set; }

        public int CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }

        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }

        public CarStatusId CarStatusId { get; set; }
        public CarStatus CarStatus { get; set; }

        public int CarColorId { get; set; }
        public CarColor CarColor { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string VIN { get; set; }

        public int Mileage { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }
    }
}