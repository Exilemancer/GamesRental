namespace GamesRental.Web.ViewModels.Game
{
    public class GameCatalogViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Platform { get; set; } = null!;

        public int AvailableCopies { get; set; }
    }
}
