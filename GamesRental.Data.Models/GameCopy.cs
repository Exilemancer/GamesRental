using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Data.Models
{
    public class GameCopy
    {
        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; } = null!;

        public bool IsRented { get; set; }

        public string? RentedByUserId { get; set; }

        [ForeignKey(nameof(RentedByUserId))]
        public ApplicationUser? RentedByUser { get; set; }

        public DateTime? RentedOn { get; set; }
    }
}