using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class ConventionRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 20, 28, 46, 550, DateTimeKind.Utc).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 20, 28, 46, 550, DateTimeKind.Utc).AddTicks(8809));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 20, 28, 46, 550, DateTimeKind.Utc).AddTicks(8895));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 19, 51, 23, 88, DateTimeKind.Utc).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 19, 51, 23, 88, DateTimeKind.Utc).AddTicks(7009));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 19, 51, 23, 88, DateTimeKind.Utc).AddTicks(7128));
        }
    }
}
