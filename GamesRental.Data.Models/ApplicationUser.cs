using Microsoft.AspNetCore.Identity;

namespace GamesRental.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public virtual ICollection<WishlistItem> Wishlist { get; set; } = new HashSet<WishlistItem>();
    }
}
