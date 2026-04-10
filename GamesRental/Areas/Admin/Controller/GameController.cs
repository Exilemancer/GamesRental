using GamesRental.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            var games = await gameService.GetAllForAdminAsync();
            return View(games);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await gameService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}