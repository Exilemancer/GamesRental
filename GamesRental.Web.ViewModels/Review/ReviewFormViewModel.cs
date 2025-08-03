using System.ComponentModel.DataAnnotations;

namespace GamesRental.Web.ViewModels.Review
{
    public class ReviewFormViewModel
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }
    }
}
