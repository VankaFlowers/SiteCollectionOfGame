using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserGameListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserGameListId",
                schema: "video_games",
                table: "game_comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserGameListId",
                schema: "video_games",
                table: "game",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_game_list",
                schema: "video_games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Game = table.Column<string>(type: "text", nullable: true),
                    ShareableLink = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_game_list", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_game_list_person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "video_games",
                        principalTable: "person",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_comments_UserGameListId",
                schema: "video_games",
                table: "game_comments",
                column: "UserGameListId");

            migrationBuilder.CreateIndex(
                name: "IX_game_UserGameListId",
                schema: "video_games",
                table: "game",
                column: "UserGameListId");

            migrationBuilder.CreateIndex(
                name: "IX_user_game_list_PersonId",
                schema: "video_games",
                table: "user_game_list",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_game_user_game_list_UserGameListId",
                schema: "video_games",
                table: "game",
                column: "UserGameListId",
                principalSchema: "video_games",
                principalTable: "user_game_list",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_game_comments_user_game_list_UserGameListId",
                schema: "video_games",
                table: "game_comments",
                column: "UserGameListId",
                principalSchema: "video_games",
                principalTable: "user_game_list",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_user_game_list_UserGameListId",
                schema: "video_games",
                table: "game");

            migrationBuilder.DropForeignKey(
                name: "FK_game_comments_user_game_list_UserGameListId",
                schema: "video_games",
                table: "game_comments");

            migrationBuilder.DropTable(
                name: "user_game_list",
                schema: "video_games");

            migrationBuilder.DropIndex(
                name: "IX_game_comments_UserGameListId",
                schema: "video_games",
                table: "game_comments");

            migrationBuilder.DropIndex(
                name: "IX_game_UserGameListId",
                schema: "video_games",
                table: "game");

            migrationBuilder.DropColumn(
                name: "UserGameListId",
                schema: "video_games",
                table: "game_comments");

            migrationBuilder.DropColumn(
                name: "UserGameListId",
                schema: "video_games",
                table: "game");
        }
    }
}
