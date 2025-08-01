using GamesRental.Web.ViewModels.Game;

namespace GamesRental.Services.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameCatalogViewModel>> GetAvailableGamesAsync();

        Task<GameDetailsViewModel?> GetGameDetailsByIdAsync(int id);

    }
}
