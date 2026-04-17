using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    [Comment("Game available for rental")]
    public class Game
    {
        [Key]
        [Comment("Game identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        [Comment("Game title")]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GameDescriptionMinLength)]
        [MaxLength(GameDescriptionMaxLength)]
        [Comment("Game description")]
        public string Description { get; set; } = null!;

        [MinLength(GameImageUrlMinLength)]
        [MaxLength(GameImageUrlMaxLength)]
        [Comment("Game cover image URL")]
        public string ImageUrl { get; set; } = null!;

        [Comment("Game release date")]
        public DateTime ReleaseDate { get; set; }

        [Comment("Total number of copies for the game")]
        public int TotalCopies { get; set; }

        [Comment("Related genre identifier")]
        public int GenreId { get; set; }
        
        [ForeignKey(nameof(GenreId))]
        [Comment("Related genre")]
        public virtual Genre Genre { get; set; } = null!;

        [Comment("Related platform identifier")]
        public int PlatformId { get; set; }

        [ForeignKey(nameof(PlatformId))]
        [Comment("Related platform")]
        public virtual Platform Platform { get; set; } = null!;

        [Comment("Physical or digital copies of the game")]
        public virtual ICollection<GameCopy> Copies { get; set; } = new HashSet<GameCopy>();

        [Comment("Game reviews")]
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        [Comment("Wishlist entries for the game")]
        public virtual ICollection<Wishlist> WishlistedBy { get; set; } = new HashSet<Wishlist>();
    }
}
