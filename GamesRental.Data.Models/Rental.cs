using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Data.Models
{
    [Comment("Rental transaction")]
    public class Rental
    {
        [Key]
        [Comment("Rental identifier")]
        public int Id { get; set; }

        [Comment("Related game copy identifier")]
        public int GameCopyId { get; set; }

        [Comment("Related rented game copy")]
        public virtual GameCopy GameCopy { get; set; } = null!;

        [Comment("Identifier of the user who rented the game copy")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [Comment("User who rented the game copy")]
        public virtual ApplicationUser User { get; set; } = null!;

        [Comment("Date and time when the game copy was rented")]
        public DateTime RentedOn { get; set; }

        [Comment("Date and time when the game copy was returned")]
        public DateTime? ReturnedOn { get; set; }
    }
}
