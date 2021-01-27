using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicCollaboration.Migrations
{
    public partial class CollaborationMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaborationMembers",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    CollaborationID = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationMembers", x => new { x.CollaborationID, x.UserID });
                    table.ForeignKey(
                        name: "FK_CollaborationMembers_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaborationMembers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationMembers_UserID",
                table: "CollaborationMembers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaborationMembers");
        }
    }
}
