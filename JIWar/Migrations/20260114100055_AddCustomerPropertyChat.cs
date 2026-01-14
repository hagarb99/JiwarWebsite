using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerPropertyChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerPropertyMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPropertyMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPropertyMessages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPropertyMessages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerPropertyMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPropertyMessages_PropertyId",
                table: "CustomerPropertyMessages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPropertyMessages_ReceiverId",
                table: "CustomerPropertyMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPropertyMessages_SenderId",
                table: "CustomerPropertyMessages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Offers_OfferID",
                schema: "Transactions",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Properties_PropertyID",
                schema: "Transactions",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Users_CustomerID",
                schema: "Transactions",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulationChatMessages_RenovationSimulations_RenovationSimulationID",
                table: "SimulationChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulationChatMessages_Users_UserId",
                table: "SimulationChatMessages");

            migrationBuilder.DropTable(
                name: "CustomerPropertyMessages");

            migrationBuilder.DropIndex(
                name: "IX_SimulationChatMessages_RenovationSimulationID",
                table: "SimulationChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_SimulationChatMessages_UserId",
                table: "SimulationChatMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SimulationChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                schema: "Transactions",
                table: "Booking",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Offers_OfferID",
                schema: "Transactions",
                table: "Booking",
                column: "OfferID",
                principalTable: "Offers",
                principalColumn: "OfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Properties_PropertyID",
                schema: "Transactions",
                table: "Booking",
                column: "PropertyID",
                principalSchema: "RealEstate",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Users_CustomerID",
                schema: "Transactions",
                table: "Booking",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
