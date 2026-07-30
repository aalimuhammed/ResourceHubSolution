using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnActivityNoAndMakeItUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "activity_no",
                table: "services",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_services_activity_no",
                table: "services",
                column: "activity_no",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_services_activity_no",
                table: "services");

            migrationBuilder.AlterColumn<string>(
                name: "activity_no",
                table: "services",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
