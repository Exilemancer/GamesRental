using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    [Comment("User review for a game")]
    public class Review
    {
        [Key]
        [Comment("Review identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Identifier of the user who wrote the review")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [Comment("User who wrote the review")]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Identifier of the reviewed game")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        [Comment("Reviewed game")]
        public virtual Game Game { get; set; } = null!;

        [Range(ReviewRatingMinValue, ReviewRatingMaxValue)]
        [Comment("Review rating value")]
        public int Rating { get; set; }

        [MaxLength(ReviewContentMaxLength)]
        [Comment("Review comment text")]
        public string? Comment { get; set; }

        [Comment("Date and time when the review was created")]
        public DateTime CreatedOn { get; set; }
    }
}
