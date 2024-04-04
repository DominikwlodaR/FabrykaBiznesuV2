using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrykaBiznesuV2.Migrations
{
    /// <inheritdoc />
    public partial class comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isReply = table.Column<bool>(type: "bit", nullable: false),
                    AgendaCommentsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaComments_AgendaComments_AgendaCommentsId",
                        column: x => x.AgendaCommentsId,
                        principalTable: "AgendaComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgendaComments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaComments_AgendaCommentsId",
                table: "AgendaComments",
                column: "AgendaCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaComments_ProjectId",
                table: "AgendaComments",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaComments");
        }
    }
}
