using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserGameListentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_user_game_list_UserGameListId",
                schema: "video_games",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_UserGameListId",
                schema: "video_games",
                table: "game");

            migrationBuilder.DropColumn(
                name: "UserGameListId",
                schema: "video_games",
                table: "game");

            migrationBuilder.RenameColumn(
                name: "Game",
                schema: "video_games",
                table: "user_game_list",
                newName: "NameOfList");

            migrationBuilder.CreateTable(
                name: "GameUserGameList",
                schema: "video_games",
                columns: table => new
                {
                    GameListsId = table.Column<int>(type: "integer", nullable: false),
                    GamesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUserGameList", x => new { x.GameListsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameUserGameList_game_GamesId",
                        column: x => x.GamesId,
                        principalSchema: "video_games",
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUserGameList_user_game_list_GameListsId",
                        column: x => x.GameListsId,
                        principalSchema: "video_games",
                        principalTable: "user_game_list",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUserGameList_GamesId",
                schema: "video_games",
                table: "GameUserGameList",
                column: "GamesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameUserGameList",
                schema: "video_games");

            migrationBuilder.RenameColumn(
                name: "NameOfList",
                schema: "video_games",
                table: "user_game_list",
                newName: "Game");

            migrationBuilder.AddColumn<int>(
                name: "UserGameListId",
                schema: "video_games",
                table: "game",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_UserGameListId",
                schema: "video_games",
                table: "game",
                column: "UserGameListId");

            migrationBuilder.AddForeignKey(
                name: "FK_game_user_game_list_UserGameListId",
                schema: "video_games",
                table: "game",
                column: "UserGameListId",
                principalSchema: "video_games",
                principalTable: "user_game_list",
                principalColumn: "Id");
        }
    }
}
