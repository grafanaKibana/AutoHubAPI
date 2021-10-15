using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class CarBrand
    {
        public int CarBrandId { get; set; }

        public string CarBrandName { get; set; }

        public virtual IEnumerable<Car> Cars { get; set; }
    }
}