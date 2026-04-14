using GamesRental.Data.Models;
using GamesRental.Web.Areas.Admin.ViewModels;
using GamesRental.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatisticsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!await User.IsAdminAsync(_userManager))
            {
                return Forbid();
            }

            var rentalHistory = await context.Rentals
                .Include(r => r.User)
                .Include(r => r.GameCopy)
                    .ThenInclude(gc => gc.Game)
                        .ThenInclude(g => g.Platform)
                .OrderByDescending(r => r.RentedOn)
                .ToListAsync();

            var model = new StatisticsIndexViewModel
            {
                TotalGames = await context.Games.CountAsync(),
                TotalUsers = await context.Users.CountAsync(),
                TotalGenres = await context.Genres.CountAsync(),
                TotalPlatforms = await context.Platforms.CountAsync(),
                TotalCopies = await context.GameCopies.CountAsync(),
                AvailableCopies = await context.GameCopies.CountAsync(c => !c.IsRented),
                RentedCopies = await context.GameCopies.CountAsync(c => c.IsRented),
                TotalRentals = rentalHistory.Count,
                TotalReviews = await context.Reviews.CountAsync(),
                WishlistEntries = await context.Wishlists.CountAsync(),
                GameStatistics = rentalHistory
                    .GroupBy(r => new
                    {
                        r.GameCopy.GameId,
                        r.GameCopy.Game.Title,
                        Platform = r.GameCopy.Game.Platform.Name
                    })
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key.Title)
                    .Select(g => new GameRentalStatisticsViewModel
                    {
                        GameId = g.Key.GameId,
                        GameTitle = g.Key.Title,
                        Platform = g.Key.Platform,
                        TotalRentals = g.Count(),
                        ActiveRentals = g.Count(r => r.ReturnedOn == null),
                        RentalHistory = g
                            .OrderByDescending(r => r.RentedOn)
                            .Select(r => new RentalHistoryItemViewModel
                            {
                                GameTitle = r.GameCopy.Game.Title,
                                Platform = r.GameCopy.Game.Platform.Name,
                                UserEmail = r.User.Email ?? r.User.UserName ?? "Unknown user",
                                RentedOn = r.RentedOn,
                                ReturnedOn = r.ReturnedOn
                            })
                            .ToList()
                    })
                    .ToList(),
                UserStatistics = rentalHistory
                    .GroupBy(r => new
                    {
                        r.UserId,
                        Email = r.User.Email ?? r.User.UserName ?? "Unknown user"
                    })
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key.Email)
                    .Select(g => new UserRentalStatisticsViewModel
                    {
                        UserId = g.Key.UserId,
                        Email = g.Key.Email,
                        TotalRentals = g.Count(),
                        ActiveRentals = g.Count(r => r.ReturnedOn == null),
                        RentalHistory = g
                            .OrderByDescending(r => r.RentedOn)
                            .Select(r => new RentalHistoryItemViewModel
                            {
                                GameTitle = r.GameCopy.Game.Title,
                                Platform = r.GameCopy.Game.Platform.Name,
                                UserEmail = g.Key.Email,
                                RentedOn = r.RentedOn,
                                ReturnedOn = r.ReturnedOn
                            })
                            .ToList()
                    })
                    .ToList()
            };

            return View(model);
        }
    }
}
