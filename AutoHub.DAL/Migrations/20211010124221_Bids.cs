using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoHub.DAL.Migrations
{
    public partial class Bids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrand",
                columns: table => new
                {
                    CarBrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBrandName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrand", x => x.CarBrandId);
                });

            migrationBuilder.CreateTable(
                name: "CarColor",
                columns: table => new
                {
                    CarColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarColorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarColor", x => x.CarColorId);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    CarModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarModelName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.CarModelId);
                });

            migrationBuilder.CreateTable(
                name: "CarStatus",
                columns: table => new
                {
                    CarStatusId = table.Column<int>(type: "int", nullable: false),
                    CarStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStatus", x => x.CarStatusId);
                });

            migrationBuilder.CreateTable(
                name: "LotStatus",
                columns: table => new
                {
                    LotStatusId = table.Column<int>(type: "int", nullable: false),
                    LotStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotStatus", x => x.LotStatusId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    UserRoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarStatusId = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrandId = table.Column<int>(type: "int", nullable: false),
                    CarModelId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarColorId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Car_CarBrand_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrand",
                        principalColumn: "CarBrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarColor_CarColorId",
                        column: x => x.CarColorId,
                        principalTable: "CarColor",
                        principalColumn: "CarColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarModel_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModel",
                        principalColumn: "CarModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarStatus_CarStatusId",
                        column: x => x.CarStatusId,
                        principalTable: "CarStatus",
                        principalColumn: "CarStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lot",
                columns: table => new
                {
                    LotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastBid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot", x => x.LotId);
                    table.ForeignKey(
                        name: "FK_Lot_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lot_LotStatus_LotStatusId",
                        column: x => x.LotStatusId,
                        principalTable: "LotStatus",
                        principalColumn: "LotStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lot_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lot_User_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    BidId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    BidValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BidTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => x.BidId);
                    table.ForeignKey(
                        name: "FK_Bid_Lot_LotId",
                        column: x => x.LotId,
                        principalTable: "Lot",
                        principalColumn: "LotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bid_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "CarStatus",
                columns: new[] { "CarStatusId", "CarStatusName" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "OnHold" },
                    { 3, "ReadyForSale" },
                    { 4, "UnderRepair" },
                    { 5, "OnSale" },
                    { 6, "Sold" }
                });

            migrationBuilder.InsertData(
                table: "LotStatus",
                columns: new[] { "LotStatusId", "LotStatusName" },
                values: new object[,]
                {
                    { 1, "NotStarted" },
                    { 2, "InProgress" },
                    { 3, "EndedUp" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "UserRoleName" },
                values: new object[,]
                {
                    { 1, "Guest" },
                    { 2, "Regular" },
                    { 3, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Phone", "RegistrationTime", "UserRoleId" },
                values: new object[] { 2, "julia.clifford@hotmail.com", "Julia", "Clifford", "gGm/YLoNuYebABCXGzuvBeXKptnMGFfobCfPXBgsTRU=", "+380501449999", new DateTime(2021, 10, 10, 12, 42, 20, 690, DateTimeKind.Utc).AddTicks(7146), 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Phone", "RegistrationTime", "UserRoleId" },
                values: new object[] { 3, "emusk@paypal.com", "Elon", "Musk", "ZwSQald1A8FIwjNFQ8xhsITfYxHkPomsLFKFa448oWI=", "+380991449999", new DateTime(2021, 10, 10, 12, 42, 20, 690, DateTimeKind.Utc).AddTicks(7210), 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Phone", "RegistrationTime", "UserRoleId" },
                values: new object[] { 1, "reshetnik.nikita@gmail.com", "Nikita", "Reshetnik", "75K3eLr+dx6JJFuJ7LwIpEpOFmwGZZkRiB84PURz6U8=", "+380698632559", new DateTime(2021, 10, 10, 12, 42, 20, 690, DateTimeKind.Utc).AddTicks(5519), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Bid_LotId",
                table: "Bid",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_UserId",
                table: "Bid",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarBrandId",
                table: "Car",
                column: "CarBrandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarColorId",
                table: "Car",
                column: "CarColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarModelId",
                table: "Car",
                column: "CarModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarStatusId",
                table: "Car",
                column: "CarStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_CarId",
                table: "Lot",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_CreatorId",
                table: "Lot",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_LotStatusId",
                table: "Lot",
                column: "LotStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_WinnerId",
                table: "Lot",
                column: "WinnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bid");

            migrationBuilder.DropTable(
                name: "Lot");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "LotStatus");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CarBrand");

            migrationBuilder.DropTable(
                name: "CarColor");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarStatus");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
