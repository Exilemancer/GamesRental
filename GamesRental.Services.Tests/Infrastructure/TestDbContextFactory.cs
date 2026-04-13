using GamesRental.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GamesRental.Services.Tests.Infrastructure;

internal static class TestDbContextFactory
{
    public static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }

    public static async Task SeedReferenceDataAsync(ApplicationDbContext context)
    {
        if (!await context.Genres.AnyAsync())
        {
            context.Genres.AddRange(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "RPG" });
        }

        if (!await context.Platforms.AnyAsync())
        {
            context.Platforms.AddRange(
                new Platform { Id = 1, Name = "PC" },
                new Platform { Id = 2, Name = "PlayStation 5" });
        }

        await context.SaveChangesAsync();
    }

    public static async Task SeedUsersAsync(ApplicationDbContext context, params ApplicationUser[] users)
    {
        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }

    public static ClaimsPrincipal CreateUserPrincipal(string userId, string email = "user@test.com")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email)
        };

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));
    }
}
