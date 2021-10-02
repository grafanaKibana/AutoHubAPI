using System.Collections.Generic;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Entities
{
    public class LotStatus
    {
        public LotStatus()
        {
            Lots = new List<Lot>();
        }

        public LotStatusId LotStatusId { get; set; }
        public string LotStatusName { get; set; }

        private IEnumerable<Lot> Lots { get; }
    }
}