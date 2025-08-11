using GamesRental.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesRental.Web.Controllers
{
    
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Catalog()
        {
            var games = await _gameService.GetAvailableGamesAsync();
            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _gameService.GetGameDetailsByIdAsync(id, User);
            if (model == null)
                return NotFound();

            return View(model);
        }
    }
}
