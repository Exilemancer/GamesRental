using System.ComponentModel.DataAnnotations;
using static GamesRental.GCommon.ValidationConstants;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace GamesRental.Web.ViewModels.Game
{
    public class GameFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GameTitleMinLength)]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GameDescriptionMinLength)]
        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Url]
        [MinLength(GameImageUrlMinLength)]
        [MaxLength(GameImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required]
        [Display(Name = "Platform")]
        public int PlatformId { get; set; }

        [Required]
        [Range(GameTotalCopiesMinValue, GameTotalCopiesMaxValue)]
        [Display(Name = "Total Copies")]
        public int TotalCopies { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        
        public virtual IEnumerable<SelectListItem> Genres { get; set; } = new HashSet<SelectListItem>();

        public virtual IEnumerable<SelectListItem> Platforms { get; set; } = new HashSet<SelectListItem>();
    }
}
