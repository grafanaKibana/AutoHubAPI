using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class CarFKAutoInclude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Lot",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 12, 45, 48, 565, DateTimeKind.Utc).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 12, 45, 48, 565, DateTimeKind.Utc).AddTicks(9551));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 12, 45, 48, 565, DateTimeKind.Utc).AddTicks(9617));

            migrationBuilder.CreateIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot",
                column: "WinnerId",
                unique: true,
                filter: "[WinnerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Lot",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 20, 33, 32, 7, DateTimeKind.Utc).AddTicks(6162));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 20, 33, 32, 7, DateTimeKind.Utc).AddTicks(9940));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 13, 20, 33, 32, 8, DateTimeKind.Utc).AddTicks(132));

            migrationBuilder.CreateIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot",
                column: "WinnerId",
                unique: true);
        }
    }
}
