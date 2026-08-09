using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddValuesToCursorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                            UPDATE Services s
                            JOIN
                            (
                                SELECT Id,
                                       ROW_NUMBER() OVER (ORDER BY Id) AS NewCursorId
                                FROM Services
                            ) temp
                            ON s.Id = temp.Id
                            SET s.cursor_id = temp.NewCursorId;
                        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
