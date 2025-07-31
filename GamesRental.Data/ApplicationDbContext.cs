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
    }
}
