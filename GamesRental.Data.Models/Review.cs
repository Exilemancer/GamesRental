using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(ReviewContentMaxLength)]
        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}