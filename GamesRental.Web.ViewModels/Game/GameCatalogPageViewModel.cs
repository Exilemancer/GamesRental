namespace GamesRental.Web.ViewModels.Game
{
    public class GameCatalogPageViewModel
    {
        public IEnumerable<GameCatalogViewModel> Games { get; set; } = new List<GameCatalogViewModel>();

        public string SearchTerm { get; set; } = string.Empty;

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalGames { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
