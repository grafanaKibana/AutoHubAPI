using System;
using System.Linq;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class UserRoleConfiguration
    {
        public UserRoleConfiguration(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable("CarStatus").HasKey(role => role.UserRoleId);
            entity.Property(role => role.UserRoleId).HasConversion<int>();
            entity.Property(role => role.UserRoleName).HasConversion<string>();

            entity.HasData(
                Enum.GetValues(typeof(UserRoleId))
                    .Cast<UserRoleId>()
                    .Select(u => new UserRole
                    {
                        UserRoleId = u,
                        UserRoleName = u.ToString()
                    }));
        }
    }
}