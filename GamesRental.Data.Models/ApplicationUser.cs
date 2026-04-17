using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesRental.Data.Models
{
    [Comment("Application user account")]
    public class ApplicationUser : IdentityUser
    {
        [Comment("User rentals")]
        public virtual ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();

        [Comment("User reviews")]
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        [Comment("User wishlist entries")]
        public virtual ICollection<Wishlist> Wishlist { get; set; } = new HashSet<Wishlist>();
    }
}
