using GamesRental.Data;
using GamesRental.Data.Models;
using GamesRental.Services.Contracts;
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
    }
}
