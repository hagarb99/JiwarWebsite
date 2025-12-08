using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jiwar.Migrations
{
    /// <inheritdoc />
    public partial class editownerid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_InteriorDesigner_DesignerID",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Design_InteriorDesigner_DesignerID",
                table: "Design");

            migrationBuilder.DropForeignKey(
                name: "FK_Design_InteriorDesigner_InteriorDesignerDesignerID",
                table: "Design");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyOwners_OwnerID",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_InteriorDesigner_DesignerID",
                schema: "Requests",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestRatings_InteriorDesigner_DesignerID",
                schema: "Ratings",
                table: "RequestRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_InteriorDesigner_DesignerID",
                schema: "Transactions",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyOwners",
                table: "PropertyOwners");

            migrationBuilder.DropIndex(
                name: "IX_PropertyOwners_UserID",
                table: "PropertyOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteriorDesigner",
                table: "InteriorDesigner");

            migrationBuilder.DropIndex(
                name: "IX_InteriorDesigner_UserID",
                table: "InteriorDesigner");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PropertyOwners");

            migrationBuilder.DropColumn(
                name: "DesignerID",
                table: "InteriorDesigner");

            migrationBuilder.RenameColumn(
                name: "DesignerID",
                schema: "Transactions",
                table: "Requests",
                newName: "DesignerUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_DesignerID",
                schema: "Transactions",
                table: "Requests",
                newName: "IX_Requests_DesignerUserID");

            migrationBuilder.RenameColumn(
                name: "InteriorDesignerDesignerID",
                table: "Design",
                newName: "InteriorDesignerUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Design_InteriorDesignerDesignerID",
                table: "Design",
                newName: "IX_Design_InteriorDesignerUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyOwners",
                table: "PropertyOwners",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteriorDesigner",
                table: "InteriorDesigner",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_InteriorDesigner_DesignerID",
                table: "Complaint",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Design_InteriorDesigner_DesignerID",
                table: "Design",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Design_InteriorDesigner_InteriorDesignerUserID",
                table: "Design",
                column: "InteriorDesignerUserID",
                principalTable: "InteriorDesigner",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyOwners_OwnerID",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerID",
                principalTable: "PropertyOwners",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_InteriorDesigner_DesignerID",
                schema: "Requests",
                table: "Proposals",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestRatings_InteriorDesigner_DesignerID",
                schema: "Ratings",
                table: "RequestRatings",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "UserID",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_InteriorDesigner_DesignerID",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Design_InteriorDesigner_DesignerID",
                table: "Design");

            migrationBuilder.DropForeignKey(
                name: "FK_Design_InteriorDesigner_InteriorDesignerUserID",
                table: "Design");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyOwners_OwnerID",
                schema: "RealEstate",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_InteriorDesigner_DesignerID",
                schema: "Requests",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestRatings_InteriorDesigner_DesignerID",
                schema: "Ratings",
                table: "RequestRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_InteriorDesigner_DesignerUserID",
                schema: "Transactions",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyOwners",
                table: "PropertyOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteriorDesigner",
                table: "InteriorDesigner");

            migrationBuilder.RenameColumn(
                name: "DesignerUserID",
                schema: "Transactions",
                table: "Requests",
                newName: "DesignerID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_DesignerUserID",
                schema: "Transactions",
                table: "Requests",
                newName: "IX_Requests_DesignerID");

            migrationBuilder.RenameColumn(
                name: "InteriorDesignerUserID",
                table: "Design",
                newName: "InteriorDesignerDesignerID");

            migrationBuilder.RenameIndex(
                name: "IX_Design_InteriorDesignerUserID",
                table: "Design",
                newName: "IX_Design_InteriorDesignerDesignerID");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PropertyOwners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DesignerID",
                table: "InteriorDesigner",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyOwners",
                table: "PropertyOwners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteriorDesigner",
                table: "InteriorDesigner",
                column: "DesignerID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_UserID",
                table: "PropertyOwners",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InteriorDesigner_UserID",
                table: "InteriorDesigner",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_InteriorDesigner_DesignerID",
                table: "Complaint",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "DesignerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Design_InteriorDesigner_DesignerID",
                table: "Design",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "DesignerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Design_InteriorDesigner_InteriorDesignerDesignerID",
                table: "Design",
                column: "InteriorDesignerDesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "DesignerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyOwners_OwnerID",
                schema: "RealEstate",
                table: "Properties",
                column: "OwnerID",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_InteriorDesigner_DesignerID",
                schema: "Requests",
                table: "Proposals",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "DesignerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestRatings_InteriorDesigner_DesignerID",
                schema: "Ratings",
                table: "RequestRatings",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "DesignerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_InteriorDesigner_DesignerID",
                schema: "Transactions",
                table: "Requests",
                column: "DesignerID",
                principalTable: "InteriorDesigner",
                principalColumn: "DesignerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
