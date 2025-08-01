using GamesRental.Web.ViewModels.Rental;

namespace GamesRental.Services.Contracts
{
    public interface IRentalService
    {
        Task<bool> RentGameAsync(int gameId, string userId);

        Task<IEnumerable<RentalViewModel>> GetActiveRentalsByUserAsync(string userId);

    }
}
