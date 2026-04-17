using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Data.Models
{
    [Comment("Wishlist entry")]
    public class Wishlist
    {
        [Key]
        [Comment("Wishlist entry identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Identifier of the user who added the game to the wishlist")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [Comment("User who owns the wishlist entry")]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Identifier of the wished game")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        [Comment("Game added to the wishlist")]
        public virtual Game Game { get; set; } = null!;

        [Comment("Date and time when the game was added to the wishlist")]
        public DateTime AddedOn { get; set; }
    }
}
