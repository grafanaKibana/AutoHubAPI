using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class EnumRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Lot_LotId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarStatus_CarStatusId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Lot_LotStatus_LotStatusId",
                table: "Lot");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRole_UserRoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Lot_LotId",
                table: "Bid",
                column: "LotId",
                principalTable: "Lot",
                principalColumn: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarStatus_CarStatusId",
                table: "Car",
                column: "CarStatusId",
                principalTable: "CarStatus",
                principalColumn: "CarStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_LotStatus_LotStatusId",
                table: "Lot",
                column: "LotStatusId",
                principalTable: "LotStatus",
                principalColumn: "LotStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRole_UserRoleId",
                table: "User",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Lot_LotId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarStatus_CarStatusId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Lot_LotStatus_LotStatusId",
                table: "Lot");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserRole_UserRoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Phone", "RegistrationTime", "UserRoleId" },
                values: new object[] { 1, "reshetnik.nikita@gmail.com", "Nikita", "Reshetnik", "75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=", "+380698632559", new DateTime(2021, 10, 15, 13, 47, 42, 232, DateTimeKind.Utc).AddTicks(8185), 3 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Phone", "RegistrationTime", "UserRoleId" },
                values: new object[] { 2, "julia.clifford@hotmail.com", "Julia", "Clifford", "gGm/YLoNuYebABCXGzuvBeXKptnMGFfobCfPXBgsTRU=", "+380501449999", new DateTime(2021, 10, 15, 13, 47, 42, 232, DateTimeKind.Utc).AddTicks(9966), 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Phone", "RegistrationTime", "UserRoleId" },
                values: new object[] { 3, "emusk@paypal.com", "Elon", "Musk", "ZwSQald1A8FIwjNFQ8xhsITfYxHkPomsLFKFa448oWI=", "+380991449999", new DateTime(2021, 10, 15, 13, 47, 42, 233, DateTimeKind.Utc).AddTicks(37), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot",
                column: "WinnerId",
                unique: true,
                filter: "[WinnerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Lot_LotId",
                table: "Bid",
                column: "LotId",
                principalTable: "Lot",
                principalColumn: "LotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarStatus_CarStatusId",
                table: "Car",
                column: "CarStatusId",
                principalTable: "CarStatus",
                principalColumn: "CarStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lot_LotStatus_LotStatusId",
                table: "Lot",
                column: "LotStatusId",
                principalTable: "LotStatus",
                principalColumn: "LotStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserRole_UserRoleId",
                table: "User",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
