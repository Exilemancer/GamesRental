using GamesRental.Web.ViewModels.Game;
using System.Security.Claims;

public interface IGameService
{
    Task<IEnumerable<GameCatalogViewModel>> GetAllAvailableAsync();

    Task<GameDetailsViewModel?> GetByIdAsync(int id, ClaimsPrincipal user);

    Task<IEnumerable<GameAdminViewModel>> GetAllForAdminAsync();

    Task<GameFormViewModel> GetCreateFormModelAsync();

    Task<GameFormViewModel> PopulateCreateFormModelAsync(GameFormViewModel model);

    Task CreateAsync(GameFormViewModel model);

    Task DeleteAsync(int id);
}
