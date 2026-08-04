using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCreatedOnColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "created_on",
                table: "services",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "changed_on",
                table: "services",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "created_on",
                table: "services",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "changed_on",
                table: "services",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
