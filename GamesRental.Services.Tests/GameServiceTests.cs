using System.Security.Claims;
using GamesRental.Data.Models;
using GamesRental.Services;
using GamesRental.Services.Tests.Infrastructure;
using GamesRental.Web.ViewModels.Game;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services.Tests;

[TestFixture]
public class GameServiceTests
{
    private const int FirstTestGameId = 1001;
    private const int SecondTestGameId = 1002;
    private const int ThirdTestGameId = 1003;
    private const int FirstTestCopyId = 5001;
    private const int SecondTestCopyId = 5002;
    private const int ThirdTestCopyId = 5003;
    private const int TestRentalId = 7001;
    private const int TestReviewId = 8001;
    private const int TestWishlistId = 9001;

    [Test]
    public async Task GetAllAvailableAsync_ShouldFilterAndPaginateAvailableGames()
    {
        await using var context = TestDbContextFactory.CreateContext();
        await TestDbContextFactory.SeedReferenceDataAsync(context);

        var actionGenre = await context.Genres.FirstAsync(g => g.Id == 1);
        var rpgGenre = await context.Genres.FirstAsync(g => g.Id == 2);
        var pcPlatform = await context.Platforms.FirstAsync(p => p.Id == 1);

        var games = new[]
        {
            new Game { Id = FirstTestGameId, Title = "Alpha", Description = "Alpha desc", ImageUrl = "https://img/1", GenreId = rpgGenre.Id, Genre = rpgGenre, PlatformId = pcPlatform.Id, Platform = pcPlatform, ReleaseDate = DateTime.UtcNow, TotalCopies = 1 },
            new Game { Id = SecondTestGameId, Title = "Beta", Description = "Beta desc", ImageUrl = "https://img/2", GenreId = actionGenre.Id, Genre = actionGenre, PlatformId = pcPlatform.Id, Platform = pcPlatform, ReleaseDate = DateTime.UtcNow, TotalCopies = 1 },
            new Game { Id = ThirdTestGameId, Title = "Gamma", Description = "Gamma desc", ImageUrl = "https://img/3", GenreId = rpgGenre.Id, Genre = rpgGenre, PlatformId = pcPlatform.Id, Platform = pcPlatform, ReleaseDate = DateTime.UtcNow, TotalCopies = 1 }
        };

        context.Games.AddRange(games);
        context.GameCopies.AddRange(
            new GameCopy { Id = FirstTestCopyId, GameId = FirstTestGameId, Game = games[0], IsRented = false },
            new GameCopy { Id = SecondTestCopyId, GameId = SecondTestGameId, Game = games[1], IsRented = true },
            new GameCopy { Id = ThirdTestCopyId, GameId = ThirdTestGameId, Game = games[2], IsRented = false });
        await context.SaveChangesAsync();

        var service = new GameService(context);

        var result = await service.GetAllAvailableAsync("rpg", 1, 1);

        Assert.That(result.TotalGames, Is.EqualTo(5));
        Assert.That(result.TotalPages, Is.EqualTo(5));
        Assert.That(result.CurrentPage, Is.EqualTo(1));
        Assert.That(result.Games.Select(g => g.Title).ToArray(), Is.EqualTo(new[] { "Alpha" }));
    }

    [Test]
    public async Task CreateAsync_ShouldCreateGameAndRequestedCopies()
    {
        await using var context = TestDbContextFactory.CreateContext();
        await TestDbContextFactory.SeedReferenceDataAsync(context);
        var service = new GameService(context);

        var model = new GameFormViewModel
        {
            Title = "New Game",
            Description = "Very good game description",
            ImageUrl = "https://img/new-game",
            GenreId = 1,
            PlatformId = 2,
            ReleaseDate = new DateTime(2024, 1, 1),
            TotalCopies = 3
        };

        await service.CreateAsync(model);

        var game = await context.Games.Include(g => g.Copies).SingleAsync(g => g.Title == "New Game");
        Assert.That(game.TotalCopies, Is.EqualTo(3));
        Assert.That(game.Copies.Count, Is.EqualTo(3));
        Assert.That(game.Copies.All(c => c.IsRented == false), Is.True);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnReviewsAndUserFlags()
    {
        await using var context = TestDbContextFactory.CreateContext();
        await TestDbContextFactory.SeedReferenceDataAsync(context);

        var user = new ApplicationUser { Id = "user-1", UserName = "admin", Email = "admin@test.com" };
        await TestDbContextFactory.SeedUsersAsync(context, user);

        var genre = await context.Genres.FirstAsync();
        var platform = await context.Platforms.FirstAsync();

        var game = new Game
        {
            Id = FirstTestGameId,
            Title = "Details Game",
            Description = "Detailed description",
            ImageUrl = "https://img/details",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = new DateTime(2023, 5, 1),
            TotalCopies = 2
        };

        var copy = new GameCopy { Id = FirstTestCopyId, GameId = FirstTestGameId, Game = game, IsRented = true, RentedByUserId = user.Id, RentedOn = DateTime.UtcNow };
        var rental = new Rental { Id = TestRentalId, GameCopyId = FirstTestCopyId, GameCopy = copy, UserId = user.Id, User = user, RentedOn = DateTime.UtcNow };
        var review = new Review { Id = TestReviewId, GameId = FirstTestGameId, Game = game, UserId = user.Id, User = user, Rating = 5, Comment = "Amazing", CreatedOn = DateTime.UtcNow };
        var wishlist = new Wishlist { Id = TestWishlistId, GameId = FirstTestGameId, Game = game, UserId = user.Id, User = user, AddedOn = DateTime.UtcNow };

        context.Games.Add(game);
        context.GameCopies.AddRange(copy, new GameCopy { Id = SecondTestCopyId, GameId = FirstTestGameId, Game = game, IsRented = false });
        context.Rentals.Add(rental);
        context.Reviews.Add(review);
        context.Wishlists.Add(wishlist);
        await context.SaveChangesAsync();

        var service = new GameService(context);
        ClaimsPrincipal principal = TestDbContextFactory.CreateUserPrincipal(user.Id, user.Email!);

        var result = await service.GetByIdAsync(FirstTestGameId, principal);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.HasAvailableCopies, Is.True);
        Assert.That(result.HasUserRented, Is.True);
        Assert.That(result.IsInWishlist, Is.True);
        Assert.That(result.Reviews.Count(), Is.EqualTo(1));
        Assert.That(result.Reviews.First().Author, Is.EqualTo(user.Email));
    }
}
