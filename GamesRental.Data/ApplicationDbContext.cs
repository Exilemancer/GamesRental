using GamesRental.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Game> Games { get; set; }
    public DbSet<GameCopy> GameCopies { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<WishlistItem>()
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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
                ImageUrl = "https://...",
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