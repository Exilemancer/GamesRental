using GamesRental.Data.Models;
using GamesRental.Services;
using GamesRental.Services.Tests.Infrastructure;
using GamesRental.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services.Tests;

[TestFixture]
public class ReviewServiceTests
{
    private const int TestGameId = 1201;
    private const int TestReviewId = 8201;

    [Test]
    public async Task AddReviewAsync_ShouldAddReviewWhenUserHasNotReviewedGame()
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
            Title = "Review Game",
            Description = "Review description",
            ImageUrl = "https://img/review",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 1
        });
        await context.SaveChangesAsync();

        var service = new ReviewService(context);
        var model = new ReviewFormViewModel { GameId = TestGameId, Rating = 5, Comment = "Top game" };

        var success = await service.AddReviewAsync(model, user.Id);

        Assert.That(success, Is.True);
        Assert.That(await context.Reviews.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task AddReviewAsync_ShouldRejectDuplicateReviewFromSameUser()
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
            Title = "Review Game",
            Description = "Review description",
            ImageUrl = "https://img/review",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 1
        };

        context.Games.Add(game);
        context.Reviews.Add(new Review
        {
            Id = TestReviewId,
            GameId = TestGameId,
            Game = game,
            UserId = user.Id,
            User = user,
            Rating = 4,
            Comment = "Existing review",
            CreatedOn = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var service = new ReviewService(context);

        var success = await service.AddReviewAsync(new ReviewFormViewModel
        {
            GameId = TestGameId,
            Rating = 5,
            Comment = "Second review"
        }, user.Id);

        Assert.That(success, Is.False);
        Assert.That(await context.Reviews.CountAsync(), Is.EqualTo(1));
    }
}
