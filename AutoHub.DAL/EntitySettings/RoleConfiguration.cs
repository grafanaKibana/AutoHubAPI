using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Entities.Identity;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(new List<AppRole>
            {
                new AppRole
                {
                    Id = 1,
                    Name = "Customer",
                    NormalizedName = "Customer".Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new AppRole
                {
                    Id = 2,
                    Name = "Seller",
                    NormalizedName = "Seller".Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new AppRole
                {
                    Id = 3,
                    Name = "Administrator",
                    NormalizedName = "Administrator".Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            });
        }
    }
}
