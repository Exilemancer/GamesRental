namespace GamesRental.Web.ViewModels.Game
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Platform { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public bool HasUserRented { get; set; }

        public bool IsInWishlist { get; set; }
    }
}
