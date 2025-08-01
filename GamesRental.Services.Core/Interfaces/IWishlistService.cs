using GamesRental.Web.ViewModels.Wishlist;

namespace GamesRental.Services.Contracts
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistGameViewModel>> GetUserWishlistAsync(string userId);
        Task<bool> AddToWishlistAsync(int gameId, string userId);
        Task<bool> RemoveFromWishlistAsync(int gameId, string userId);
        Task<bool> IsInWishlistAsync(int gameId, string userId);
    }
}
