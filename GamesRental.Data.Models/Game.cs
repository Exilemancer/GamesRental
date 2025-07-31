using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int TotalCopies { get; set; }

        public int GenreId { get; set; }
        
        [ForeignKey(nameof(GenreId))]
        public virtual Genre Genre { get; set; } = null!;

        public int PlatformId { get; set; }

        [ForeignKey(nameof(PlatformId))]
        public virtual Platform Platform { get; set; } = null!;

        public virtual ICollection<GameCopy> Copies { get; set; } = new HashSet<GameCopy>();

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public virtual ICollection<WishlistItem> WishlistedBy { get; set; } = new HashSet<WishlistItem>();
    }
}