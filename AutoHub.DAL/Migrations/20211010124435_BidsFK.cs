using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AutoHub.DAL.Migrations
{
    public partial class BidsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 10, 12, 42, 20, 690, DateTimeKind.Utc).AddTicks(5519));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 10, 12, 42, 20, 690, DateTimeKind.Utc).AddTicks(7146));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 10, 12, 42, 20, 690, DateTimeKind.Utc).AddTicks(7210));
        }
    }
}
