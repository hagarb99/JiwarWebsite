using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddSimulationGoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RenovationProjects",
                newName: "RenovationProjects",
                newSchema: "Management");

            migrationBuilder.CreateTable(
                name: "RenovationSimulations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BudgetMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RenovationGoalsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationSimulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationSimulations_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "RealEstate",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationMedias_RenovationSimulations_RenovationSimulationID",
                        column: x => x.RenovationSimulationID,
                        principalTable: "RenovationSimulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAIGenerated = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationRecommendations_RenovationSimulations_RenovationSimulationID",
                        column: x => x.RenovationSimulationID,
                        principalTable: "RenovationSimulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAnalytics_AnalysisDate",
                schema: "Analytics",
                table: "PropertyAnalytics",
                column: "AnalysisDate");

            migrationBuilder.CreateIndex(
                name: "IX_RenovationProjects_PropertyID",
                schema: "Management",
                table: "RenovationProjects",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_RenovationSimulations_PropertyID",
                table: "RenovationSimulations",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationMedias_RenovationSimulationID",
                table: "SimulationMedias",
                column: "RenovationSimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationRecommendations_RenovationSimulationID",
                table: "SimulationRecommendations",
                column: "RenovationSimulationID");

            migrationBuilder.AddForeignKey(
                name: "FK_RenovationProjects_Properties_PropertyID",
                schema: "Management",
                table: "RenovationProjects",
                column: "PropertyID",
                principalSchema: "RealEstate",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenovationProjects_Properties_PropertyID",
                schema: "Management",
                table: "RenovationProjects");

            migrationBuilder.DropTable(
                name: "SimulationMedias");

            migrationBuilder.DropTable(
                name: "SimulationRecommendations");

            migrationBuilder.DropTable(
                name: "RenovationSimulations");

            migrationBuilder.DropIndex(
                name: "IX_PropertyAnalytics_AnalysisDate",
                schema: "Analytics",
                table: "PropertyAnalytics");

            migrationBuilder.DropIndex(
                name: "IX_RenovationProjects_PropertyID",
                schema: "Management",
                table: "RenovationProjects");

            migrationBuilder.RenameTable(
                name: "RenovationProjects",
                schema: "Management",
                newName: "RenovationProjects");
        }
    }
}
