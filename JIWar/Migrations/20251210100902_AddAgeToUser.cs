using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Design_Requests_RequestId",
                table: "Design");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_InteriorDesigner_DesignerUserID",
                schema: "Transactions",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DesignerUserID",
                schema: "Transactions",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DesignerUserID",
                schema: "Transactions",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Design",
                newName: "RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_Design_RequestId",
                table: "Design",
                newName: "IX_Design_RequestID");

            migrationBuilder.AddColumn<string>(
                name: "InteriorDesignerID",
                schema: "Requests",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationLat",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(10,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationLang",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(10,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvaliable",
                schema: "RealEstate",
                table: "Properties",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "InteriorDesignerID",
                table: "InteriorDesigner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "RequestID",
                table: "Design",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Design_Requests_RequestID",
                table: "Design",
                column: "RequestID",
                principalSchema: "Transactions",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Design_Requests_RequestID",
                table: "Design");

            migrationBuilder.DropColumn(
                name: "InteriorDesignerID",
                schema: "Requests",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "InteriorDesignerID",
                table: "InteriorDesigner");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "Design",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Design_RequestID",
                table: "Design",
                newName: "IX_Design_RequestId");

            migrationBuilder.AddColumn<string>(
                name: "DesignerUserID",
                schema: "Transactions",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationLat",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(10,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LocationLang",
                schema: "RealEstate",
                table: "Properties",
                type: "decimal(10,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvaliable",
                schema: "RealEstate",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Design",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DesignerUserID",
                schema: "Transactions",
                table: "Requests",
                column: "DesignerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Design_Requests_RequestId",
                table: "Design",
                column: "RequestId",
                principalSchema: "Transactions",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_InteriorDesigner_DesignerUserID",
                schema: "Transactions",
                table: "Requests",
                column: "DesignerUserID",
                principalTable: "InteriorDesigner",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
