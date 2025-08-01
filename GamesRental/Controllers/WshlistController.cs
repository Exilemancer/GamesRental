using GamesRental.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamesRental.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = await _wishlistService.GetUserWishlistAsync(userId);
            return View(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _wishlistService.AddToWishlistAsync(gameId, userId);
            return RedirectToAction("Details", "Game", new { id = gameId });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _wishlistService.RemoveFromWishlistAsync(gameId, userId);
            return RedirectToAction("Details", "Game", new { id = gameId });
        }
    }
}
