using GamesRental.Data;
using GamesRental.Data.Models;
using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Rental;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Services
{
    public class RentalService : IRentalService
    {
        private readonly ApplicationDbContext _context;

        public RentalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RentGameAsync(int gameId, string userId)
        {
            var copy = await _context.GameCopies
                .FirstOrDefaultAsync(gc => gc.GameId == gameId && !gc.IsRented);

            if (copy == null)
                return false;

            var rental = new Rental
            {
                GameCopyId = copy.Id,
                UserId = userId,
                RentedOn = DateTime.UtcNow
            };

            copy.IsRented = true;
            copy.RentedByUserId = userId;
            copy.RentedOn = DateTime.UtcNow;

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RentalViewModel>> GetActiveRentalsByUserAsync(string userId)
        {
            return await _context.Rentals
                .Include(r => r.GameCopy)
                    .ThenInclude(gc => gc.Game)
                        .ThenInclude(g => g.Platform)
                .Where(r => r.UserId == userId && r.ReturnedOn == null)
                .Select(r => new RentalViewModel
                {
                    RentalId = r.Id,
                    GameTitle = r.GameCopy.Game.Title,
                    Platform = r.GameCopy.Game.Platform.Name,
                    ImageUrl = r.GameCopy.Game.ImageUrl,
                    RentedOn = r.RentedOn
                })
                .ToListAsync();
        }

        public async Task<bool> ReturnGameAsync(int rentalId, string userId)
        {
            var rental = await _context.Rentals
                .Include(r => r.GameCopy)
                .FirstOrDefaultAsync(r => r.Id == rentalId && r.UserId == userId && r.ReturnedOn == null);

            if (rental == null)
                return false;

            rental.ReturnedOn = DateTime.UtcNow;

            rental.GameCopy.IsRented = false;
            rental.GameCopy.RentedByUserId = null;
            rental.GameCopy.RentedOn = null;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
