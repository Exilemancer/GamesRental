using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGamesLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/b/b9/Elden_Ring_Box_art.jpg/250px-Elden_Ring_Box_art.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/c/c6/The_Legend_of_Zelda_Breath_of_the_Wild.jpg/250px-The_Legend_of_Zelda_Breath_of_the_Wild.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/a/a6/FIFA_23_Cover.jpg/250px-FIFA_23_Cover.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Call_of_Duty_Modern_Warfare_II_Key_Art.jpg/250px-Call_of_Duty_Modern_Warfare_II_Key_Art.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/8/8d/Super_Mario_Odyssey.jpg/250px-Super_Mario_Odyssey.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/0/0c/Witcher_3_cover_art.jpg/250px-Witcher_3_cover_art.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/1/1f/Animal_Crossing_New_Horizons.jpg/250px-Animal_Crossing_New_Horizons.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/1/14/Halo_Infinite.png/250px-Halo_Infinite.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/e/ee/God_of_War_Ragnar%C3%B6k_cover.jpg/250px-God_of_War_Ragnar%C3%B6k_cover.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/thumb/c/ce/FFVIIRemake.png/250px-FFVIIRemake.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://...");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://...");
        }
    }
}
