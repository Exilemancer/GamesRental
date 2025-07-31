using System.ComponentModel.DataAnnotations;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PlatformNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}