using Microsoft.AspNetCore.Identity;
using DomainLayer.Models.Identity;

namespace ServiceAbstraction
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
