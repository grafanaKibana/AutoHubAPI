using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class CarColor
    {
        public CarColor()
        {
            Cars = new List<Car>();
        }

        public int CarColorId { get; set; }

        public string CarColorName { get; set; }

        public virtual IEnumerable<Car> Cars { get; set; }
    }
}