using GamesRental.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public virtual DbSet<Game> Games { get; set; } = null!;

    public virtual DbSet<GameCopy> GameCopies { get; set; } = null!;

    public virtual DbSet<Rental> Rentals { get; set; } = null!;

    public virtual DbSet<Genre> Genres { get; set; } = null!;

    public virtual DbSet<Platform> Platforms { get; set; } = null!;

    public virtual DbSet<Review> Reviews { get; set; } = null!;

    public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Wishlist>()
            .HasIndex(w => new { w.UserId, w.GameId })
            .IsUnique();

        builder.Entity<Review>()
            .HasIndex(r => new { r.UserId, r.GameId })
            .IsUnique();

        builder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "RPG" },
            new Genre { Id = 3, Name = "Adventure" },
            new Genre { Id = 4, Name = "Simulation" },
            new Genre { Id = 5, Name = "Strategy" },
            new Genre { Id = 6, Name = "Sports" },
            new Genre { Id = 7, Name = "Puzzle" },
            new Genre { Id = 8, Name = "Horror" },
            new Genre { Id = 9, Name = "Multiplayer" },
            new Genre { Id = 10, Name = "Indie" }
        );

        builder.Entity<Platform>().HasData(
            new Platform { Id = 1, Name = "PlayStation 4" },
            new Platform { Id = 2, Name = "PlayStation 5" },
            new Platform { Id = 3, Name = "Xbox One" },
            new Platform { Id = 4, Name = "Xbox Series X" },
            new Platform { Id = 5, Name = "Nintendo Switch" },
            new Platform { Id = 6, Name = "Nintendo Switch 2" }
        );

        builder.Entity<Game>().HasData(
            new Game
            {
                Id = 1,
                Title = "Elden Ring",
                Description = "Epic action RPG",
                GenreId = 2,
                PlatformId = 1,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/b/b9/Elden_Ring_Box_art.jpg/250px-Elden_Ring_Box_art.jpg",
                ReleaseDate = new DateTime(2022, 2, 25),
                TotalCopies = 3
            },

            new Game
            {
                Id = 2,
                Title = "The Legend of Zelda: Breath of the Wild",
                Description = "Open-world adventure game",
                GenreId = 3,
                PlatformId = 5,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/c/c6/The_Legend_of_Zelda_Breath_of_the_Wild.jpg/250px-The_Legend_of_Zelda_Breath_of_the_Wild.jpg",
                ReleaseDate = new DateTime(2017, 3, 3),
                TotalCopies = 3
            },

            new Game
            {
                Id = 3,
                Title = "FIFA 23",
                Description = "Latest installment in the FIFA series",
                GenreId = 6,
                PlatformId = 1,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a6/FIFA_23_Cover.jpg/250px-FIFA_23_Cover.jpg",
                ReleaseDate = new DateTime(2022, 9, 30),
                TotalCopies = 3
            },

            new Game
            {
                Id = 4,
                Title = "Call of Duty: Modern Warfare II",
                Description = "First-person shooter game",
                GenreId = 1,
                PlatformId = 2,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Call_of_Duty_Modern_Warfare_II_Key_Art.jpg/250px-Call_of_Duty_Modern_Warfare_II_Key_Art.jpg",
                ReleaseDate = new DateTime(2022, 10, 28),
                TotalCopies = 3
            },

            new Game
            {
                Id = 5,
                Title = "Super Mario Odyssey",
                Description = "3D platformer featuring Mario",
                GenreId = 7,
                PlatformId = 5,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/8/8d/Super_Mario_Odyssey.jpg/250px-Super_Mario_Odyssey.jpg",
                ReleaseDate = new DateTime(2017, 10, 27),
                TotalCopies = 3
            },

            new Game
            {
                Id = 6,
                Title = "The Witcher 3: Wild Hunt",
                Description = "Open-world RPG with rich storytelling",
                GenreId = 2,
                PlatformId = 1,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/0/0c/Witcher_3_cover_art.jpg/250px-Witcher_3_cover_art.jpg",
                ReleaseDate = new DateTime(2015, 5, 19),
                TotalCopies = 3
            },

            new Game
            {
                Id = 7,
                Title = "Animal Crossing: New Horizons",
                Description = "Life simulation game",
                GenreId = 4,
                PlatformId = 5,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/1f/Animal_Crossing_New_Horizons.jpg/250px-Animal_Crossing_New_Horizons.jpg",
                ReleaseDate = new DateTime(2020, 3, 20),
                TotalCopies = 3
            },

            new Game
            {
                Id = 8,
                Title = "Halo Infinite",
                Description = "First-person shooter game",
                GenreId = 1,
                PlatformId = 3,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/14/Halo_Infinite.png/250px-Halo_Infinite.png",
                ReleaseDate = new DateTime(2021, 12, 8),
                TotalCopies = 3
            },

            new Game
            {
                Id = 9,
                Title = "God of War Ragnarök",
                Description = "Action-adventure game",
                GenreId = 1,
                PlatformId = 2,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/e/ee/God_of_War_Ragnar%C3%B6k_cover.jpg/250px-God_of_War_Ragnar%C3%B6k_cover.jpg",
                ReleaseDate = new DateTime(2022, 11, 9),
                TotalCopies = 3
            },

            new Game
            {
                Id = 10,
                Title = "Final Fantasy VII Remake",
                Description = "Action RPG remake of the classic game",
                GenreId = 2,
                PlatformId = 1,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/c/ce/FFVIIRemake.png/250px-FFVIIRemake.png",
                ReleaseDate = new DateTime(2020, 4, 10),
                TotalCopies = 3
            }
        );

        builder.Entity<GameCopy>().HasData(
            new GameCopy { Id = 1, GameId = 1, IsRented = false },
            new GameCopy { Id = 2, GameId = 1, IsRented = false },
            new GameCopy { Id = 3, GameId = 1, IsRented = false },
            new GameCopy { Id = 4, GameId = 2, IsRented = false },
            new GameCopy { Id = 5, GameId = 2, IsRented = false },
            new GameCopy { Id = 6, GameId = 2, IsRented = false },
            new GameCopy { Id = 7, GameId = 3, IsRented = false },
            new GameCopy { Id = 8, GameId = 3, IsRented = false },
            new GameCopy { Id = 9, GameId = 3, IsRented = false },
            new GameCopy { Id = 10, GameId = 4, IsRented = false },
            new GameCopy { Id = 11, GameId = 4, IsRented = false },
            new GameCopy { Id = 12, GameId = 4, IsRented = false },
            new GameCopy { Id = 13, GameId = 5, IsRented = false },
            new GameCopy { Id = 14, GameId = 5, IsRented = false },
            new GameCopy { Id = 15, GameId = 5, IsRented = false },
            new GameCopy { Id = 16, GameId = 6, IsRented = false },
            new GameCopy { Id = 17, GameId = 6, IsRented = false },
            new GameCopy { Id = 18, GameId = 6, IsRented = false },
            new GameCopy { Id = 19, GameId = 7, IsRented = false },
            new GameCopy { Id = 20, GameId = 7, IsRented = false },
            new GameCopy { Id = 21, GameId = 7, IsRented = false },
            new GameCopy { Id = 22, GameId = 8, IsRented = false },
            new GameCopy { Id = 23, GameId = 8, IsRented = false },
            new GameCopy { Id = 24, GameId = 8, IsRented = false },
            new GameCopy { Id = 25, GameId = 9, IsRented = false },
            new GameCopy { Id = 26, GameId = 9, IsRented = false },
            new GameCopy { Id = 27, GameId = 9, IsRented = false },
            new GameCopy { Id = 28, GameId = 10, IsRented = false },
            new GameCopy { Id = 29, GameId = 10, IsRented = false },
            new GameCopy { Id = 30, GameId = 10, IsRented = false }
        );
    }
}