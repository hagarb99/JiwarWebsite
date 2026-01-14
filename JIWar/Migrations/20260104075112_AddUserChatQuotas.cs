using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddUserChatQuotas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AI");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SimulationChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserChatQuotas",
                schema: "AI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RemainingMessages = table.Column<int>(type: "int", nullable: false),
                    LastReset = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatQuotas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChatQuotas_UserId",
                schema: "AI",
                table: "UserChatQuotas",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChatQuotas",
                schema: "AI");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SimulationChatMessages");
        }
    }
}
