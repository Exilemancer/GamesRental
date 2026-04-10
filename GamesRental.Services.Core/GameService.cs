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

		public async Task<IEnumerable<GameCatalogViewModel>> GetAllAvailableAsync()
		{
			return await _context.Games
				.Include(g => g.Genre)
				.Include(g => g.Platform)
				.Include(g => g.Copies)
				.Where(g => g.Copies.Any(c => !c.IsRented))
				.Select(g => new GameCatalogViewModel
				{
					Id = g.Id,
					Title = g.Title,
					ImageUrl = g.ImageUrl,
					Genre = g.Genre.Name,
					Platform = g.Platform.Name,
					AvailableCopies = g.Copies.Count(c => !c.IsRented)
				})
				.ToListAsync();
		}

        public async Task<GameDetailsViewModel?> GetByIdAsync(int id, ClaimsPrincipal user)
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

        public async Task DeleteAsync(int id)
		{
			var game = await _context.Games.FindAsync(id);
			if (game != null)
			{
				_context.Games.Remove(game);
				await _context.SaveChangesAsync();
			}
		}

        public async Task<IEnumerable<GameAdminViewModel>> GetAllForAdminAsync()
        {
            return await _context.Games
                .Select(g => new GameAdminViewModel
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .ToListAsync();
        }
    }
}