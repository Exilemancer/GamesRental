using GamesRental.Data.Models;
using GamesRental.Services;
using GamesRental.Services.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services.Tests;

[TestFixture]
public class WishlistServiceTests
{
    private const int TestGameId = 1301;
    private const int TestWishlistId = 9301;

    [Test]
    public async Task AddToWishlistAsync_ShouldAddNewEntry()
    {
        await using var context = TestDbContextFactory.CreateContext();
        await TestDbContextFactory.SeedReferenceDataAsync(context);

        var user = new ApplicationUser { Id = "user-1", UserName = "user1", Email = "user1@test.com" };
        await TestDbContextFactory.SeedUsersAsync(context, user);

        var genre = await context.Genres.FirstAsync();
        var platform = await context.Platforms.FirstAsync();
        context.Games.Add(new Game
        {
            Id = TestGameId,
            Title = "Wishlist Game",
            Description = "Wishlist description",
            ImageUrl = "https://img/wishlist",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 1
        });
        await context.SaveChangesAsync();

        var service = new WishlistService(context);

        var success = await service.AddToWishlistAsync(TestGameId, user.Id);

        Assert.That(success, Is.True);
        Assert.That(await context.Wishlists.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task RemoveFromWishlistAsync_ShouldRemoveExistingEntry()
    {
        await using var context = TestDbContextFactory.CreateContext();
        await TestDbContextFactory.SeedReferenceDataAsync(context);

        var user = new ApplicationUser { Id = "user-1", UserName = "user1", Email = "user1@test.com" };
        await TestDbContextFactory.SeedUsersAsync(context, user);

        var genre = await context.Genres.FirstAsync();
        var platform = await context.Platforms.FirstAsync();
        var game = new Game
        {
            Id = TestGameId,
            Title = "Wishlist Game",
            Description = "Wishlist description",
            ImageUrl = "https://img/wishlist",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 1
        };

        context.Games.Add(game);
        context.Wishlists.Add(new Wishlist { Id = TestWishlistId, GameId = TestGameId, Game = game, UserId = user.Id, User = user, AddedOn = DateTime.UtcNow });
        await context.SaveChangesAsync();

        var service = new WishlistService(context);

        var success = await service.RemoveFromWishlistAsync(TestGameId, user.Id);

        Assert.That(success, Is.True);
        Assert.That(await context.Wishlists.CountAsync(), Is.EqualTo(0));
    }
}
