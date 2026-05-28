using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Presistance.Identity
{
    public static class ApplicationIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "User",
                    Email = "user@example.com",
                    UserName = "user@example.com",
                    PhoneNumber = "01112233445"
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
