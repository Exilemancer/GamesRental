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

            var model = new StatisticsIndexViewModel
            {
                TotalGames = await context.Games.CountAsync(),
                TotalGenres = await context.Genres.CountAsync(),
                TotalPlatforms = await context.Platforms.CountAsync(),
                TotalCopies = await context.GameCopies.CountAsync(),
                AvailableCopies = await context.GameCopies.CountAsync(c => !c.IsRented),
                RentedCopies = await context.GameCopies.CountAsync(c => c.IsRented),
                TotalReviews = await context.Reviews.CountAsync(),
                WishlistEntries = await context.Wishlists.CountAsync()
            };

            return View(model);
        }
    }
}
