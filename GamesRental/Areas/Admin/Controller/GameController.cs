using GamesRental.Data.Models;
using GamesRental.Web.Extensions;
using GamesRental.Web.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class GameController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public GameController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        var games = await _context.Games
            .Include(g => g.Genre)
            .Include(g => g.Platform)
            .OrderBy(g => g.Title)
            .ToListAsync();

        return View(games);
    }

    public async Task<IActionResult> Create()
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        var model = new GameFormViewModel
        {
            ReleaseDate = DateTime.UtcNow,
            Genres = await GetGenresAsync(),
            Platforms = await GetPlatformsAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GameFormViewModel model)
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            model.Genres = await GetGenresAsync();
            model.Platforms = await GetPlatformsAsync();
            return View(model);
        }

        var game = new Game
        {
            Title = model.Title,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            GenreId = model.GenreId,
            PlatformId = model.PlatformId,
            ReleaseDate = model.ReleaseDate,
            TotalCopies = model.TotalCopies > 0 ? model.TotalCopies : 3
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var copies = Enumerable.Range(0, game.TotalCopies)
            .Select(_ => new GameCopy { GameId = game.Id, IsRented = false });

        _context.GameCopies.AddRange(copies);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Game added successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        var game = await _context.Games
            .Include(g => g.Genre)
            .Include(g => g.Platform)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        var game = await _context.Games
            .Include(g => g.Copies)
            .Include(g => g.Reviews)
            .Include(g => g.WishlistedBy)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        var copyIds = game.Copies.Select(c => c.Id).ToList();
        var rentals = await _context.Rentals
            .Where(r => copyIds.Contains(r.GameCopyId))
            .ToListAsync();

        _context.Rentals.RemoveRange(rentals);
        _context.Reviews.RemoveRange(game.Reviews);
        _context.Wishlists.RemoveRange(game.WishlistedBy);
        _context.GameCopies.RemoveRange(game.Copies);
        _context.Games.Remove(game);

        await _context.SaveChangesAsync();

        TempData["Success"] = "Game deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        => await _context.Genres
            .OrderBy(g => g.Name)
            .Select(g => new SelectListItem(g.Name, g.Id.ToString()))
            .ToListAsync();

    private async Task<IEnumerable<SelectListItem>> GetPlatformsAsync()
        => await _context.Platforms
            .OrderBy(p => p.Name)
            .Select(p => new SelectListItem(p.Name, p.Id.ToString()))
            .ToListAsync();
}
