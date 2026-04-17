using GamesRental.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace GamesRental.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static async Task<bool> IsAdminAsync(this ClaimsPrincipal? user, UserManager<ApplicationUser> userManager)
        {
            if (user?.Identity?.IsAuthenticated != true)
            {
                return false;
            }

            var appUser = await userManager.GetUserAsync(user);
            if (appUser == null)
            {
                return false;
            }

            return await userManager.IsInRoleAsync(appUser, "Admin");
        }
    }
}
