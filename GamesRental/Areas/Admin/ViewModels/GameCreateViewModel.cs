using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static GamesRental.GCommon.ValidationConstants;

namespace GamesRental.Web.Areas.Admin.ViewModels
{
    public class GameCreateViewModel
    {
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
        [Range(1, 100)]
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
