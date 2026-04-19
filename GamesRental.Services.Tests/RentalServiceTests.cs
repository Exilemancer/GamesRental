using GamesRental.Data.Models;
using GamesRental.Services;
using GamesRental.Services.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services.Tests;

[TestFixture]
public class RentalServiceTests
{
    private const int TestGameId = 1101;
    private const int TestCopyId = 5101;
    private const int TestRentalId = 7101;

    [Test]
    public async Task RentGameAsync_ShouldCreateRentalAndMarkCopyAsRented()
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
            Title = "Rent Me",
            Description = "Rental description",
            ImageUrl = "https://img/rent",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 1
        };

        context.Games.Add(game);
        context.GameCopies.Add(new GameCopy { Id = TestCopyId, GameId = TestGameId, Game = game, IsRented = false });
        await context.SaveChangesAsync();

        var service = new RentalService(context);

        var success = await service.RentGameAsync(TestGameId, user.Id);

        Assert.That(success, Is.True);
        Assert.That(await context.Rentals.CountAsync(), Is.EqualTo(1));

        var rentedCopy = await context.GameCopies.SingleAsync(gc => gc.Id == TestCopyId);
        Assert.That(rentedCopy.IsRented, Is.True);
    }

    [Test]
    public async Task ReturnGameAsync_ShouldCompleteRentalAndFreeCopy()
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
            Title = "Return Me",
            Description = "Return description",
            ImageUrl = "https://img/return",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 1
        };

        var copy = new GameCopy { Id = TestCopyId, GameId = TestGameId, Game = game, IsRented = true, RentedByUserId = user.Id, RentedOn = DateTime.UtcNow };
        var rental = new Rental { Id = TestRentalId, GameCopyId = TestCopyId, GameCopy = copy, UserId = user.Id, User = user, RentedOn = DateTime.UtcNow };

        context.Games.Add(game);
        context.GameCopies.Add(copy);
        context.Rentals.Add(rental);
        await context.SaveChangesAsync();

        var service = new RentalService(context);

        var success = await service.ReturnGameAsync(rental.Id, user.Id);

        var updatedRental = await context.Rentals.Include(r => r.GameCopy).SingleAsync();
        Assert.That(success, Is.True);
        Assert.That(updatedRental.ReturnedOn, Is.Not.Null);
        Assert.That(updatedRental.GameCopy.IsRented, Is.False);
        Assert.That(updatedRental.GameCopy.RentedByUserId, Is.Null);
    }

    [Test]
    public async Task RentGameAsync_ShouldNotChangeRentalDate_WhenUserAlreadyHasActiveRentalForSameGame()
    {
        await using var context = TestDbContextFactory.CreateContext();
        await TestDbContextFactory.SeedReferenceDataAsync(context);

        var user = new ApplicationUser { Id = "user-1", UserName = "user1", Email = "user1@test.com" };
        await TestDbContextFactory.SeedUsersAsync(context, user);

        var genre = await context.Genres.FirstAsync();
        var platform = await context.Platforms.FirstAsync();
        var originalRentedOn = new DateTime(2026, 4, 1, 10, 30, 0, DateTimeKind.Utc);

        var game = new Game
        {
            Id = TestGameId,
            Title = "Already Rented",
            Description = "Rental description",
            ImageUrl = "https://img/rent",
            GenreId = genre.Id,
            Genre = genre,
            PlatformId = platform.Id,
            Platform = platform,
            ReleaseDate = DateTime.UtcNow,
            TotalCopies = 2
        };

        var rentedCopy = new GameCopy
        {
            Id = TestCopyId,
            GameId = TestGameId,
            Game = game,
            IsRented = true,
            RentedByUserId = user.Id,
            RentedOn = originalRentedOn
        };

        var availableCopy = new GameCopy
        {
            Id = TestCopyId + 1,
            GameId = TestGameId,
            Game = game,
            IsRented = false
        };

        var existingRental = new Rental
        {
            Id = TestRentalId,
            GameCopyId = TestCopyId,
            GameCopy = rentedCopy,
            UserId = user.Id,
            User = user,
            RentedOn = originalRentedOn
        };

        context.Games.Add(game);
        context.GameCopies.AddRange(rentedCopy, availableCopy);
        context.Rentals.Add(existingRental);
        await context.SaveChangesAsync();

        var service = new RentalService(context);

        var alreadyRented = await service.HasActiveRentalForGameAsync(TestGameId, user.Id);
        var success = await service.RentGameAsync(TestGameId, user.Id);

        Assert.That(alreadyRented, Is.True);
        Assert.That(success, Is.False);
        Assert.That(await context.Rentals.CountAsync(), Is.EqualTo(1));

        var persistedRental = await context.Rentals.SingleAsync();
        var persistedRentedCopy = await context.GameCopies.SingleAsync(gc => gc.Id == TestCopyId);
        var persistedAvailableCopy = await context.GameCopies.SingleAsync(gc => gc.Id == TestCopyId + 1);

        Assert.That(persistedRental.RentedOn, Is.EqualTo(originalRentedOn));
        Assert.That(persistedRentedCopy.RentedOn, Is.EqualTo(originalRentedOn));
        Assert.That(persistedAvailableCopy.IsRented, Is.False);
        Assert.That(persistedAvailableCopy.RentedOn, Is.Null);
    }

}
