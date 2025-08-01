using GamesRental.Data;
using GamesRental.Data.Models;
using GamesRental.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .ToListAsync();

            return View(games);
        }

        // TODO: Create, Edit, Delete
 
    [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new GameCreateViewModel
            {
                Genres = _context.Genres
                    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                    .ToList(),
                Platforms = _context.Platforms
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres
                    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                    .ToList();
                model.Platforms = _context.Platforms
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToList();

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
                TotalCopies = model.TotalCopies,
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
                    
            for (int i = 0; i < model.TotalCopies; i++)
            {
                _context.GameCopies.Add(new GameCopy
                {
                    GameId = game.Id,
                    IsRented = false
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null) return NotFound();

            var model = new GameCreateViewModel
            {
                Title = game.Title,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                GenreId = game.GenreId,
                PlatformId = game.PlatformId,
                ReleaseDate = game.ReleaseDate,
                TotalCopies = game.TotalCopies,

                Genres = _context.Genres.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }),
                Platforms = _context.Platforms.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
            };

            ViewBag.GameId = game.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GameCreateViewModel model)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
                model.Platforms = _context.Platforms.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                ViewBag.GameId = id;
                return View(model);
            }

            game.Title = model.Title;
            game.Description = model.Description;
            game.ImageUrl = model.ImageUrl;
            game.GenreId = model.GenreId;
            game.PlatformId = model.PlatformId;
            game.ReleaseDate = model.ReleaseDate;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games
                .Include(g => g.Copies)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return NotFound();

            _context.GameCopies.RemoveRange(game.Copies);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
