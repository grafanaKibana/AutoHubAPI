using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Entities
{
    public class Car
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
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public CarStatusId CarStatusId { get; set; }
        public CarStatus CarStatus { get; set; }
    }
}