using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "video_games");

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genre_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("genre_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValueSql: "NULL::character varying"),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "platform",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    platform_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("platform_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publisher_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("publisher_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("region_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genre_id = table.Column<int>(type: "integer", nullable: true),
                    game_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game", x => x.id);
                    table.ForeignKey(
                        name: "fk_gm_gen",
                        column: x => x.genre_id,
                        principalSchema: "video_games",
                        principalTable: "genre",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "game_publisher",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_id = table.Column<int>(type: "integer", nullable: true),
                    publisher_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gamepub", x => x.id);
                    table.ForeignKey(
                        name: "fk_gpu_gam",
                        column: x => x.game_id,
                        principalSchema: "video_games",
                        principalTable: "game",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gpu_pub",
                        column: x => x.publisher_id,
                        principalSchema: "video_games",
                        principalTable: "publisher",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "persons_games",
                schema: "video_games",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "integer", nullable: false),
                    PersonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons_games", x => new { x.GamesId, x.PersonsId });
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

            migrationBuilder.CreateTable(
                name: "game_platform",
                schema: "video_games",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_publisher_id = table.Column<int>(type: "integer", nullable: true),
                    platform_id = table.Column<int>(type: "integer", nullable: true),
                    release_year = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gameplatform", x => x.id);
                    table.ForeignKey(
                        name: "fk_gpl_gp",
                        column: x => x.game_publisher_id,
                        principalSchema: "video_games",
                        principalTable: "game_publisher",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gpl_pla",
                        column: x => x.platform_id,
                        principalSchema: "video_games",
                        principalTable: "platform",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "region_sales",
                schema: "video_games",
                columns: table => new
                {
                    region_id = table.Column<int>(type: "integer", nullable: true),
                    game_platform_id = table.Column<int>(type: "integer", nullable: true),
                    num_sales = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "NULL::numeric")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_rs_gp",
                        column: x => x.game_platform_id,
                        principalSchema: "video_games",
                        principalTable: "game_platform",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_rs_reg",
                        column: x => x.region_id,
                        principalSchema: "video_games",
                        principalTable: "region",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_genre_id",
                schema: "video_games",
                table: "game",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_platform_game_publisher_id",
                schema: "video_games",
                table: "game_platform",
                column: "game_publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_platform_platform_id",
                schema: "video_games",
                table: "game_platform",
                column: "platform_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_publisher_game_id",
                schema: "video_games",
                table: "game_publisher",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_publisher_publisher_id",
                schema: "video_games",
                table: "game_publisher",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_persons_games_PersonsId",
                schema: "video_games",
                table: "persons_games",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_region_sales_game_platform_id",
                schema: "video_games",
                table: "region_sales",
                column: "game_platform_id");

            migrationBuilder.CreateIndex(
                name: "IX_region_sales_region_id",
                schema: "video_games",
                table: "region_sales",
                column: "region_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons_games",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "region_sales",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "person",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "game_platform",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "region",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "game_publisher",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "platform",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "game",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "publisher",
                schema: "video_games");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "video_games");
        }
    }
}
