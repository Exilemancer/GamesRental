using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        public int GameCopyId { get; set; }

        public virtual GameCopy GameCopy { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public DateTime RentedOn { get; set; }

        public DateTime? ReturnedOn { get; set; }
    }
}