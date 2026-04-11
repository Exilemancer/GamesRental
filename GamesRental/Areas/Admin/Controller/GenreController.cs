using GamesRental.Data.Models;
using GamesRental.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class GenreController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public GenreController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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

        var genres = await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();

        return View(genres);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name)
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            TempData["Error"] = "Genre name is required.";
            return RedirectToAction(nameof(Index));
        }

        var normalizedName = name.Trim();
        var exists = await _context.Genres.AnyAsync(g => g.Name == normalizedName);

        if (exists)
        {
            TempData["Error"] = "Genre already exists.";
            return RedirectToAction(nameof(Index));
        }

        _context.Genres.Add(new Genre { Name = normalizedName });
        await _context.SaveChangesAsync();

        TempData["Success"] = "Genre added successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await User.IsAdminAsync(_userManager))
        {
            return Forbid();
        }

        var genre = await _context.Genres
            .Include(g => g.Games)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            TempData["Error"] = "Genre not found.";
            return RedirectToAction(nameof(Index));
        }

        if (genre.Games.Any())
        {
            TempData["Error"] = "Cannot delete genre that is used by games.";
            return RedirectToAction(nameof(Index));
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Genre deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
