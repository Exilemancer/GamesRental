using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamesRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDbData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "RPG" },
                    { 3, "Adventure" },
                    { 4, "Simulation" },
                    { 5, "Strategy" },
                    { 6, "Sports" },
                    { 7, "Puzzle" },
                    { 8, "Horror" },
                    { 9, "Multiplayer" },
                    { 10, "Indie" }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PlayStation 4" },
                    { 2, "PlayStation 5" },
                    { 3, "Xbox One" },
                    { 4, "Xbox Series X" },
                    { 5, "Nintendo Switch" },
                    { 6, "Nintendo Switch 2" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GenreId", "ImageUrl", "PlatformId", "ReleaseDate", "Title", "TotalCopies" },
                values: new object[,]
                {
                    { 1, "Epic action RPG", 2, "https://...", 1, new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring", 3 },
                    { 2, "Open-world adventure game", 3, "https://...", 5, new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda: Breath of the Wild", 3 },
                    { 3, "Latest installment in the FIFA series", 6, "https://...", 1, new DateTime(2022, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "FIFA 23", 3 },
                    { 4, "First-person shooter game", 1, "https://...", 2, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Call of Duty: Modern Warfare II", 3 },
                    { 5, "3D platformer featuring Mario", 7, "https://...", 5, new DateTime(2017, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Super Mario Odyssey", 3 },
                    { 6, "Open-world RPG with rich storytelling", 2, "https://...", 1, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt", 3 },
                    { 7, "Life simulation game", 4, "https://...", 5, new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Animal Crossing: New Horizons", 3 },
                    { 8, "First-person shooter game", 1, "https://...", 3, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Halo Infinite", 3 },
                    { 9, "Action-adventure game", 1, "https://...", 2, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "God of War Ragnarök", 3 },
                    { 10, "Action RPG remake of the classic game", 2, "https://...", 1, new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final Fantasy VII Remake", 3 }
                });

            migrationBuilder.InsertData(
                table: "GameCopies",
                columns: new[] { "Id", "GameId", "IsRented", "RentedByUserId", "RentedOn" },
                values: new object[,]
                {
                    { 1, 1, false, null, null },
                    { 2, 1, false, null, null },
                    { 3, 1, false, null, null },
                    { 4, 2, false, null, null },
                    { 5, 2, false, null, null },
                    { 6, 2, false, null, null },
                    { 7, 3, false, null, null },
                    { 8, 3, false, null, null },
                    { 9, 3, false, null, null },
                    { 10, 4, false, null, null },
                    { 11, 4, false, null, null },
                    { 12, 4, false, null, null },
                    { 13, 5, false, null, null },
                    { 14, 5, false, null, null },
                    { 15, 5, false, null, null },
                    { 16, 6, false, null, null },
                    { 17, 6, false, null, null },
                    { 18, 6, false, null, null },
                    { 19, 7, false, null, null },
                    { 20, 7, false, null, null },
                    { 21, 7, false, null, null },
                    { 22, 8, false, null, null },
                    { 23, 8, false, null, null },
                    { 24, 8, false, null, null },
                    { 25, 9, false, null, null },
                    { 26, 9, false, null, null },
                    { 27, 9, false, null, null },
                    { 28, 10, false, null, null },
                    { 29, 10, false, null, null },
                    { 30, 10, false, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "GameCopies",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
