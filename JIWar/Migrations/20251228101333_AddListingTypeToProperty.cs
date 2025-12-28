using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddListingTypeToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MediaType",
                schema: "Property",
                table: "PropertyMedia",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ListingType",
                schema: "RealEstate",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DesignerID",
                schema: "Design",
                table: "DesignerProposals",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerProposals_DesignerID",
                schema: "Design",
                table: "DesignerProposals",
                column: "DesignerID");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignerProposals_InteriorDesigners_DesignerID",
                schema: "Design",
                table: "DesignerProposals",
                column: "DesignerID",
                principalTable: "InteriorDesigners",
                principalColumn: "InteriorDesignerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignerProposals_InteriorDesigners_DesignerID",
                schema: "Design",
                table: "DesignerProposals");

            migrationBuilder.DropIndex(
                name: "IX_DesignerProposals_DesignerID",
                schema: "Design",
                table: "DesignerProposals");

            migrationBuilder.DropColumn(
                name: "ListingType",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "MediaType",
                schema: "Property",
                table: "PropertyMedia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DesignerID",
                schema: "Design",
                table: "DesignerProposals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
