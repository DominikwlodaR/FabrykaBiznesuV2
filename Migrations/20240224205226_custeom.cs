using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrykaBiznesuV2.Migrations
{
    /// <inheritdoc />
    public partial class custeom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsManual",
                table: "AgendaTask",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManual",
                table: "AgendaTask");
        }
    }
}
