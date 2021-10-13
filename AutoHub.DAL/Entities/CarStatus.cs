using System.Collections.Generic;
using AutoHub.DAL.Enums;

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

        public virtual IEnumerable<Car> Cars { get; }
    }
}