using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddSimulationDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PropertyID",
                table: "RenovationSimulations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DesignerID",
                schema: "Design",
                table: "DesignerProposals",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "SimulationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearBuilt = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationDetails_RenovationSimulations_RenovationSimulationID",
                        column: x => x.RenovationSimulationID,
                        principalTable: "RenovationSimulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
            name: "IX_DesignerProposals_DesignerID_NEW",
            table: "DesignerProposals",
            column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationDetails_RenovationSimulationID",
                table: "SimulationDetails",
                column: "RenovationSimulationID");

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

            migrationBuilder.DropTable(
                name: "SimulationDetails");

            migrationBuilder.DropIndex(
                name: "IX_DesignerProposals_DesignerID",
                schema: "Design",
                table: "DesignerProposals");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyID",
                table: "RenovationSimulations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
