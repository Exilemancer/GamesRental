namespace GamesRental.Web.Areas.Admin.ViewModels
{
    public class StatisticsIndexViewModel
    {
        public int TotalGames { get; set; }

        public int TotalUsers { get; set; }

        public int TotalGenres { get; set; }

        public int TotalPlatforms { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public int RentedCopies { get; set; }

        public int TotalRentals { get; set; }

        public int TotalReviews { get; set; }

        public int WishlistEntries { get; set; }

        public IEnumerable<GameRentalStatisticsViewModel> GameStatistics { get; set; } = new List<GameRentalStatisticsViewModel>();

        public IEnumerable<UserRentalStatisticsViewModel> UserStatistics { get; set; } = new List<UserRentalStatisticsViewModel>();
    }

    public class GameRentalStatisticsViewModel
    {
        public int GameId { get; set; }

        public string GameTitle { get; set; } = string.Empty;

        public string Platform { get; set; } = string.Empty;

        public int TotalRentals { get; set; }

        public int ActiveRentals { get; set; }

        public IEnumerable<RentalHistoryItemViewModel> RentalHistory { get; set; } = new List<RentalHistoryItemViewModel>();
    }

    public class UserRentalStatisticsViewModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int TotalRentals { get; set; }

        public int ActiveRentals { get; set; }

        public IEnumerable<RentalHistoryItemViewModel> RentalHistory { get; set; } = new List<RentalHistoryItemViewModel>();
    }

    public class RentalHistoryItemViewModel
    {
        public string GameTitle { get; set; } = string.Empty;

        public string Platform { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public DateTime RentedOn { get; set; }

        public DateTime? ReturnedOn { get; set; }
    }
}
