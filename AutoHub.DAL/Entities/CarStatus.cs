using System.Collections.Generic;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Entities
{
    public class CarStatus
    {
        public CarStatus()
        {
            Cars = new List<Car>();
        }

        public CarStatusId CarStatusId { get; set; }
        public string CarStatusName { get; set; }

        private IEnumerable<Car> Cars { get; }
    }
}