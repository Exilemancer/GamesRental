using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Catalog()
    {
        var games = await _gameService.GetAllAvailableAsync();
        return View(games);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _gameService.GetByIdAsync(id, User);
        if (model == null)
            return NotFound();

        return View(model);
    }
}