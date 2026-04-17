using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    [Comment("Gaming platform")]
    public class Platform
    {
        [Key]
        [Comment("Platform identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(PlatformNameMaxLength)]
        [Comment("Platform name")]
        public string Name { get; set; } = null!;

        [Comment("Games available on the platform")]
        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
