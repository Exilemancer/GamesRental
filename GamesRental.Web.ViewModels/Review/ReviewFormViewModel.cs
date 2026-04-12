using System.ComponentModel.DataAnnotations;
using GamesRental.GCommon;

namespace GamesRental.Web.ViewModels.Review
{
    public class ReviewFormViewModel
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        [Range(ValidationConstants.ReviewRatingMinValue, ValidationConstants.ReviewRatingMaxValue)]
        public int Rating { get; set; }

        [Required]
        [StringLength(ValidationConstants.ReviewContentMaxLength)]
        public string Comment { get; set; } = null!;
    }
}
