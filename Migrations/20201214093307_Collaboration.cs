using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicCollaboration.Migrations
{
    public partial class Collaboration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollaborationID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collaboration",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Bpm = table.Column<int>(nullable: false),
                    SongKey = table.Column<string>(nullable: true),
                    Genre = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaboration", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CollaborationID",
                table: "AspNetUsers",
                column: "CollaborationID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Collaboration_CollaborationID",
                table: "AspNetUsers",
                column: "CollaborationID",
                principalTable: "Collaboration",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Collaboration_CollaborationID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Collaboration");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CollaborationID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CollaborationID",
                table: "AspNetUsers");
        }
    }
}
