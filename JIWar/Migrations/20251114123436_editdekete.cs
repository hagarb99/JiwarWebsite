using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class editdekete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FinishingStatus",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "userTypeEnum",
                table: "Users",
                newName: "UserTypeEnum");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "RealEstate",
                table: "Properties",
                newName: "statusEnum");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "Transactions",
                table: "Booking",
                newName: "status");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                schema: "RealEstate",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvaliable",
                schema: "RealEstate",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "LocationLang",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(10,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LocationLat",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(10,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                schema: "RealEstate",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "Transactions",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                schema: "Transactions",
                table: "Booking",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    planTypeEnum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    statusEnum2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyOwners_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerID",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_userId",
                schema: "RealEstate",
                table: "Properties",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_UserID",
                table: "PropertyOwners",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyOwners_OwnerID",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerID",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_userId",
                schema: "RealEstate",
                table: "Properties",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyOwners_OwnerID",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_userId",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyOwners");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OwnerID",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_userId",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsAvaliable",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LocationLang",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LocationLat",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Cost",
                schema: "Transactions",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "UserTypeEnum",
                table: "Users",
                newName: "userTypeEnum");

            migrationBuilder.RenameColumn(
                name: "statusEnum",
                schema: "RealEstate",
                table: "Properties",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "Transactions",
                table: "Booking",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                schema: "RealEstate",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FinishingStatus",
                schema: "RealEstate",
                table: "Properties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyType",
                schema: "RealEstate",
                table: "Properties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "Transactions",
                table: "Booking",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
