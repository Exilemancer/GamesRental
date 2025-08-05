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
                Genre = g.Genre.Name,
                Platform = g.Platform.Name,
                AvailableCopies = g.Copies.Count(c => !c.IsRented)
            });
        }

        public async Task<GameDetailsViewModel?> GetGameDetailsByIdAsync(int id)
        {
            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Include(g => g.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return null;

            return new GameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                Genre = game.Genre.Name,
                Platform = game.Platform.Name,
                ReleaseDate = game.ReleaseDate,
                Reviews = game.Reviews.Select(r => new GameReviewViewModel
                {
                    Username = r.User.UserName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                }).ToList()
            };
        }
    }
}
