using GamesRental.Data.Models;
using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Wishlist;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;

        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishlistGameViewModel>> GetUserWishlistAsync(string userId)
        {
            return await _context.Wishlists
                .Include(w => w.Game).ThenInclude(g => g.Genre)
                .Include(w => w.Game).ThenInclude(g => g.Platform)
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistGameViewModel
                {
                    GameId = w.GameId,
                    Title = w.Game.Title,
                    ImageUrl = w.Game.ImageUrl,
                    Genre = w.Game.Genre.Name,
                    Platform = w.Game.Platform.Name
                })
                .ToListAsync();
        }

        public async Task<bool> AddToWishlistAsync(int gameId, string userId)
        {
            if (await _context.Wishlists.AnyAsync(w => w.GameId == gameId && w.UserId == userId))
                return false;

            _context.Wishlists.Add(new Wishlist
            {
                GameId = gameId,
                UserId = userId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int gameId, string userId)
        {
            var item = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.GameId == gameId && w.UserId == userId);

            if (item == null) return false;

            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsInWishlistAsync(int gameId, string userId)
        {
            return await _context.Wishlists.AnyAsync(w => w.GameId == gameId && w.UserId == userId);
        }
    }
}
