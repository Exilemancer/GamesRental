using GamesRental.Data;
using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Game;
using Microsoft.EntityFrameworkCore;

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
                Genre = g.Genre?.Name,
                Platform = g.Platform?.Name,
                AvailableCopies = g.Copies.Count(c => !c.IsRented)
            });
        }
    }
}
