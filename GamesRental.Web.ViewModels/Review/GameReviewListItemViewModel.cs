namespace GamesRental.Web.ViewModels.Review
{
    public class GameReviewListItemViewModel
    {
        public string Author { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
