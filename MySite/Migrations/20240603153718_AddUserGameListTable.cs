using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGameListTable : Migration
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
                name: "UserGamesList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShareableLink = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGamesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGamesList_person_PersonId",
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
                name: "IX_UserGamesList_PersonId",
                table: "UserGamesList",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_game_UserGamesList_UserGameListId",
                schema: "video_games",
                table: "game",
                column: "UserGameListId",
                principalTable: "UserGamesList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_game_comments_UserGamesList_UserGameListId",
                schema: "video_games",
                table: "game_comments",
                column: "UserGameListId",
                principalTable: "UserGamesList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_UserGamesList_UserGameListId",
                schema: "video_games",
                table: "game");

            migrationBuilder.DropForeignKey(
                name: "FK_game_comments_UserGamesList_UserGameListId",
                schema: "video_games",
                table: "game_comments");

            migrationBuilder.DropTable(
                name: "UserGamesList");

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
