using GamesRental.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


public class WishlistController : Controller
{
    private readonly IWishlistService wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        this.wishlistService = wishlistService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(int gameId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await wishlistService.AddToWishlistAsync(gameId, userId);
        return RedirectToAction("Catalog", "Game");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int gameId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await wishlistService.RemoveFromWishlistAsync(gameId, userId);
        return RedirectToAction("Index", "Wishlist");
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var wishlist = await wishlistService.GetUserWishlistAsync(userId);
        return View(wishlist);
    }
}
