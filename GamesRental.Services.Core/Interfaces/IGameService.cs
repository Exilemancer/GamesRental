using GamesRental.Web.ViewModels.Game;
using System.Security.Claims;

public interface IGameService
{
	Task<IEnumerable<GameCatalogViewModel>> GetAllAvailableAsync();

	Task<GameDetailsViewModel?> GetByIdAsync(int id, ClaimsPrincipal user);

	Task<IEnumerable<GameAdminViewModel>> GetAllForAdminAsync();

	Task DeleteAsync(int id);
}