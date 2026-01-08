using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class SimulationChatMessageConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SimulationChatMessages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationChatMessages_RenovationSimulationID",
                table: "SimulationChatMessages",
                column: "RenovationSimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationChatMessages_UserId",
                table: "SimulationChatMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulationChatMessages_RenovationSimulations_RenovationSimulationID",
                table: "SimulationChatMessages",
                column: "RenovationSimulationID",
                principalTable: "RenovationSimulations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulationChatMessages_Users_UserId",
                table: "SimulationChatMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulationChatMessages_RenovationSimulations_RenovationSimulationID",
                table: "SimulationChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulationChatMessages_Users_UserId",
                table: "SimulationChatMessages");

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
        }
    }
}
