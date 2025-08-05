using GamesRental.Web.ViewModels.Game;
using System.Security.Claims;

namespace GamesRental.Services.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameCatalogViewModel>> GetAvailableGamesAsync();

        Task<GameDetailsViewModel?> GetGameDetailsByIdAsync(int id, ClaimsPrincipal user);

        Task<GameDetailsViewModel?> GetDetailsAsync(int id, ClaimsPrincipal user);
    }
}
