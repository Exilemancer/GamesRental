using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamesRental.Web.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _reviewService.HasUserReviewedGameAsync(gameId, userId))
            {
                TempData["Error"] = "You have already reviewed this game.";
                return RedirectToAction("Details", "Game", new { id = gameId });
            }

            var model = new ReviewFormViewModel { GameId = gameId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewFormViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
                return View(model);

            var success = await _reviewService.AddReviewAsync(model, userId);
            if (!success)
            {
                TempData["Error"] = "You have already reviewed this game.";
            }
            else
            {
                TempData["Success"] = "Review added successfully.";
            }

            return RedirectToAction("Details", "Game", new { id = model.GameId });
        }
    }
}
