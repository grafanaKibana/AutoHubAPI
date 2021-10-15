using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class CarModel
    {
        public int CarModelId { get; set; }

        public string CarModelName { get; set; }

        public virtual IEnumerable<Car> Cars { get; set; }
    }
}