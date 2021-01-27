using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicCollaboration.Migrations
{
    public partial class AddOwnerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaboration_AspNetUsers_OwnerId",
                table: "Collaboration");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Collaboration",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Collaboration_OwnerId",
                table: "Collaboration",
                newName: "IX_Collaboration_OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaboration_AspNetUsers_OwnerID",
                table: "Collaboration",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaboration_AspNetUsers_OwnerID",
                table: "Collaboration");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Collaboration",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Collaboration_OwnerID",
                table: "Collaboration",
                newName: "IX_Collaboration_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaboration_AspNetUsers_OwnerId",
                table: "Collaboration",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
