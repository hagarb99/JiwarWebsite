using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_userId",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "userId",
                schema: "RealEstate",
                table: "Properties",
                newName: "OwnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_userId",
                schema: "RealEstate",
                table: "Properties",
                newName: "IX_Properties_OwnerUserId");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMonths",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "Property",
                table: "PropertyMedia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "RealEstate",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "RealEstate",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "RealEstate",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "RealEstate",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PropertyID",
                schema: "Messaging",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PropertyPriceHistories",
                columns: table => new
                {
                    PropertyPriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPriceHistories", x => x.PropertyPriceHistoryId);
                    table.ForeignKey(
                        name: "FK_PropertyPriceHistories_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportOrders_Reports_ReportID",
                        column: x => x.ReportID,
                        principalSchema: "Complaints",
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportOrders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_PropertyID",
                schema: "Messaging",
                table: "Chat",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPriceHistories_PropertyID",
                table: "PropertyPriceHistories",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportOrders_ReportID",
                table: "ReportOrders",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportOrders_UserID",
                table: "ReportOrders",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Properties_PropertyID",
                schema: "Messaging",
                table: "Chat",
                column: "PropertyID",
                principalSchema: "RealEstate",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_OwnerUserId",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Properties_PropertyID",
                schema: "Messaging",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_OwnerUserId",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyPriceHistories");

            migrationBuilder.DropTable(
                name: "ReportOrders");

            migrationBuilder.DropIndex(
                name: "IX_Chat_PropertyID",
                schema: "Messaging",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "DurationInMonths",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "Property",
                table: "PropertyMedia");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyID",
                schema: "Messaging",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                schema: "RealEstate",
                table: "Properties",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OwnerUserId",
                schema: "RealEstate",
                table: "Properties",
                newName: "IX_Properties_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_userId",
                schema: "RealEstate",
                table: "Properties",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
