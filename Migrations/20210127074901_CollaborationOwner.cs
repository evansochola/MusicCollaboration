using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicCollaboration.Migrations
{
    public partial class CollaborationOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Collaboration",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_OwnerId",
                table: "Collaboration",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaboration_AspNetUsers_OwnerId",
                table: "Collaboration",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaboration_AspNetUsers_OwnerId",
                table: "Collaboration");

            migrationBuilder.DropIndex(
                name: "IX_Collaboration_OwnerId",
                table: "Collaboration");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Collaboration");
        }
    }
}
