using GamesRental.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GamesRental.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool LooksLikeAdmin(this ClaimsPrincipal? user)
        {
            if (user?.Identity?.IsAuthenticated != true)
            {
                return false;
            }

            var possibleNames = new[]
            {
                user.Identity?.Name,
                user.FindFirstValue(ClaimTypes.Name),
                user.FindFirstValue(ClaimTypes.Email),
                user.FindFirstValue(ClaimTypes.Upn),
                user.FindFirstValue("preferred_username"),
                user.FindFirstValue("email")
            };

            return possibleNames
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Any(value => value!.Contains("admin", StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<bool> IsAdminAsync(this ClaimsPrincipal? user, UserManager<ApplicationUser> userManager)
        {
            if (user?.Identity?.IsAuthenticated != true)
            {
                return false;
            }

            var appUser = await userManager.GetUserAsync(user);
            if (appUser == null)
            {
                return user.LooksLikeAdmin();
            }

            if (await userManager.IsInRoleAsync(appUser, "Admin"))
            {
                return true;
            }

            return (appUser.Email?.Contains("admin", StringComparison.OrdinalIgnoreCase) ?? false)
                || (appUser.UserName?.Contains("admin", StringComparison.OrdinalIgnoreCase) ?? false)
                || user.LooksLikeAdmin();
        }
    }
}
