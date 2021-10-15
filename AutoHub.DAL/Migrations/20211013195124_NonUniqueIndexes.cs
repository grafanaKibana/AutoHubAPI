using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class NonUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lot_User_CreatorId",
                table: "Lot");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarBrandId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarModelId",
                table: "Car");

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

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarBrandId",
                table: "Car",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarModelId",
                table: "Car",
                column: "CarModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_User_CreatorId",
                table: "Lot",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lot_User_CreatorId",
                table: "Lot");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarBrandId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarModelId",
                table: "Car");

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

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarBrandId",
                table: "Car",
                column: "CarBrandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarModelId",
                table: "Car",
                column: "CarModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_User_CreatorId",
                table: "Lot",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
