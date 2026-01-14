using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectDeliveryAndReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Users",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalReviews",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                schema: "dbo",
                table: "DesignerProposals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryNotes",
                schema: "dbo",
                table: "DesignerProposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DesignerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyOwnerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProposalID = table.Column<int>(type: "int", nullable: false),
                    DesignRequestID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignerReviews_DesignRequests_DesignRequestID",
                        column: x => x.DesignRequestID,
                        principalSchema: "Design",
                        principalTable: "DesignRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesignerReviews_DesignerProposals_ProposalID",
                        column: x => x.ProposalID,
                        principalSchema: "dbo",
                        principalTable: "DesignerProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignerReviews_Users_DesignerID",
                        column: x => x.DesignerID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DesignerReviews_Users_PropertyOwnerID",
                        column: x => x.PropertyOwnerID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesignerReviews_DesignerID",
                table: "DesignerReviews",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerReviews_DesignRequestID",
                table: "DesignerReviews",
                column: "DesignRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerReviews_PropertyOwnerID",
                table: "DesignerReviews",
                column: "PropertyOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignerReviews_ProposalID",
                table: "DesignerReviews",
                column: "ProposalID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignerReviews");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalReviews",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                schema: "dbo",
                table: "DesignerProposals");

            migrationBuilder.DropColumn(
                name: "DeliveryNotes",
                schema: "dbo",
                table: "DesignerProposals");
        }
    }
}
