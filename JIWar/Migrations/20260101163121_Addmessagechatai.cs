using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class Addmessagechatai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SimulationDetails_RenovationSimulationID",
                table: "SimulationDetails");

            migrationBuilder.CreateTable(
                name: "SimulationChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenovationSimulationID = table.Column<int>(type: "int", nullable: false),
                    Sender = table.Column<int>(type: "int", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationChatMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulationDetails_RenovationSimulationID",
                table: "SimulationDetails",
                column: "RenovationSimulationID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_SimulationDetails_RenovationSimulationID",
                table: "SimulationDetails");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationDetails_RenovationSimulationID",
                table: "SimulationDetails",
                column: "RenovationSimulationID");
        }
    }
}
