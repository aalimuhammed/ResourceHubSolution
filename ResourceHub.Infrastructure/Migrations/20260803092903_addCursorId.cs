using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCursorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cursor_id",
                table: "services",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cursor_id",
                table: "services");
        }
    }
}
