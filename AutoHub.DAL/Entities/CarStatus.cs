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
        
        public ECarStatus CarStatusId { get; set; }
        public string CarStatusName { get; set; }

        private IEnumerable<Car> Cars { get; set; }
    }
}