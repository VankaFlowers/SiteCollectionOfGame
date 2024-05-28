using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons_games",
                schema: "video_games",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "integer", nullable: false),
                    PersonsId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons_games", x => new { x.PersonsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_persons_games_game_GamesId",
                        column: x => x.GamesId,
                        principalSchema: "video_games",
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_persons_games_person_PersonsId",
                        column: x => x.PersonsId,
                        principalSchema: "video_games",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_games_GamesId",
                schema: "video_games",
                table: "persons_games",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons_games",
                schema: "video_games");
        }
    }
}
