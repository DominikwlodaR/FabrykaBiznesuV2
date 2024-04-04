using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrykaBiznesuV2.Migrations
{
    /// <inheritdoc />
    public partial class team3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Team_TeamId1",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_TeamId1",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "Team");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId1",
                table: "Team",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamId1",
                table: "Team",
                column: "TeamId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Team_TeamId1",
                table: "Team",
                column: "TeamId1",
                principalTable: "Team",
                principalColumn: "TeamId");
        }
    }
}
