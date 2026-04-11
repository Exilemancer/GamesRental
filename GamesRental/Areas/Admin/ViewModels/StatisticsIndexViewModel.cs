namespace GamesRental.Web.Areas.Admin.ViewModels
{
    public class StatisticsIndexViewModel
    {
        public int TotalGames { get; set; }

        public int TotalGenres { get; set; }

        public int TotalPlatforms { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public int RentedCopies { get; set; }

        public int TotalReviews { get; set; }

        public int WishlistEntries { get; set; }
    }
}
