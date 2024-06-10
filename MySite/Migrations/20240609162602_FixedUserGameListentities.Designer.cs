﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySite.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MySite.Migrations
{
    [DbContext(typeof(DbVideoGamesContext))]
    [Migration("20240609162602_FixedUserGameListentities")]
    partial class FixedUserGameListentities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GamePerson", b =>
                {
                    b.Property<int>("GamesId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonsId")
                        .HasColumnType("integer");

                    b.HasKey("GamesId", "PersonsId");

                    b.HasIndex("PersonsId");

                    b.ToTable("GamePerson", "video_games");
                });

            modelBuilder.Entity("GameUserGameList", b =>
                {
                    b.Property<int>("GameListsId")
                        .HasColumnType("integer");

                    b.Property<int>("GamesId")
                        .HasColumnType("integer");

                    b.HasKey("GameListsId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameUserGameList", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserGameListId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PersonId");

                    b.HasIndex("UserGameListId");

                    b.ToTable("game_comments", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GameName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("game_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<int?>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("genre_id");

                    b.HasKey("Id")
                        .HasName("pk_game");

                    b.HasIndex("GenreId");

                    b.ToTable("game", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.GamePlatform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GamePublisherId")
                        .HasColumnType("integer")
                        .HasColumnName("game_publisher_id");

                    b.Property<int?>("PlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("platform_id");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("integer")
                        .HasColumnName("release_year");

                    b.HasKey("Id")
                        .HasName("pk_gameplatform");

                    b.HasIndex("GamePublisherId");

                    b.HasIndex("PlatformId");

                    b.ToTable("game_platform", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.GamePublisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("integer")
                        .HasColumnName("publisher_id");

                    b.HasKey("Id")
                        .HasName("pk_gamepub");

                    b.HasIndex("GameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("game_publisher", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GenreName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("genre_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("genre_pkey");

                    b.ToTable("genre", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code2FA")
                        .HasColumnType("text");

                    b.Property<string>("Enable2FA")
                        .HasColumnType("text");

                    b.Property<string>("LoginName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("login_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_person");

                    b.ToTable("person", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PlatformName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("platform_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("platform_pkey");

                    b.ToTable("platform", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PublisherName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("publisher_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("publisher_pkey");

                    b.ToTable("publisher", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RegionName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("region_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("region_pkey");

                    b.ToTable("region", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.RegionSale", b =>
                {
                    b.Property<int?>("GamePlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("game_platform_id");

                    b.Property<decimal?>("NumSales")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("num_sales")
                        .HasDefaultValueSql("NULL::numeric");

                    b.Property<int?>("RegionId")
                        .HasColumnType("integer")
                        .HasColumnName("region_id");

                    b.HasIndex("GamePlatformId");

                    b.HasIndex("RegionId");

                    b.ToTable("region_sales", "video_games");
                });

            modelBuilder.Entity("MySite.Entities.UserGameList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NameOfList")
                        .HasColumnType("text");

                    b.Property<int?>("PersonId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ShareableLink")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("user_game_list", "video_games");
                });

            modelBuilder.Entity("GamePerson", b =>
                {
                    b.HasOne("MySite.Entities.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MySite.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameUserGameList", b =>
                {
                    b.HasOne("MySite.Entities.UserGameList", null)
                        .WithMany()
                        .HasForeignKey("GameListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MySite.Entities.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MySite.Entities.Comment", b =>
                {
                    b.HasOne("MySite.Entities.Game", "Game")
                        .WithMany("Comments")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MySite.Entities.Person", "Person")
                        .WithMany("Comments")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MySite.Entities.UserGameList", null)
                        .WithMany("Comments")
                        .HasForeignKey("UserGameListId");

                    b.Navigation("Game");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MySite.Entities.Game", b =>
                {
                    b.HasOne("MySite.Entities.Genre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("fk_gm_gen");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MySite.Entities.GamePlatform", b =>
                {
                    b.HasOne("MySite.Entities.GamePublisher", "GamePublisher")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GamePublisherId")
                        .HasConstraintName("fk_gpl_gp");

                    b.HasOne("MySite.Entities.Platform", "Platform")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformId")
                        .HasConstraintName("fk_gpl_pla");

                    b.Navigation("GamePublisher");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("MySite.Entities.GamePublisher", b =>
                {
                    b.HasOne("MySite.Entities.Game", "Game")
                        .WithMany("GamePublishers")
                        .HasForeignKey("GameId")
                        .HasConstraintName("fk_gpu_gam");

                    b.HasOne("MySite.Entities.Publisher", "Publisher")
                        .WithMany("GamePublishers")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("fk_gpu_pub");

                    b.Navigation("Game");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("MySite.Entities.RegionSale", b =>
                {
                    b.HasOne("MySite.Entities.GamePlatform", "GamePlatform")
                        .WithMany()
                        .HasForeignKey("GamePlatformId")
                        .HasConstraintName("fk_rs_gp");

                    b.HasOne("MySite.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .HasConstraintName("fk_rs_reg");

                    b.Navigation("GamePlatform");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("MySite.Entities.UserGameList", b =>
                {
                    b.HasOne("MySite.Entities.Person", "Person")
                        .WithMany("GameLists")
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MySite.Entities.Game", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("GamePublishers");
                });

            modelBuilder.Entity("MySite.Entities.GamePublisher", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("MySite.Entities.Genre", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("MySite.Entities.Person", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("GameLists");
                });

            modelBuilder.Entity("MySite.Entities.Platform", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("MySite.Entities.Publisher", b =>
                {
                    b.Navigation("GamePublishers");
                });

            modelBuilder.Entity("MySite.Entities.UserGameList", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
