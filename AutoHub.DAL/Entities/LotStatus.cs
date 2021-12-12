using AutoHub.DAL.Enums;
using System.Collections.Generic;

namespace AutoHub.DAL.Entities
{
    public class LotStatus
    {
        public LotStatus()
        {
            Lots = new List<Lot>();
        }

        public LotStatusEnum LotStatusId { get; set; }
        public string LotStatusName { get; set; }

        public virtual IEnumerable<Lot> Lots { get; }
    }
}