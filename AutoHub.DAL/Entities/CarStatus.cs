using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class CarStatus
    {
        public enum Status
        {
            OnHold = 1,
            ReadyForSale = 2,
            UnderRepair = 3,
            OnSale = 4,
            Sold = 5
        }

        public CarStatus()
        {
            Cars = new List<Car>();
        }
        
        public Status CarStatusId { get; set; }
        public string CarStatusName { get; set; }

        private IEnumerable<Car> Cars { get; set; }
    }
}