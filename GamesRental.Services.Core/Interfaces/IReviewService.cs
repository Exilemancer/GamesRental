using GamesRental.Web.ViewModels.Review;

namespace GamesRental.Services.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewFormViewModel model, string userId);

        Task<bool> HasUserReviewedGameAsync(int gameId, string userId);

        Task<IEnumerable<MyReviewViewModel>> GetUserReviewsAsync(string userId);
    }
}
