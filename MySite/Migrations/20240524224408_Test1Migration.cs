using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class Test1Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons_games",
                schema: "video_games");

            migrationBuilder.AlterColumn<string>(
                name: "login_name",
                schema: "video_games",
                table: "person",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                defaultValueSql: "NULL::character varying",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldDefaultValueSql: "NULL::character varying");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "video_games",
                table: "person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "game_comments",
                schema: "video_games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_game_comments_game_GameId",
                        column: x => x.GameId,
                        principalSchema: "video_games",
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_comments_person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "video_games",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePerson",
                schema: "video_games",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "integer", nullable: false),
                    PersonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePerson", x => new { x.GamesId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_GamePerson_game_GamesId",
                        column: x => x.GamesId,
                        principalSchema: "video_games",
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePerson_person_PersonsId",
                        column: x => x.PersonsId,
                        principalSchema: "video_games",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_comments_GameId",
                schema: "video_games",
                table: "game_comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_game_comments_PersonId",
                schema: "video_games",
                table: "game_comments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePerson_PersonsId",
                schema: "video_games",
                table: "GamePerson",
                column: "PersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_comments",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "GamePerson",
                schema: "video_games");

            migrationBuilder.AlterColumn<string>(
                name: "login_name",
                schema: "video_games",
                table: "person",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValueSql: "NULL::character varying",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldDefaultValueSql: "NULL::character varying");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "video_games",
                table: "person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "persons_games",
                schema: "video_games",
                columns: table => new
                {
                    PersonsId = table.Column<int>(type: "integer", nullable: false),
                    GamesId = table.Column<int>(type: "integer", nullable: false),
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
    }
}
