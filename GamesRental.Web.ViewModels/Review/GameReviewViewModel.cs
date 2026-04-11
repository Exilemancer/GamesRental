namespace GamesRental.Web.ViewModels.Review
{
    public class MyReviewViewModel
    {
        public string GameTitle { get; set; } = null!;

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
