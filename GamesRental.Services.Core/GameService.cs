using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Game;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GamesRental.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameCatalogViewModel>> GetAvailableGamesAsync()
        {
            var games = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Include(g => g.Copies)
                .Where(g => g.Copies.Any(c => !c.IsRented))
                .ToListAsync();

            return games.Select(g => new GameCatalogViewModel
            {
                Id = g.Id,
                Title = g.Title,
                ImageUrl = g.ImageUrl,
                Genre = g.Genre.Name,
                Platform = g.Platform.Name,
                AvailableCopies = g.Copies.Count(c => !c.IsRented)
            });
        }

        public async Task<GameDetailsViewModel?> GetDetailsAsync(int id, ClaimsPrincipal user)
        {
            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
                return null;

            string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            bool hasUserRented = false;
            bool isInWishlist = false;

            if (!string.IsNullOrEmpty(userId))
            {
                hasUserRented = await _context.Rentals
                    .AnyAsync(r => r.GameCopy.GameId == id && r.UserId == userId && r.ReturnedOn == null);

                isInWishlist = await _context.Wishlists
                    .AnyAsync(w => w.GameId == id && w.UserId == userId);
            }

            return new GameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                Genre = game.Genre.Name,
                Platform = game.Platform.Name,
                ReleaseDate = game.ReleaseDate,
                HasUserRented = hasUserRented,
                IsInWishlist = isInWishlist
            };
        }

        public async Task<GameDetailsViewModel?> GetGameDetailsByIdAsync(int id, ClaimsPrincipal user)
        {
            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
                return null;

            string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            bool hasUserRented = false;
            bool isInWishlist = false;

            if (!string.IsNullOrEmpty(userId))
            {
                hasUserRented = await _context.Rentals
                    .AnyAsync(r => r.GameCopy.GameId == id && r.UserId == userId && r.ReturnedOn == null);

                isInWishlist = await _context.Wishlists
                    .AnyAsync(w => w.GameId == id && w.UserId == userId);
            }

            return new GameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                Genre = game.Genre.Name,
                Platform = game.Platform.Name,
                ReleaseDate = game.ReleaseDate,
                HasUserRented = hasUserRented,
                IsInWishlist = isInWishlist
            };
        }
    }
}