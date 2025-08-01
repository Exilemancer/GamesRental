namespace GamesRental.Web.ViewModels.Game
{
    public class GameReviewViewModel
    {
        public string Username { get; set; } = null!;

        public int Rating { get; set; }

        public string Comment { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}
