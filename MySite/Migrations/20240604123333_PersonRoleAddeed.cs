using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class PersonRoleAddeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "video_games",
                table: "person",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "video_games",
                table: "person");
        }
    }
}
