using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class editconfiginPandB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                schema: "Transactions",
                table: "Booking",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Offers_OfferID",
                schema: "Transactions",
                table: "Booking",
                column: "OfferID",
                principalTable: "Offers",
                principalColumn: "OfferID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Properties_PropertyID",
                schema: "Transactions",
                table: "Booking",
                column: "PropertyID",
                principalSchema: "RealEstate",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Users_CustomerID",
                schema: "Transactions",
                table: "Booking",
                column: "CustomerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
