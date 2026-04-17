using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Data.Models
{
    [Comment("Individual rentable copy of a game")]
    public class GameCopy
    {
        [Key]
        [Comment("Game copy identifier")]
        public int Id { get; set; }

        [Comment("Related game identifier")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        [Comment("Related game")]
        public virtual Game Game { get; set; } = null!;

        [Comment("Whether the copy is currently rented")]
        public bool IsRented { get; set; }

        [Comment("Identifier of the user currently renting the copy")]
        public string? RentedByUserId { get; set; }

        [ForeignKey(nameof(RentedByUserId))]
        [Comment("User currently renting the copy")]
        public ApplicationUser? RentedByUser { get; set; }

        [Comment("Date and time when the copy was rented")]
        public DateTime? RentedOn { get; set; }
    }
}
