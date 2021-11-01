using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class LotLastBidDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastBid",
                table: "Lot");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Lot",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 1,
                column: "LotStatusName",
                value: "New");

            migrationBuilder.UpdateData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 2,
                column: "LotStatusName",
                value: "NotStarted");

            migrationBuilder.UpdateData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 3,
                column: "LotStatusName",
                value: "InProgress");

            migrationBuilder.InsertData(
                table: "LotStatus",
                columns: new[] { "LotStatusId", "LotStatusName" },
                values: new object[] { 4, "EndedUp" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 13, 47, 42, 232, DateTimeKind.Utc).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 13, 47, 42, 232, DateTimeKind.Utc).AddTicks(9966));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 13, 47, 42, 233, DateTimeKind.Utc).AddTicks(37));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Lot",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LastBid",
                table: "Lot",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 1,
                column: "LotStatusName",
                value: "NotStarted");

            migrationBuilder.UpdateData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 2,
                column: "LotStatusName",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "LotStatus",
                keyColumn: "LotStatusId",
                keyValue: 3,
                column: "LotStatusName",
                value: "EndedUp");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 13, 12, 42, 649, DateTimeKind.Utc).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 13, 12, 42, 649, DateTimeKind.Utc).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2021, 10, 15, 13, 12, 42, 649, DateTimeKind.Utc).AddTicks(8785));
        }
    }
}
