using GamesRental.Data.Models;
using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddReviewAsync(ReviewFormViewModel model, string userId)
        {
            if (await _context.Reviews.AnyAsync(r => r.GameId == model.GameId && r.UserId == userId))
                return false;

            var review = new Review
            {
                GameId = model.GameId,
                UserId = userId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedOn = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasUserReviewedGameAsync(int gameId, string userId)
        {
            return await _context.Reviews.AnyAsync(r => r.GameId == gameId && r.UserId == userId);
        }
    }
}
