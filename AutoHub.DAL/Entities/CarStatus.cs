using AutoHub.DAL.Enums;
using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class CarStatus
    {
        public CarStatus()
        {
            Cars = new List<Car>();
        }

        public CarStatusEnum CarStatusId { get; set; }
        public string CarStatusName { get; set; }

        public virtual IEnumerable<Car> Cars { get; }
    }
}