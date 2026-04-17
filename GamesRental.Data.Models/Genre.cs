using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    [Comment("Game genre")]
    public class Genre
    {
        [Key]
        [Comment("Genre identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        [Comment("Genre name")]
        public string Name { get; set; } = null!;

        [Comment("Games assigned to the genre")]
        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
