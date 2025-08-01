namespace GamesRental.Services.Contracts
{
    public interface IRentalService
    {
        Task<bool> RentGameAsync(int gameId, string userId);
    }
}
