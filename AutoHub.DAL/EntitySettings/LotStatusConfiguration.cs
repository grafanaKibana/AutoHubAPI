using System;
using System.Linq;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class LotStatusConfiguration
    {
        public LotStatusConfiguration(EntityTypeBuilder<LotStatus> entity)
        {
            entity.ToTable("LotStatus").HasKey(status => status.LotStatusId);
            entity.Property(status => status.LotStatusId).HasConversion<int>();
            entity.Property(status => status.LotStatusName).HasConversion<string>();

            entity.HasData(
                Enum.GetValues(typeof(LotStatusId))
                    .Cast<LotStatusId>()
                    .Select(s => new LotStatus
                    {
                        LotStatusId = s,
                        LotStatusName = s.ToString()
                    }));
        }
    }
}