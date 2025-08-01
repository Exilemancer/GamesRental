namespace GamesRental.Web.ViewModels.Wishlist
{
    public class WishlistGameViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Platform { get; set; } = null!;

    }
}
