using GamesRental.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamesRental.Web.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool success = await _rentalService.RentGameAsync(gameId, userId);

            if (!success)
                TempData["Error"] = "No available copies for this game.";
            else
                TempData["Success"] = "Game rented successfully!";

            return RedirectToAction("Details", "Game", new { id = gameId });
        }

        [HttpGet]
        public async Task<IActionResult> MyGames()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rentals = await _rentalService.GetActiveRentalsByUserAsync(userId);
            return View(rentals);
        }

        [HttpPost]
        public async Task<IActionResult> Return(int rentalId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool success = await _rentalService.ReturnGameAsync(rentalId, userId);

            if (!success)
                TempData["Error"] = "Error while returning the game.";
            else
                TempData["Success"] = "Game returned successfully!";

            return RedirectToAction("MyGames");
        }
    }
}
