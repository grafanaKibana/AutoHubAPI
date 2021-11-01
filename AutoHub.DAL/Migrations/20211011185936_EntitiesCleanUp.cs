using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class EntitiesCleanUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 11, 18, 59, 35, 241, DateTimeKind.Utc).AddTicks(1035));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 11, 18, 59, 35, 241, DateTimeKind.Utc).AddTicks(3136));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 11, 18, 59, 35, 241, DateTimeKind.Utc).AddTicks(3250));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 10, 12, 44, 34, 947, DateTimeKind.Utc).AddTicks(325));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 10, 12, 44, 34, 947, DateTimeKind.Utc).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 10, 12, 44, 34, 947, DateTimeKind.Utc).AddTicks(3678));
        }
    }
}
