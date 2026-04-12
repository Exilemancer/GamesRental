using GamesRental.Data.Models;
using GamesRental.Services.Contracts;
using GamesRental.Web.ViewModels.Game;
using GamesRental.Web.ViewModels.Review;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GamesRental.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GameCatalogPageViewModel> GetAllAvailableAsync(string? searchTerm, int currentPage, int gamesPerPage)
        {
            currentPage = currentPage < 1 ? 1 : currentPage;
            searchTerm = searchTerm?.Trim();

            var gamesQuery = _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Include(g => g.Copies)
                .Where(g => g.Copies.Any(c => !c.IsRented))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var normalizedSearch = searchTerm.ToLower();
                gamesQuery = gamesQuery.Where(g =>
                    g.Title.ToLower().Contains(normalizedSearch)
                    || g.Genre.Name.ToLower().Contains(normalizedSearch)
                    || g.Platform.Name.ToLower().Contains(normalizedSearch));
            }

            var totalGames = await gamesQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalGames / (double)gamesPerPage);
            totalPages = totalPages == 0 ? 1 : totalPages;
            currentPage = currentPage > totalPages ? totalPages : currentPage;

            var games = await gamesQuery
                .OrderBy(g => g.Title)
                .Skip((currentPage - 1) * gamesPerPage)
                .Take(gamesPerPage)
                .Select(g => new GameCatalogViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Genre = g.Genre.Name,
                    Platform = g.Platform.Name,
                    AvailableCopies = g.Copies.Count(c => !c.IsRented)
                })
                .ToListAsync();

            return new GameCatalogPageViewModel
            {
                Games = games,
                SearchTerm = searchTerm ?? string.Empty,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                TotalGames = totalGames
            };
        }

        public async Task<GameDetailsViewModel?> GetByIdAsync(int id, ClaimsPrincipal user)
        {
            var game = await _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Include(g => g.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return null;
            }

            string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            bool hasUserRented = false;
            bool isInWishlist = false;
            bool hasAvailableCopies = await _context.GameCopies
                .AnyAsync(gc => gc.GameId == id && !gc.IsRented);

            if (!string.IsNullOrEmpty(userId))
            {
                hasUserRented = await _context.Rentals
                    .AnyAsync(r => r.GameCopy.GameId == id && r.UserId == userId && r.ReturnedOn == null);

                isInWishlist = await _context.Wishlists
                    .AnyAsync(w => w.GameId == id && w.UserId == userId);
            }

            return new GameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                Genre = game.Genre.Name,
                Platform = game.Platform.Name,
                ReleaseDate = game.ReleaseDate,
                HasAvailableCopies = hasAvailableCopies,
                HasUserRented = hasUserRented,
                IsInWishlist = isInWishlist,
                Reviews = game.Reviews
                    .OrderByDescending(r => r.CreatedOn)
                    .Select(r => new GameReviewListItemViewModel
                    {
                        Author = r.User.Email ?? r.User.UserName ?? "Anonymous",
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CreatedOn = r.CreatedOn
                    })
                    .ToList()
            };
        }

        public async Task DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GameAdminViewModel>> GetAllForAdminAsync()
        {
            return await _context.Games
                .Select(g => new GameAdminViewModel
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .ToListAsync();
        }

        public async Task<GameFormViewModel> GetCreateFormModelAsync()
        {
            return await PopulateCreateFormModelAsync(new GameFormViewModel
            {
                ReleaseDate = DateTime.UtcNow.Date,
                TotalCopies = 1
            });
        }

        public async Task<GameFormViewModel> PopulateCreateFormModelAsync(GameFormViewModel model)
        {
            model.Genres = await _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToListAsync();

            model.Platforms = await _context.Platforms
                .OrderBy(p => p.Name)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToListAsync();

            return model;
        }

        public async Task CreateAsync(GameFormViewModel model)
        {
            var game = new Game
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                GenreId = model.GenreId,
                PlatformId = model.PlatformId,
                ReleaseDate = model.ReleaseDate,
                TotalCopies = model.TotalCopies
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            var copies = Enumerable.Range(0, model.TotalCopies)
                .Select(_ => new GameCopy
                {
                    GameId = game.Id,
                    IsRented = false
                });

            _context.GameCopies.AddRange(copies);
            await _context.SaveChangesAsync();
        }
    }
}
