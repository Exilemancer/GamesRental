using GamesRental.Data.Models;
using GamesRental.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class PlatformController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PlatformController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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

        var platforms = await _context.Platforms
            .OrderBy(p => p.Name)
            .ToListAsync();

        return View(platforms);
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
            TempData["Error"] = "Platform name is required.";
            return RedirectToAction(nameof(Index));
        }

        var normalizedName = name.Trim();
        var exists = await _context.Platforms.AnyAsync(p => p.Name == normalizedName);

        if (exists)
        {
            TempData["Error"] = "Platform already exists.";
            return RedirectToAction(nameof(Index));
        }

        _context.Platforms.Add(new Platform { Name = normalizedName });
        await _context.SaveChangesAsync();

        TempData["Success"] = "Platform added successfully.";
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

        var platform = await _context.Platforms
            .Include(p => p.Games)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (platform == null)
        {
            TempData["Error"] = "Platform not found.";
            return RedirectToAction(nameof(Index));
        }

        if (platform.Games.Any())
        {
            TempData["Error"] = "Cannot delete platform that is used by games.";
            return RedirectToAction(nameof(Index));
        }

        _context.Platforms.Remove(platform);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Platform deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
