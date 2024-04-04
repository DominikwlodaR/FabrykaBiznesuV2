using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrykaBiznesuV2.Migrations
{
    /// <inheritdoc />
    public partial class issubtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "AgendaTask");

            migrationBuilder.AddColumn<bool>(
                name: "isSubtask",
                table: "AgendaTask",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSubtask",
                table: "AgendaTask");

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "AgendaTask",
                type: "int",
                nullable: true);
        }
    }
}
