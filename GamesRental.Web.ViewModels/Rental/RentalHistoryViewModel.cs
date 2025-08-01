namespace GamesRental.Web.ViewModels.Rental
{
    public class RentalHistoryViewModel
    {
        public string GameTitle { get; set; } = null!;

        public string Platform { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public DateTime RentedOn { get; set; }

        public DateTime ReturnedOn { get; set; }
    }
}
